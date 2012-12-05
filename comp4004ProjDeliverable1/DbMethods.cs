using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using comp4004ProjDeliverable1.Model;
using System.Diagnostics;

namespace comp4004ProjDeliverable1
{
    class DbMethods
    {
        private static DbMethods _instance;
        private SqlCeConnection _conn;

        public static DbMethods getInstance()
        {
            if(DbMethods._instance == null)
            {
                DbMethods._instance = new DbMethods();
            }

            return DbMethods._instance;
        }

        private DbMethods()
        {
            this._conn = new SqlCeConnection(@"Data Source=|DataDirectory|\Database.sdf");
            this._conn.Open();
        }

        //insert patients in database
        public void InsertPatient()
        {
            SqlCeCommand insertCommand = new SqlCeCommand("INSERT INTO Patient([Type]) VALUES('Human')", this._conn);
            
            int ID;

            insertCommand.ExecuteNonQuery();
            //ID = (int)selectCommand.ExecuteScalar();

            //Console.WriteLine("Patient " + ID + " has been inserted into the database");
        }

        public int GetLastInsertedPatientID()
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT * FROM Patient WHERE ID = @@IDENTITY", this._conn);

            return (int)selectCommand.ExecuteScalar();
        }

        //check if patient exists
        public bool PatientExists(int patientID)
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT COUNT(*) FROM Patient WHERE ID = @patientID", this._conn);
            selectCommand.Parameters.AddWithValue("@patientID", patientID);

            int count = (int)selectCommand.ExecuteScalar();

            if (count == 1)
            {
                return true;
            }

