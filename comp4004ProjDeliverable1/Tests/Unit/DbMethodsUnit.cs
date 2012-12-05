using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Data.SqlServerCe;
using System.Data;

namespace comp4004ProjDeliverable1
{
    using Model;

    class DbMethodsUnit
    {
        [Fact]
        public void getPatientsNumberTest()
        {
            int patientsNumber = DbMethods.getInstance().getPatientsCount();
            Assert.True(patientsNumber >= 0);

            int patientToDelete = this.insertPatient();
            int newPatientsNumber = DbMethods.getInstance().getPatientsCount();
            Assert.True(newPatientsNumber > patientsNumber);

            this.deletePatient(patientToDelete);
            newPatientsNumber = DbMethods.getInstance().getPatientsCount();
            Assert.True(patientsNumber == newPatientsNumber);
        }

        [Fact]
        public void getVisitsNumberTest()
        {
            int patientID = this.insertPatient();

            int visitsNumber = DbMethods.getInstance().getVisitsCount(patientID);
            Assert.True(visitsNumber >= 0);

            int visitToDelete = this.insertVisit(patientID, 1, 1, DateTime.Now);
            int newVisitsNumber = DbMethods.getInstance().getVisitsCount(patientID);
            Assert.True(newVisitsNumber > visitsNumber);

            this.deleteVisit(visitToDelete);
            this.deletePatient(patientID);
            newVisitsNumber = DbMethods.getInstance().getVisitsCount(patientID);
            Assert.True(visitsNumber == newVisitsNumber);
        }

        [Fact]
        public void addPatientTest()
        {
            DbMethods.getInstance().clearTables();

            int patientsNumber = DbMethods.getInstance().getPatientsCount();
            int patientID = 0;
                
            DbMethods.getInstance().InsertPatient();
            
            int newPatientsNumber = DbMethods.getInstance().getPatientsCount();
            Assert.True(newPatientsNumber > patientsNumber);

            this.deletePatient(patientID);
        }

        [Fact]
        public void insertVisitTest()
        {
            DbMethods.getInstance().clearTables();
            int patientID = 0;
                
            DbMethods.getInstance().InsertPatient();
        
            int visitsNumber = DbMethods.getInstance().getVisitsCount(patientID);

            DbMethods.getInstance().InsertVisit(patientID, 1, 1, DateTime.Now);
            int newVisitsNumber = DbMethods.getInstance().getVisitsCount(patientID);
            Assert.True(newVisitsNumber > visitsNumber);
        }

        [Fact]
        public void insertVisitsTest()
        {
            Dictionary<int, List<Visit>> data = new Dictionary<int, List<Visit>>();

            int pId = this.insertPatient();

            data.Add(pId, new List<Visit>()
            {
                new Visit(pId, 1, 1, DateTime.Parse("2012-12-12")),
                new Visit(pId, 1, 1, DateTime.Parse("2012-11-11"))
            });

            DbMethods.getInstance().InsertVisits(data);

            List<Visit> visits = DbMethods.getInstance().getPatientVisits(pId);

            Assert.True(visits.Count == 2);
        }

        [Fact]
        public void getPatientVisits()
        {
            int patientID = this.insertPatient();
            DateTime date = DateTime.Now;

            int visit1 = this.insertVisit(patientID, 1, 1, date);
            int visit2 = this.insertVisit(patientID, 2, 2, date);

            List<Visit> visits = DbMethods.getInstance().getPatientVisits(patientID);

            Assert.True(visits.Count == 2);

            for (int i = 0; i < visits.Count; i++)
            {
                Assert.True(visits[i].ProfessionalID == (i+1));
                Assert.True(visits[i].Date.Date == date.Date);
            }
        }

