using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace comp4004ProjDeliverable1
{
    using Model;

    public struct GenerationRule
    {
        public int PatientsLeft;
        public int VisitsAllowed;

        public GenerationRule(int patientsLeft, int visitsAllowed)
        {
            this.PatientsLeft = patientsLeft;
            this.VisitsAllowed = visitsAllowed;
        }
    }

    class Controller
    {
        public long ProcessExecutionTime;

        public Controller()
        {
            
        }

        //generate data for iteration 1
        public void createIteration1PatientsAndVisits(int numberOfPatients)
        {
            Random random = new Random();
            List<Patient> patients = new List<Patient>();
            List<Patient> globallySimilarPatients = new List<Patient>();
            List<Patient> globallyNonSimilarPatients = new List<Patient>();
            List<PatientMatchNode> patientMatchNodes = new List<PatientMatchNode>();
            float globallySimilarPatientsThreshold = 0.1f;
            float globallyNonSimilarPatientsThreshold = 0.1f;
            int requiredNumberOfGloballySimilarPatients = (int)((globallySimilarPatientsThreshold * numberOfPatients) + 0.5f);
            int requiredNumberOfGloballyNonSimilarPatients = (int)((globallyNonSimilarPatientsThreshold * numberOfPatients) + 0.5f);
            int currentNumberOfGloballySimilarPatients = 0;
            int currentNumberOfGloballyNonSimilarPatients = 0;
            DateTime maxRangeStart = new DateTime(2011, 01, 01);
            DateTime maxRangeEnd = new DateTime(2011, 12, 31);
            DateTime rangeStart = Util.getRandomDate(maxRangeStart, maxRangeEnd.AddMonths(-1));
            DateTime rangeEnd = rangeStart.AddDays(15);
            
            int randRationalMin = 1;
            int randRationalMax = 5;


            DbMethods.getInstance().clearTables();

            for (int i = 0; i < numberOfPatients; i++)
            {
                int patientID = i;
                DbMethods.getInstance().InsertPatient();
                Patient patient = new Patient(patientID);
                PatientMatchNode patientNode = new PatientMatchNode(patient);
                
                int numberOfVisits = random.Next(10, 20);
                bool restart = true;

                do
                {
                    patient.ClearVisits();

                    //generate directed visits
                    for (int j = 0; j < numberOfVisits; j++)
                    {
                        Visit v = new Visit(-1, random.Next(1, 100), random.Next(randRationalMin, randRationalMax), Util.getRandomDate(rangeStart, rangeEnd));
                        if (!patient.HasVisit(v))
                        {
                            patient.AddVisit(v);
                        }
                        else
                        {
                            j--;
                        }
                    }

                    if (requiredNumberOfGloballySimilarPatients > 0)
                    {
                        //SIMILAR PATIENTS

                        foreach (PatientMatchNode n in patientMatchNodes)
                        {
                            if (n.Patient.HasSimilarVisits(patient, 0.5f))
                            {
                                n.addMatch(patient);
                                restart = false;
                            }
                        }

                        List<PatientMatchNode> nodesToRemove = new List<PatientMatchNode>();
                        foreach (PatientMatchNode n in patientMatchNodes)
                        {
                            if (n.getMatchesNumber() >= 4)
                            {
                                requiredNumberOfGloballySimilarPatients--;
                                globallySimilarPatients.Add(n.Patient);
                                nodesToRemove.Add(n);
                            }
                        }

                        foreach (PatientMatchNode n in nodesToRemove)
                        {
                            patientMatchNodes.Remove(n);
                        }

                        nodesToRemove.Clear();

                        
                        if(patientMatchNodes.Count < requiredNumberOfGloballySimilarPatients)
                        {
                            patientMatchNodes.Add(patientNode);
                            restart = false;
                        }
                        
                    }
                    else if (requiredNumberOfGloballyNonSimilarPatients > 0)
                    {
                        //NON-SIMILAR PATIENTS
                        rangeStart = maxRangeStart;
                        rangeEnd = maxRangeEnd;

                        bool hasSimilar = false;
                        foreach (Patient p in patients)
                        {
                            if (!patient.HasNoSimilarVisits(p))
                            {
                                hasSimilar = true;
                            }
                        }

                        if (!hasSimilar)
                        {
                            restart = false;
                            requiredNumberOfGloballyNonSimilarPatients--;
                            globallyNonSimilarPatients.Add(patient);
                            Console.WriteLine("Patient " + patient.ID + " has been added to the blacklist");
                        }
                    }
                    else
                    {
                        bool hasSimilar = false;

                        foreach (Patient p in globallyNonSimilarPatients)
                        {
                            if (!patient.HasNoSimilarVisits(p))
                            {
                                hasSimilar = true;        
                            }
                        }

                        if (!hasSimilar)
                        {
                            restart = false;
                        }
                    }

                }
                while(restart);

                //adding the visits
                foreach (Visit v in patient.Visits)
                {
                    DbMethods.getInstance().InsertVisit(patientID, v.ProfessionalID, v.RationalID, v.Date);
                }

                patients.Add(patient);
                Console.WriteLine();
            }

            Console.WriteLine("Added " + globallySimilarPatients.Count + " globally similar patients");
            Console.WriteLine("Added " + globallyNonSimilarPatients.Count + " globally non-similar patients");
            Console.WriteLine("Added " + patients.Count + " patients");
        }

        //generate data for iteration 2
        public void createIteration2Patients(int numPatients)
        {
            DateTime now = DateTime.Now;
            Random random = new Random();
            int randRationalMin = 1;
            int randRationalMax = 5;

            DbMethods.getInstance().clearTables();

            for (int i = 0; i < 5; i++)
            {
                int id = i;
                DbMethods.getInstance().InsertPatient();
                for(int j = 0; j < 10; j++)
                {
                    DbMethods.getInstance().InsertVisit(id, j, 2, now);
                }
            }

            for (int i = 5; i < numPatients; i++)
            {
                int id = i;
                DbMethods.getInstance().InsertPatient();
                for (int j = 0; j < 20; j++)
                {
                    DbMethods.getInstance().InsertVisit(id, random.Next(1, 10), random.Next(randRationalMin, randRationalMax), Util.getRandomDate(new DateTime(2012, 01, 01), new DateTime(2012, 12, 31)));
                }
            }
        }

        //generate data for iteration 3 scenario 1
        public void createIteration3Patients(
            int patientCount_in, 
            int safePatientsNeeded_in,
            int safenessThreshold_in,
            List<GenerationRule> generationRules_in)
        {
            int patientCount = patientCount_in;
            int safePatientsNeeded = safePatientsNeeded_in;
            int patientsInserted = 0;
            int safenessThreshold = safenessThreshold_in;
            Queue<GenerationRule> generationRules = new Queue<GenerationRule>(generationRules_in);
            GenerationRule currentRule = generationRules.Dequeue();
            Random r = new Random();
            List<Visit> safeVisitsBuffer = new List<Visit>();
            DbMethods db = DbMethods.getInstance();
            int counter = 0;

            db.clearTables();

            while (patientsInserted < patientCount)
            {
                if (currentRule.PatientsLeft == 0)
                {
                    currentRule = generationRules.Dequeue();
                }

                db.InsertPatient();
                int patientID = patientsInserted;//db.InsertPatient();
                int visitsLeft = currentRule.VisitsAllowed;


                //adding safe visits
                if (safePatientsNeeded >= 0)
                {
                    if (patientsInserted % (safenessThreshold+1) == 0)
                    {
                        safeVisitsBuffer = new List<Visit>();

                        while(visitsLeft > 0)
                        {
                            safeVisitsBuffer.Add(new Visit(-1, r.Next(1, 10), r.Next(1, 6), new DateTime(2012, r.Next(1, 13), r.Next(1, 29))));
                            visitsLeft--;
                        }
                    }

                    db.InsertVisits(new Dictionary<int, List<Visit>>() { { patientID, safeVisitsBuffer } });
                    safePatientsNeeded--;
                }
                else
                {
                    while (visitsLeft > 0)
                    {
                        db.InsertVisit(patientID, r.Next(1, 10), r.Next(1, 6), new DateTime(2012, r.Next(1, 13), r.Next(1, 29)));
                        visitsLeft--;
                    }
                }
                

                currentRule.PatientsLeft--;
                patientsInserted++;
            }
        }

        //return list of ACV for a specified patient using the tree based algorithm
        public List<ACV> retrievePatientACV_clientBased(int patientID, int n)
        {
            List<Visit> visits = DbMethods.getInstance().getPatientVisits(patientID);
            Patient p = new Patient(patientID);
            

            List<ACV> acvs = new List<ACV>();
            p.FindACVs(n, new ACVFoundEventHandler(
                (Patient sender, ACVFoundArgs args) =>
                    {
                        acvs.Add(args.ACV);
                    }
            ));

            return acvs;
        }

        //return list of ACV for a specified patient using the query based algorithm
        public List<ACV> retrievePatientACV_queryBased(int patientID, int n)
        {
            List<ACV> acvs = DbMethods.getInstance().getPatientACV(patientID, n);
            
            return acvs;
        }

        //return list of safe patients using the query algorithm
        public List<Patient> getSafePatients_queries(int acvSize)
        {
            List<Patient> patients = DbMethods.getInstance().getPatientsWVisits();
            List<Patient> safePatients = new List<Patient>();
            /*
            foreach (Patient p in patients)
            {
                if (p.IsSafe(acvSize))
                {
                    safePatients.Add(p);
                }
            }*/

            return safePatients;
        }

        //return list of safe patients using the client side algorithm
        public List<Patient> getSafePatients_client(int acvSize, int safetyThreshold)
        {
            List<Patient> patients = DbMethods.getInstance().getPatientsWVisits();

            return getSafePatients_client(acvSize, safetyThreshold, patients);
        }

        public List<Patient> getSafePatients_client(int acvSize, int safetyThreshold, List<Patient> patients)
        {
            List<Patient> safePatients = new List<Patient>();

            foreach (Patient p in patients)
            {
                if (p.IsSafe(patients, acvSize, safetyThreshold))
                {
                    safePatients.Add(p);
                }
            }

            return safePatients;
        }

        //return list of unsafe patients using the query based algorithm
        public List<Patient> getUnsafePatients_queries(int acvSize)
        {
            List<Patient> patients = DbMethods.getInstance().getPatients();
            List<Patient> unsafePatient = new List<Patient>();

            /*
            foreach (Patient p in patients)
            {
                if (!p.IsSafe(acvSize))
                {
                    unsafePatient.Add(p);
                }
            }*/

            return unsafePatient;
        }

        //get list of unsafe patients using the client side algorithm
        public List<Patient> getUnsafePatients_client(int acvSize, int safetyThreshold)
        {
            List<Patient> patients = DbMethods.getInstance().getPatientsWVisits();
            return getUnsafePatients_client(acvSize, safetyThreshold, patients);
        }

        public List<Patient> getUnsafePatients_client(int acvSize, int safetyThreshold, List<Patient> patients)
        {
            List<Patient> unsafePatients = new List<Patient>();

            foreach (Patient p in patients)
            {
                if (!p.IsSafe(patients, acvSize, safetyThreshold))
                {
                    unsafePatients.Add(p);
                }
            }

            return unsafePatients;
        }
    }
}