            return false;
        }

        public int GetLastInsertedVisitID()
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT * FROM Visit WHERE ID = @@IDENTITY", this._conn);

            return (int)selectCommand.ExecuteScalar();
        }

        //insert visit for a patient
        public void InsertVisit(int patientID, int professionalID, int rationalID, DateTime date) 
        {
            SqlCeCommand insertCommand = new SqlCeCommand("INSERT INTO Visit([FK_Patient], [FK_Professional], [FK_Rational], [date]) VALUES(@patientID, @professionalID, @rationalID, @date)", this._conn);
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT ID FROM Visit WHERE ID = @@IDENTITY", this._conn);
            int ID;

            insertCommand.Parameters.AddWithValue("@patientID", patientID);
            insertCommand.Parameters.AddWithValue("@professionalID", professionalID);
            insertCommand.Parameters.AddWithValue("@rationalID", rationalID);
            insertCommand.Parameters.AddWithValue("@date", date);

            insertCommand.ExecuteNonQuery();
            //ID = (int)selectCommand.ExecuteScalar();

            //Console.WriteLine("Visit added, ID : " + ID + ", Date : " + date + ", Rational : " + rationalID + " - " + this.getRational(rationalID));
        }

        public void InsertVisits(Dictionary<int, List<Visit>> data)
        {
            foreach (KeyValuePair<int, List<Visit>> row in data)
            {
                foreach (Visit v in row.Value)
                {
                    this.InsertVisit(row.Key, v.ProfessionalID, v.RationalID, v.Date);
                }
            }
        }

        //get all visits for a patient
        public List<Visit> getPatientVisits(int patientID)
        {
            List<Visit> visitsList = new List<Visit>();
            Patient patient = new Patient(patientID);
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT * FROM Visit WHERE FK_Patient = @patientID", this._conn);
            selectCommand.Parameters.AddWithValue("@patientID", patientID);
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(selectCommand);
            DataTable visits = new DataTable();
            dataAdapter.Fill(visits);

            foreach (DataRow row in visits.Rows)
            {
                visitsList.Add(new Visit(patient, (int)row["ID"], (int)row["FK_Professional"], (int)row["FK_Rational"], (DateTime)row["date"]));
            }

            return visitsList;
        }

        //get the number of patients in the database
        public int getPatientsCount()
        {
            SqlCeCommand countCommand = new SqlCeCommand("SELECT COUNT(*) FROM Patient", this._conn);
            int count = (int)countCommand.ExecuteScalar();

            return count;
        }

        //get the number of visits for a patient
        public int getVisitsCount(int patientID)
        {
            SqlCeCommand countCommand = new SqlCeCommand("SELECT COUNT(*) FROM Visit WHERE FK_Patient = @patientID", this._conn);
            countCommand.Parameters.AddWithValue("@patientID", patientID);
            int count = (int)countCommand.ExecuteScalar();

            return count;
        }

        //get total number of visits
        public int getVisitsCount()
        {
            SqlCeCommand countCommand = new SqlCeCommand("SELECT COUNT(*) FROM Visit", this._conn);
            int count = (int)countCommand.ExecuteScalar();

            return count;
        }

        //get all patients
        public List<Patient> getPatients()
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT * FROM Patient", this._conn);
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(selectCommand);
            DataTable dt = new DataTable();
            List<Patient> patients = new List<Patient>();

            dataAdapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                patients.Add(new Patient((int)row["ID"]));
            }

            return patients;
        }

        //get all patients with visits
        public List<Patient> getPatientsWVisits()
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT *, v.id AS visitID FROM Patient p JOIN Visit v ON p.id = FK_Patient", this._conn);
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(selectCommand);
            DataTable dt = new DataTable();
            List<Patient> patients = new List<Patient>();
            Stopwatch sw = new Stopwatch();

            dataAdapter.Fill(dt);

            sw.Start();
            int idBuffer = -1;
            Patient curPatient = null;
            foreach (DataRow row in dt.Rows)
            {
                int id = (int)row["ID"];

                if (id != idBuffer)
                {
                    idBuffer = id;
                    curPatient = new Patient(id);
                    patients.Add(curPatient);
                }

                if (curPatient != null)
                {
                    curPatient.AddVisit(new Visit((int)row["visitID"], (int)row["FK_Professional"], (int)row["FK_Rational"], (DateTime)row["date"]));
                }
            }
            sw.Stop();
            return patients;
        }

        public SqlCeConnection getConnection()
        {
            return this._conn;
        }

        public string getRational(int id)
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT rational FROM Rational WHERE ID = @ID", this._conn);
            selectCommand.Parameters.AddWithValue("@ID", id);
            return (string)selectCommand.ExecuteScalar();
        }

        public List<Rational> getRationals()
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT * from Rational", this._conn);
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(selectCommand);
            DataTable dt = new DataTable();
            List<Rational> rationals = new List<Rational>();

            dataAdapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                rationals.Add(new Rational(row.Field<int>("ID"), row.Field<string>("Rational")));
            }

            return rationals;
        }

        public void clearTables()
        {
            SqlCeCommand deleteCommand = new SqlCeCommand("DELETE FROM Patient", this._conn);
            deleteCommand.ExecuteNonQuery();
            deleteCommand.CommandText = "DELETE FROM Visit";
            deleteCommand.ExecuteNonQuery();

            SqlCeCommand alterTableCommand = new SqlCeCommand("ALTER TABLE Patient alter column id IDENTITY(1,1)", this._conn);
            alterTableCommand.ExecuteNonQuery();
            alterTableCommand.CommandText = "ALTER TABLE Visit alter column id IDENTITY(1,1)";
            alterTableCommand.ExecuteNonQuery();
        }

        //returns the number patient matches for a specified ACV
        public int getPatientMatchesCount(ACV acv, int patientID)
        {
            Dictionary<Visit, int> visits = new Dictionary<Visit, int>();
            bool isAdded = false;
            SqlCeCommand selectCommand = new SqlCeCommand();
            selectCommand.Connection = this._conn;
            string query =
                 "SELECT COUNT(*) " +
                 "FROM (SELECT DISTINCT(FK_Patient) FROM Visit) Visit";
            string where = " WHERE ";
            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (Visit v in acv.Visits)
            {
                isAdded = false;

                foreach (KeyValuePair<Visit, int> pair in visits)
                {
                    if (pair.Key.equals(v))
                    {
                        visits[pair.Key]++;
                        isAdded = true;
                        break;
                    }
                }

                if (!isAdded)
                {
                    visits.Add(v, 1);
                }
            }

            int counter = 0;
            foreach (KeyValuePair<Visit, int> pair in visits)
            {
                if (counter > 0)
                {
                    where += " AND ";
                }

                where += "Visit.FK_Patient IN ("+
                                "SELECT Visit.FK_Patient "+
                                "FROM Visit "+
                                "WHERE ( Visit.FK_Rational = @rationalID" + counter + " AND date = @date" + counter + ") "+
                                "GROUP BY FK_Patient "+
                                "HAVING count( Visit.FK_Patient ) >= @havingCount" + counter + ")";



                selectCommand.Parameters.AddWithValue("@rationalID" + counter, pair.Key.RationalID);
                selectCommand.Parameters.AddWithValue("@date" + counter, pair.Key.Date);
                selectCommand.Parameters.AddWithValue("@havingCount" + counter, pair.Value);

                counter++;
            }

            selectCommand.CommandText = query + where + " AND FK_patient != " + patientID;

            int count = (int)selectCommand.ExecuteScalar();

            sw.Stop();
            //Console.WriteLine("DbMethods.getPatientMatchesCount() : " + sw.ElapsedTicks + " ticks");
            return count;
        }

        //returns list of ACV for a patient
        public List<ACV> getPatientACV(int patientID, int acvSize)
        {
            SqlCeCommand selectCommand = new SqlCeCommand("SELECT DISTINCT * FROM Visit v0", this._conn);
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(selectCommand);
            DataTable dt = new DataTable();
            List<ACV> acvs = new List<ACV>();

            for (int i = 1; i < acvSize; i++)
            {
                selectCommand.CommandText += " JOIN Visit v" + i + " ON v" + (i - 1) + ".FK_patient = v" + i + ".FK_patient";
            }

            selectCommand.CommandText += " WHERE v0.FK_patient = @patientID";
            selectCommand.Parameters.AddWithValue("@patientID", patientID);

            for (int i = 0; i < acvSize - 1; i++)
            {
                selectCommand.CommandText += " AND v" + i + ".id < v" + (i + 1) + ".id";
            }

            dataAdapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                ACV acv = new ACV(new List<Visit>());

                for (int i = 0; i < acvSize; i++)
                {
                    if (i == 0)
                    {
                        acv.Add(new Visit((int)row["ID"], (int)row["FK_Professional"], (int)row["FK_Rational"], (DateTime)row["date"]));
                    }
                    else
                    {
                        acv.Add(new Visit((int)row["ID" + i], (int)row["FK_Professional" + i], (int)row["FK_Rational" + i], (DateTime)row["date" + i]));
                    }
                }

                acvs.Add(acv);
            }

            return acvs;
        }

    }
}