        [Fact]
        public void getPatientMatchesCountTest()
        {
            int pID1 = this.insertPatient();
            int pID2 = this.insertPatient();
            int pID3 = this.insertPatient();
            ACV acv1 = new ACV(new List<Visit>());
            ACV acv2 = new ACV(new List<Visit>());
            ACV acv3 = new ACV(new List<Visit>());
            DateTime now = DateTime.Now;

            this.insertVisit(pID1, 1, 1, now);
            this.insertVisit(pID1, 1, 1, now);
            this.insertVisit(pID1, 1, 2, now);

            this.insertVisit(pID2, 1, 1, now);
            this.insertVisit(pID2, 1, 2, now);
            this.insertVisit(pID2, 1, 3, now);
            
            this.insertVisit(pID3, 1, 1, now.AddDays(1));
            this.insertVisit(pID3, 1, 2, now.AddDays(2));
            this.insertVisit(pID3, 1, 3, now.AddDays(3));

            acv1.Add(new Visit(1, 1, 1, now));
            acv1.Add(new Visit(1, 1, 1, now));

            int count = DbMethods.getInstance().getPatientMatchesCount(acv1, pID1);
            Assert.True(count == 0);

            acv2.Add(new Visit(1, 1, 1, now));
            acv2.Add(new Visit(1, 1, 2, now));

            count = DbMethods.getInstance().getPatientMatchesCount(acv2, pID1);
            Assert.True(count == 1);

            acv3.Add(new Visit(1, 1, 1, now.AddDays(1)));

            count = DbMethods.getInstance().getPatientMatchesCount(acv3, pID1);
            Assert.True(count == 1);
        }

        [Fact]
        public void getPatientACVTest()
        {
            int pID1 = this.insertPatient();
            DateTime now = DateTime.Now;
            int ACVSize = 3;

            this.insertVisit(pID1, 1, 1, now);
            this.insertVisit(pID1, 1, 1, now);
            this.insertVisit(pID1, 1, 2, now);
            this.insertVisit(pID1, 1, 1, now);
            this.insertVisit(pID1, 1, 1, now);
            this.insertVisit(pID1, 1, 2, now);

            List<ACV> acvs = DbMethods.getInstance().getPatientACV(pID1, ACVSize);

            Assert.True(acvs.Count == (Util.getFactorial(6)/(Util.getFactorial(ACVSize)*Util.getFactorial(6-ACVSize))));
        }

        private int insertPatient()
        {
            SqlCeCommand insertCommand = new SqlCeCommand("INSERT INTO Patient([Type]) VALUES('Human')", DbMethods.getInstance().getConnection());
            insertCommand.ExecuteNonQuery();

            SqlCeCommand getIdCommand = new SqlCeCommand("SELECT ID FROM Patient WHERE ID = @@IDENTITY", DbMethods.getInstance().getConnection());
            int uid = (int)getIdCommand.ExecuteScalar();

            return uid;
        }

        private void deletePatient(int patientID)
        {
            SqlCeCommand deleteCommand = new SqlCeCommand("DELETE FROM Patient WHERE ID = @patientID", DbMethods.getInstance().getConnection());
            deleteCommand.Parameters.AddWithValue("@patientID", patientID);
            deleteCommand.ExecuteNonQuery();
        }

        private int insertVisit(int patientID, int professionalID, int rationalID, DateTime date)
        {
            SqlCeCommand insertCommand = new SqlCeCommand("INSERT INTO Visit(FK_Patient, FK_Professional, FK_Rational, date) VALUES(@patientID, @professionalID, @rationalID, @date)", DbMethods.getInstance().getConnection());
            insertCommand.Parameters.AddWithValue("@patientID", patientID);
            insertCommand.Parameters.AddWithValue("@professionalID", professionalID);
            insertCommand.Parameters.AddWithValue("@rationalID", rationalID);
            insertCommand.Parameters.AddWithValue("@date", date);

            insertCommand.ExecuteNonQuery();

            SqlCeCommand getIdCommand = new SqlCeCommand("SELECT ID FROM Visit WHERE ID = @@IDENTITY", DbMethods.getInstance().getConnection());
            int id = (int)getIdCommand.ExecuteScalar();

            return id;
        }

        private void deleteVisit(int visitID)
        {
            SqlCeCommand deleteCommand = new SqlCeCommand("DELETE FROM Visit WHERE ID = @visitID", DbMethods.getInstance().getConnection());
            deleteCommand.Parameters.AddWithValue("@visitID", visitID);
            deleteCommand.ExecuteNonQuery();
        }
    }
}
