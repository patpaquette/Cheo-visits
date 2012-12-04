using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace comp4004ProjDeliverable1.Views
{
    using Model;

    class ConsoleView : BaseView
    {
        int _noOfPatients;
        Controller _controller;
        
        public ConsoleView()
        {
            _noOfPatients = 0;
            Console.SetWindowSize(150, 60);
            Console.SetWindowPosition(0, 0);
            Console.BufferHeight = Int16.MaxValue - 1;
            _controller = new Controller();
        }

        public override void Render()
        {
            do
            {
                Console.WriteLine("How many Patients would you like to create?(between 10 and 50)");
                _noOfPatients = int.Parse(Console.ReadLine());
            }
            while (_noOfPatients < 10 || _noOfPatients > 50);

            this._controller.createIteration2Patients(this._noOfPatients);

            this.RenderMainMenu();
        }

        private void RenderMainMenu()
        {
            bool exit = false;

            string[] menuItems = new string[]{
                "Show list of patients",
                "Show patient visits",
                "Work on one patient",
                "CM of visits 1, 5, and 9 of patient 1 matches",
                "Get list of safe patients with the client based algorithm",
                "Get list of safe patients with the query based algorithm",
                "Get list of unsafe patients with the client based algorithm",
                "Get list of unsafe patients with the query based algorithm",
                "Exit"
            };

            int itemChoice = -1;

            while (!exit)
            {
                do
                {
                    int counter = 1;
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do?");
                    foreach (string menuItem in menuItems)
                    {
                        Console.WriteLine(counter++ + ". " + menuItem);
                    }

                    try
                    {
                        itemChoice = int.Parse(Console.ReadLine()) - 1;
                    }
                    catch (Exception e)
                    {
                        itemChoice = -1;
                    }
                }
                while (itemChoice < 0 || itemChoice >= menuItems.Length);

                Console.WriteLine();
                switch (menuItems[itemChoice])
                {
                    case "Show list of patients":
                        this.RenderPatients();
                        Console.WriteLine();
                        break;
                    case "Show patient visits":
                        this.RenderPatientVisits();
                        Console.WriteLine();
                        break;
                    case "Work on one patient":
                        this.RenderPatientMenu();
                        break;
                    case "CM of visits 1, 5, and 9 of patient 1 matches":
                        List<Visit> p1Visits = DbMethods.getInstance().getPatientVisits(DbMethods.getInstance().getPatients()[0].ID);
                        ACV cm = new ACV(new List<Visit>()
                        {
                            p1Visits[0],
                            p1Visits[4],
                            p1Visits[8]
                        });

                        this.RenderCMMatches(cm);
                        break;
                    case "Get list of safe patients with the client based algorithm":
                        this.RenderSafePatients(0);
                        break;
                    case "Get list of safe patients with the query based algorithm":
                        this.RenderSafePatients(1);
                        break;
                    case "Get list of unsafe patients with the client based algorithm":
                        this.RenderUnsafePatients(0);
                        break;
                    case "Get list of unsafe patients with the query based algorithm":
                        this.RenderUnsafePatients(1);
                        break;
                    case "Exit":
                        exit = true;
                        break;
                }
            }
        }

        private void RenderSafePatients(int method)
        {
            int n = 0;
            List<Patient> patients = new List<Patient>();
            Stopwatch sw = new Stopwatch();
            long processOnlyTimer;

            do
            {
                Console.WriteLine("Choose the size of the ACVs : ");
                n = int.Parse(Console.ReadLine());
            }
            while (n < 0);

            sw.Start();
            if (method == 0)
            {
                patients = this._controller.getSafePatients_client(n, 3);
            }
            else if(method == 1)
            {
                patients = this._controller.getSafePatients_queries(n);
            }

            processOnlyTimer = sw.ElapsedMilliseconds;

            foreach (Patient p in patients)
            {
                Console.WriteLine(p.ToString());
            }

            Console.WriteLine("RetrieveSafePatients without output to console : " + processOnlyTimer + " ms");
            Console.WriteLine("RetrieveSafePatients : " + sw.ElapsedMilliseconds + " ms");
            Console.WriteLine();
        }

        private void RenderUnsafePatients(int method)
        {
            
            int n = 0;
            List<Patient> patients = new List<Patient>();
            Stopwatch sw = new Stopwatch();
            long processOnlyTimer;

            do
            {
                Console.WriteLine("Choose the size of the ACVs : ");
                n = int.Parse(Console.ReadLine());
            }
            while (n < 0);

            sw.Start();
            if (method == 0)
            {
                patients = this._controller.getUnsafePatients_client(n, 3);
            }
            else if (method == 1)
            {
                patients = this._controller.getUnsafePatients_queries(n);
            }

            processOnlyTimer = sw.ElapsedMilliseconds;

            foreach (Patient p in patients)
            {
                Console.WriteLine(p.ToString());
            }

            sw.Stop();

            Console.WriteLine("RetrieveUnsafePatients without output to console : " + processOnlyTimer + " ms");
            Console.WriteLine("RetrieveUnsafePatients : " + sw.ElapsedMilliseconds + " ms");
            Console.WriteLine();
        }

        private void RenderPatientMenu()
        {
            int patientID = -1;
            bool back = false;
            int itemChoice = -1;

            string[] menuItems = new string[]{
                "Get all ACVs for this patient using the client method",
                "Get all ACVs for this patient using the query method",
                "Get all ACVs matching a CM of size n",
                "Back"
            };

            patientID = this.AskForPatientID();

            while (!back)
            {
                int counter = 1;

                do
                {
                    foreach (string menuItem in menuItems)
                    {
                        Console.WriteLine(counter++ + ". " + menuItem);
                    }

                    itemChoice = int.Parse(Console.ReadLine()) - 1;
                }
                while (itemChoice < 0 || itemChoice >= menuItems.Length);

                switch (menuItems[itemChoice])
                {
                    case "Get all ACVs for this patient using the client method":
                        this.RenderACVs(patientID, 0);
                        break;
                    case "Get all ACVs for this patient using the query method":
                        this.RenderACVs(patientID, 1);
                        break;
                    case "Get all ACVs matching a CM of size n":
                        this.RenderCMMatches(patientID);
                        break;
                    case "Back":
                        back = true;
                        break;
                }
            }
        }

        private void RenderACVs(int patientID, int method)
        {
            int n = 0;
            do
            {
                Console.WriteLine("Choose the size of the ACVs : ");
                n = int.Parse(Console.ReadLine());
            }
            while (n < 0);

            if (method == 0) //client based
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                List<ACV> acvs = this._controller.retrievePatientACV_clientBased(patientID, n);

                long processOnlyTimer = sw.ElapsedMilliseconds;

                int counter = 1;
                foreach (ACV acv in acvs)
                {
                    Console.WriteLine("ACV no : " + counter++);
                    Console.WriteLine(acv.ToString());
                }

                sw.Stop();
                Console.WriteLine("RetrivePatientACV_clientBased without output to console : " + processOnlyTimer + " ms");
                Console.WriteLine("RetrievePatientACV_queryBased : " + sw.ElapsedMilliseconds + " ms");
                Console.WriteLine();
            }
            else if (method == 1)//query based
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                
                List<ACV> acvs = this._controller.retrievePatientACV_queryBased(patientID, n);

                long processOnlyTimer = sw.ElapsedMilliseconds;
                
                int counter = 1;
                foreach (ACV acv in acvs)
                {
                    Console.WriteLine("ACV no : " + counter++);
                    Console.WriteLine(acv.ToString());
                }

                sw.Stop();
                Console.WriteLine("RetrivePatientACV_queryBased without output to console : " + processOnlyTimer + " ms");
                Console.WriteLine("RetrievePatientACV_queryBased : " + sw.ElapsedMilliseconds + " ms");
                Console.WriteLine();
            }
        }

        private void RenderCMMatches(ACV cm)
        {
            int patientID = this.AskForPatientID();
            Patient p = new Patient(patientID);

            List<ACV> matches = p.GetCMMatches(cm);
            int matchCounter = 1;

            foreach (ACV acv in matches)
            {
                Console.WriteLine("Match : " + matchCounter++);
                Console.WriteLine(acv.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Combination to match : ");
            Console.WriteLine(cm.ToString());
            Console.WriteLine("Number of matches : " + matches.Count);
            Console.WriteLine();
        }

        private void RenderCMMatches(int patientID)
        {
            int cmSize = 0;
            ACV CM = new ACV(new List<Visit>());
            Patient p = new Patient(patientID);

            do
            {
                Console.WriteLine("Choose the size of the CM : ");
                cmSize = int.Parse(Console.ReadLine());
            } while (cmSize <= 0);

            for (int i = 0; i < cmSize; i++)
            {
                int year;
                int month;
                int day;
                int rational;

                do
                {
                    Console.WriteLine("CM no : " + (i + 1));
                    Console.WriteLine("Month : ");
                    month = int.Parse(Console.ReadLine());
                } while (month <= 0);

                do
                {
                    Console.WriteLine("Day : ");
                    day = int.Parse(Console.ReadLine());
                } while (day <= 0);

                do
                {
                    Console.WriteLine("Year : ");
                    year = int.Parse(Console.ReadLine());
                } while (year <= 0);

                do
                {
                    Console.WriteLine("Rational : ");
                    Console.WriteLine("1. New");
                    Console.WriteLine("2. Checkup");
                    Console.WriteLine("3. Follow-up");
                    Console.WriteLine("4. Referral");
                    Console.WriteLine("5. Emergency");
                    rational = int.Parse(Console.ReadLine());
                } while (rational < 1 && rational > 5);

                CM.Add(new Visit(-1, -1, rational, new DateTime(year, month, day)));
            }

            //this._controller.matchCM(patientID, CM, false);
            List<ACV> matches = p.GetCMMatches(CM);
            int matchCounter = 1;

            foreach (ACV acv in matches)
            {
                Console.WriteLine();
                Console.WriteLine("Match : " + matchCounter++);
                Console.WriteLine(acv.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Combination to match : ");
            Console.WriteLine(CM.ToString());
            Console.WriteLine("Number of matches : " + matches.Count);
            Console.WriteLine();
        }

        private void RenderPatientVisits()
        {
            int patientID = -1;
            List<Visit> visits = null;

            patientID = this.AskForPatientID();

            visits = DbMethods.getInstance().getPatientVisits(patientID);

            foreach (Visit v in visits)
            {
                Console.WriteLine(v.ToString());
                Console.WriteLine();
            }
            
        }

        private void RenderPatients()
        {
            List<Patient> patients = DbMethods.getInstance().getPatients();

            foreach (Patient patient in patients)
            {
                Console.WriteLine(patient.ToString());
            }
        }


        private int AskForPatientID()
        {
            int patientID = -1;

            do
            {
                Console.WriteLine("What is the patient's ID?");
                patientID = int.Parse(Console.ReadLine());
            }
            while (!DbMethods.getInstance().PatientExists(patientID));

            return patientID;
        }
    }
}
