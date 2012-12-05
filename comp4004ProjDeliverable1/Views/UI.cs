using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace comp4004ProjDeliverable1
{
    using Model;

    public partial class UI : Form
    {
        private Controller _controller;
        private List<Visit> _patientVisits;
        private List<Patient> _patients;
        private Patient _selectedPatient;
        private DbMethods _db;
        private List<ACV> _clientACVs;
        private List<Visit> _CM;

        public UI()
        {
            InitializeComponent();
            _controller = new Controller();

            this._CM = new List<Visit>();
            this._db = DbMethods.getInstance();
            this.listRationaleAddVisit.DataSource = DbMethods.getInstance().getRationals();
            
        }

        private void udpateDataBindings()
        {
            this.listPatientVisits.DataSource = null;
            this.listPatients.DataSource = this._patients;
        }

        private void updatePatientVisits()
        {
       
            this.listPatientVisits.DataSource = null;
            this.listPatientVisits.DataSource = this._selectedPatient.Visits;
            this.listPatientVisits.DisplayMember = "ShortString";
        }

        private void updateCMs()
        {
            this.listCM.DataSource = null;
            this.listCM.DataSource = this._CM;
            this.listCM.DisplayMember = "ShortString";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UI_Load(object sender, EventArgs e)
        {
        }

        private void btnGenerateScanario1_Click(object sender, EventArgs e)
        {
            this._db.clearTables();

            int safePatientsPercentage;
                
            try
            {
                safePatientsPercentage = int.Parse(this.tbPatientSafetyPercentage.Text);
            }
            catch(Exception ex)
            {
                safePatientsPercentage = 10;
            }
                
            
            _controller.createIteration3Patients(1000, (int)(1000*safePatientsPercentage/100.0f), 5, new List<GenerationRule>()
            {
                new GenerationRule(100, 20),
                new GenerationRule(100, 25),
                new GenerationRule(100, 30),
                new GenerationRule(100, 35),
                new GenerationRule(100, 40),
                new GenerationRule(100, 45),
                new GenerationRule(100, 50),
                new GenerationRule(100, 55),
                new GenerationRule(100, 60),
                new GenerationRule(100, 65)
            });

            this._patients = DbMethods.getInstance().getPatientsWVisits();
            this.udpateDataBindings();
        }

        private void btnGenerateScenario2_Click(object sender, EventArgs e)
        {
            this._db.clearTables();

            int safePatientsPercentage;

            try
            {
                safePatientsPercentage = int.Parse(this.tbPatientSafetyPercentage.Text);
            }
            catch (Exception ex)
            {
                safePatientsPercentage = 10;
            }


            _controller.createIteration3Patients(10000, (int)(10000 * safePatientsPercentage / 100.0f), 5, new List<GenerationRule>()
            {
                new GenerationRule(1000, 20),
                new GenerationRule(1000, 30),
                new GenerationRule(1000, 40),
                new GenerationRule(1000, 50),
                new GenerationRule(1000, 60),
                new GenerationRule(1000, 70),
                new GenerationRule(1000, 80),
                new GenerationRule(1000, 90),
                new GenerationRule(1000, 100),
                new GenerationRule(1000, 110)
            });

            this._patients = DbMethods.getInstance().getPatientsWVisits();
            this.udpateDataBindings();
        }

        private void tmpGenerationButton_Click(object sender, EventArgs e)
        {
            int safePatientsPercentage;

            try
            {
                safePatientsPercentage = int.Parse(this.tbPatientSafetyPercentage.Text);
            }
            catch (Exception ex)
            {
                safePatientsPercentage = 10;
            }

            _controller.createIteration3Patients(50, (int)(50*safePatientsPercentage/100.0f), 5, new List<GenerationRule>()
            {
                new GenerationRule(25, 20),
                new GenerationRule(25, 20)
            });

            this._patients = this._db.getPatientsWVisits();
            this.udpateDataBindings();
            this.updatePatientVisits();
        }

        private void listPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._selectedPatient = (Patient)this.listPatients.SelectedValue;
            this.updatePatientVisits();
        }

        private void btnAddVisit_Click(object sender, EventArgs e)
        {
            DateTime date = this.calendarAddVisit.SelectionStart;
            int rational = ((Rational)this.listRationaleAddVisit.SelectedValue).ID;
            Random r = new Random();
            int acvSize = int.Parse(this.tbACVSize.Text);
            int professionalID = r.Next(1, 10);
            Stopwatch sw = new Stopwatch();
            //List<Patient> patients = this._db.getPatientsWVisits();

            this._db.InsertVisit(this._selectedPatient.ID, professionalID, rational, date);
            int visitId = this._db.GetLastInsertedVisitID();
            this._selectedPatient.Visits.Add(new Visit(visitId, professionalID, rational, date));
            this.updatePatientVisits();

            sw.Start();
            if(this._selectedPatient.IsStillSafe(this._patients, acvSize, new Visit(-1, professionalID, rational, date)))
            {
                this.lblPatientSafety.Text = "Safe";
            }
            else
            {
                this.lblPatientSafety.Text = "Unsafe";
            }
            sw.Stop();

            this.lblSafetyCheckTime.Text = sw.ElapsedMilliseconds + " ms";
        }

        private void btnGenerateACVs_Click(object sender, EventArgs e)
        {

            int size = int.Parse(tbACVSize.Text);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            this._clientACVs = this._controller.retrievePatientACV_clientBased(this._selectedPatient.ID, size);
            sw.Stop();

            this.lblACVTime.Text = this._clientACVs.Count + " ACVs in " + sw.ElapsedMilliseconds + " ms";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckSafety_Click(object sender, EventArgs e)
        {
            Patient p = this._selectedPatient;
            int size = int.Parse(tbACVSize.Text);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            if (this._selectedPatient.IsSafe(this._patients, size))
            {
                this.lblPatientSafety.Text = "Safe";
            }
            else
            {
                this.lblPatientSafety.Text = "Unsafe";
            }
            sw.Stop();

            this.lblSafetyCheckTime.Text = sw.ElapsedMilliseconds + " ms";
        }

        private void btnAddToCM_Click(object sender, EventArgs e)
        {

            this._CM.Add((Visit)this.listPatientVisits.SelectedValue);
            this.listCM.DataSource = null;
            this.listCM.DataSource = this._CM;
            this.updateCMs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date = this.calendarAddVisit.SelectionStart;
            int rational = ((Rational)this.listRationaleAddVisit.SelectedValue).ID;
            Random r = new Random();

            this._CM.Add(new Visit(this._selectedPatient.ID, r.Next(1, 11), rational, date));
            this.updateCMs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Visit> visits = this._CM;
            List<Patient> patients = this._patients;
            patients.Remove(this._selectedPatient);
            Stopwatch sw = new Stopwatch();
            List<Patient> matchedPatients = new List<Patient>();
            string output = "";

            sw.Start();
            foreach (Patient p in patients)
            {
                if (p.MatchesACV(new ACV(visits)))
                {
                    matchedPatients.Add(p);
                }
            }
            sw.Stop();

            foreach (Patient p in matchedPatients)
            {
                output += p.ToString() + Environment.NewLine;
            }

            this.output(output);
            this.lblMatchCMTime.Text = sw.ElapsedMilliseconds + " ms";
        }

        private void output(string output)
        {
            this.tbOutput.Text = "";
            this.tbOutput.Text = output;
        }

        private void appendOutput(string append)
        {
            this.tbOutput.Text += append;
        }

        private void btnShowACVs_Click(object sender, EventArgs e)
        {
            string output = "";
            foreach (ACV acv in this._clientACVs)
            {
                output += acv.ToString() + Environment.NewLine;
            }

            this.output(output);
        }

        private void btnGetSafePatients_Click(object sender, EventArgs e)
        {
            int safetyThreshold = int.Parse(this.tbSafetyThreshold.Text);
            int acvSize = int.Parse(this.tbACVSize.Text);
            string output = "";
            Stopwatch sw = new Stopwatch();
            
            sw.Start();
            List<Patient> safePatients = this._controller.getSafePatients_client(acvSize, safetyThreshold);

            foreach (Patient p in safePatients)
            {
                output += p.ToString() + Environment.NewLine;
            }
            sw.Stop();

            this.lblGetSafePatientsTime.Text = sw.ElapsedMilliseconds + " ms";
            this.output(output);
        }

        private void btnGetUnsafePatients_Click(object sender, EventArgs e)
        {
            int safetyThreshold = int.Parse(this.tbSafetyThreshold.Text);
            int acvSize = int.Parse(this.tbACVSize.Text);
            string output = "";
            Stopwatch sw = new Stopwatch();
            
            sw.Start();
            List<Patient> unsafePatients = this._controller.getUnsafePatients_client(acvSize, safetyThreshold);

            foreach (Patient p in unsafePatients)
            {
                output += p.ToString() + Environment.NewLine;
            }
            sw.Stop();

            this.lblGetUnsafePatientsTime.Text = sw.ElapsedMilliseconds + " ms";
            this.output(output);
        }

        private void UI_FormClosing(Object sender, FormClosingEventArgs e)
        {

            
        }

        private void UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            DbMethods.getInstance().clearTables();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.tbBrowse.Text.Length > 0)
            {
                try
                {
                    Dictionary<int, List<Visit>> data = DataLoader.GetVisitData(this.tbBrowse.Text);
                    DbMethods.getInstance().InsertVisits(data);
                    //this.udpateDataBindings();

                    this._selectedPatient.Visits = this._db.getPatientVisits(this._selectedPatient.ID);
                    this.updatePatientVisits();
                }
                catch (System.Exception ex)
                {
                	
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        
    }
}
