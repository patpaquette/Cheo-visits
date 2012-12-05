using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace comp4004ProjDeliverable1
{
    using Model;

    class PatientUnit
    {
        List<ACV> _patientACV;

        [Fact]
        public void similarHistoryTest()
        {
            Patient p1 = new Patient(1);
            Patient p2 = new Patient(2);
            Patient p3 = new Patient(3);
            DateTime now = DateTime.Now;

            p1.AddVisit(new Visit(1, 1, 1, now));
            p1.AddVisit(new Visit(2, 1, 2, now));
            p1.AddVisit(new Visit(3, 1, 3, now));
            p1.AddVisit(new Visit(4, 1, 3, now.AddDays(1)));

            p2.AddVisit(new Visit(5, 1, 1, now));
            p2.AddVisit(new Visit(6, 1, 2, now));

            p3.AddVisit(new Visit(7, 1, 3, now));
            p3.AddVisit(new Visit(8, 1, 3, now.AddDays(2)));

            bool result = p1.HasSimilarVisits(p2, 0.5f);
            Assert.True(result == true);

            result = p1.HasSimilarVisits(p3, 0.5f);
            Assert.True(result == false);

            result = p1.HasSimilarVisits(p3, 0.25f);
            Assert.True(result == true);

            result = p1.HasSimilarVisits(p2, 0.75f);
            Assert.True(result == false);
        }

        [Fact]
        public void findACVTest()
        {
            
            DateTime now = DateTime.Now;
            this._patientACV = new List<ACV>();
            int ACVSize = 3;

            DbMethods.getInstance().clearTables();
            DbMethods.getInstance().InsertPatient();
            DbMethods.getInstance().InsertVisit(0, 1, 1, now);
            DbMethods.getInstance().InsertVisit(0, 1, 2, now);
            DbMethods.getInstance().InsertVisit(0, 1, 3, now);
            DbMethods.getInstance().InsertVisit(0, 1, 4, now.AddDays(1));

            Patient p1 = new Patient(0);

            p1.FindACVs(ACVSize, PatientACVFound);

            Assert.True(this._patientACV.Count == (Util.getFactorial(p1.Visits.Count)/(Util.getFactorial(ACVSize)*Util.getFactorial(p1.Visits.Count-ACVSize))));
        }

        [Fact]
        public void matchACVTest()
        {
            this._patientACV = new List<ACV>();
            DateTime now = DateTime.Now;

            

            DbMethods.getInstance().clearTables();
            DbMethods.getInstance().InsertPatient();
            DbMethods.getInstance().InsertVisit(0, 1, 1, now);
            DbMethods.getInstance().InsertVisit(0, 1, 2, now);
            DbMethods.getInstance().InsertVisit(0, 1, 3, now);

            DbMethods.getInstance().InsertPatient();
            DbMethods.getInstance().InsertVisit(1, 1, 1, now);
            DbMethods.getInstance().InsertVisit(1, 1, 2, now);
            DbMethods.getInstance().InsertVisit(1, 1, 3, now);

            Patient p1 = new Patient(0);
            Patient p2 = new Patient(1);

            p2.FindACVs(2, PatientACVFound);

            foreach (ACV v in this._patientACV)
            {
                Assert.True(p1.MatchesACV(v));
            }
        }

        [Fact]
        public void isSafeTest()
        {

            int p1ID = 0;
            int p2ID = 1;
            int p3ID = 2;
            Patient p = new Patient(p1ID);
            DateTime now = DateTime.Now;

            DbMethods.getInstance().clearTables();

            DbMethods.getInstance().InsertPatient();
            DbMethods.getInstance().InsertPatient();
            DbMethods.getInstance().InsertPatient();

            DbMethods.getInstance().InsertVisit(p1ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p1ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p1ID, 1, 2, now);

            DbMethods.getInstance().InsertVisit(p2ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p2ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p2ID, 1, 3, now);

            DbMethods.getInstance().InsertVisit(p3ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p3ID, 1, 2, now);
            DbMethods.getInstance().InsertVisit(p3ID, 1, 3, now);

            p.Visits = DbMethods.getInstance().getPatientVisits(p.ID);

            bool isSafe = p.IsSafe(2, 1);

            Assert.True(isSafe);

            isSafe = p.IsSafe(2, 2);

            Assert.False(isSafe);

            isSafe = p.IsSafe(3, 1);

            Assert.False(isSafe);

            isSafe = p.IsSafe(1, 1);

            Assert.True(isSafe);

            isSafe = p.IsSafe(1, 2);

            Assert.False(isSafe);
        }

        [Fact]
        public void getCMMatchesTest()
        {
            DbMethods.getInstance().clearTables();
            int p1ID = 0;    
            DbMethods.getInstance().InsertPatient();
            
            DateTime now = DateTime.Now;

            DbMethods.getInstance().InsertVisit(p1ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p1ID, 1, 1, now);
            DbMethods.getInstance().InsertVisit(p1ID, 1, 2, now);

            Patient p = new Patient(p1ID);
            p.Visits = DbMethods.getInstance().getPatientVisits(p1ID);

            ACV acv1 = new ACV(new List<Visit>(){
                p.Visits[0],
                p.Visits[1]
            });

            ACV acv2 = new ACV(new List<Visit>()
            {
                p.Visits[0],
                new Visit(1, 1, 3, now)
            });

            List<ACV> matches = p.GetCMMatches(acv1);
            Assert.True(matches.Count == 1);

            matches = p.GetCMMatches(acv2);
            Assert.True(matches.Count == 0);
        }

        private void PatientACVFound(Patient sender, ACVFoundArgs args)
        {
            ACV acv = args.ACV;
            this._patientACV.Add(acv);


        }
    }
}
