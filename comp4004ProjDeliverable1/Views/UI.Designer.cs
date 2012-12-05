namespace comp4004ProjDeliverable1
{
    partial class UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateScanario1 = new System.Windows.Forms.Button();
            this.btnGenerateScenario2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBrowse = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listPatients = new System.Windows.Forms.ListBox();
            this.listPatientVisits = new System.Windows.Forms.ListBox();
            this.tmpGenerationButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listRationaleAddVisit = new System.Windows.Forms.ListBox();
            this.btnAddVisitpatient = new System.Windows.Forms.Button();
            this.calendarAddVisit = new System.Windows.Forms.MonthCalendar();
            this.label7 = new System.Windows.Forms.Label();
            this.tbACVSize = new System.Windows.Forms.TextBox();
            this.btnGenerateACVs = new System.Windows.Forms.Button();
            this.btnShowACVs = new System.Windows.Forms.Button();
            this.lblACVTime = new System.Windows.Forms.Label();
            this.btnCheckSafety = new System.Windows.Forms.Button();
            this.lblPatientSafety = new System.Windows.Forms.Label();
            this.lblSafetyCheckTime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listCM = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAddToCM = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblMatchCMTime = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnGetSafePatients = new System.Windows.Forms.Button();
            this.btnGetUnsafePatients = new System.Windows.Forms.Button();
            this.tbSafetyThreshold = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblGetSafePatientsTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblGetUnsafePatientsTime = new System.Windows.Forms.Label();
            this.tbPatientSafetyPercentage = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data generation";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnGenerateScanario1
            // 
            this.btnGenerateScanario1.Location = new System.Drawing.Point(26, 55);
            this.btnGenerateScanario1.Name = "btnGenerateScanario1";
            this.btnGenerateScanario1.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateScanario1.TabIndex = 1;
            this.btnGenerateScanario1.Text = "Scenario 1";
            this.btnGenerateScanario1.UseVisualStyleBackColor = true;
            this.btnGenerateScanario1.Click += new System.EventHandler(this.btnGenerateScanario1_Click);
            // 
            // btnGenerateScenario2
            // 
            this.btnGenerateScenario2.Location = new System.Drawing.Point(26, 84);
            this.btnGenerateScenario2.Name = "btnGenerateScenario2";
            this.btnGenerateScenario2.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateScenario2.TabIndex = 2;
            this.btnGenerateScenario2.Text = "Scenario 2";
            this.btnGenerateScenario2.UseVisualStyleBackColor = true;
            this.btnGenerateScenario2.Click += new System.EventHandler(this.btnGenerateScenario2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(762, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Generate 10,000 random data for patients: the first thousand should have 20 visit" +
    "s, the next thousand 30, the next 40, up to 110 visits for patients 9001 to 1000" +
    "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(717, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Generate 1,000 random data for patients: the first hundred should have 20 visits," +
    " the next hundred 25, the next 30, up to 65 for patients 901 up to 1000";
            // 
            // tbBrowse
            // 
            this.tbBrowse.Location = new System.Drawing.Point(133, 116);
            this.tbBrowse.Name = "tbBrowse";
            this.tbBrowse.Size = new System.Drawing.Size(282, 20);
            this.tbBrowse.TabIndex = 7;
            this.tbBrowse.Text = "Data/VisitDataTest.txt";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(436, 113);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Generate";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Patients";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Patient visits";
            // 
            // listPatients
            // 
            this.listPatients.FormattingEnabled = true;
            this.listPatients.Location = new System.Drawing.Point(12, 223);
            this.listPatients.Name = "listPatients";
            this.listPatients.Size = new System.Drawing.Size(152, 628);
            this.listPatients.TabIndex = 12;
            this.listPatients.SelectedIndexChanged += new System.EventHandler(this.listPatients_SelectedIndexChanged);
            // 
            // listPatientVisits
            // 
            this.listPatientVisits.FormattingEnabled = true;
            this.listPatientVisits.Location = new System.Drawing.Point(170, 223);
            this.listPatientVisits.Name = "listPatientVisits";
            this.listPatientVisits.Size = new System.Drawing.Size(147, 381);
            this.listPatientVisits.TabIndex = 13;
            // 
            // tmpGenerationButton
            // 
            this.tmpGenerationButton.Location = new System.Drawing.Point(26, 116);
            this.tmpGenerationButton.Name = "tmpGenerationButton";
            this.tmpGenerationButton.Size = new System.Drawing.Size(75, 23);
            this.tmpGenerationButton.TabIndex = 14;
            this.tmpGenerationButton.Text = "TEMP";
            this.tmpGenerationButton.UseVisualStyleBackColor = true;
            this.tmpGenerationButton.Click += new System.EventHandler(this.tmpGenerationButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 668);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(406, 668);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Rationale";
            // 
            // listRationaleAddVisit
            // 
            this.listRationaleAddVisit.FormattingEnabled = true;
            this.listRationaleAddVisit.Location = new System.Drawing.Point(409, 690);
            this.listRationaleAddVisit.Name = "listRationaleAddVisit";
            this.listRationaleAddVisit.Size = new System.Drawing.Size(120, 160);
            this.listRationaleAddVisit.TabIndex = 21;
            // 
            // btnAddVisitpatient
            // 
            this.btnAddVisitpatient.Location = new System.Drawing.Point(176, 624);
            this.btnAddVisitpatient.Name = "btnAddVisitpatient";
            this.btnAddVisitpatient.Size = new System.Drawing.Size(120, 24);
            this.btnAddVisitpatient.TabIndex = 22;
            this.btnAddVisitpatient.Text = "Add visit to patient";
            this.btnAddVisitpatient.UseVisualStyleBackColor = true;
            this.btnAddVisitpatient.Click += new System.EventHandler(this.btnAddVisit_Click);
            // 
            // calendarAddVisit
            // 
            this.calendarAddVisit.Location = new System.Drawing.Point(170, 690);
            this.calendarAddVisit.Name = "calendarAddVisit";
            this.calendarAddVisit.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(557, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "ACV size :";
            // 
            // tbACVSize
            // 
            this.tbACVSize.Location = new System.Drawing.Point(618, 220);
            this.tbACVSize.Name = "tbACVSize";
            this.tbACVSize.Size = new System.Drawing.Size(30, 20);
            this.tbACVSize.TabIndex = 25;
            this.tbACVSize.Text = "3";
            // 
            // btnGenerateACVs
            // 
            this.btnGenerateACVs.Location = new System.Drawing.Point(560, 246);
            this.btnGenerateACVs.Name = "btnGenerateACVs";
            this.btnGenerateACVs.Size = new System.Drawing.Size(88, 23);
            this.btnGenerateACVs.TabIndex = 26;
            this.btnGenerateACVs.Text = "Generate ACVs";
            this.btnGenerateACVs.UseVisualStyleBackColor = true;
            this.btnGenerateACVs.Click += new System.EventHandler(this.btnGenerateACVs_Click);
            // 
            // btnShowACVs
            // 
            this.btnShowACVs.Location = new System.Drawing.Point(560, 275);
            this.btnShowACVs.Name = "btnShowACVs";
            this.btnShowACVs.Size = new System.Drawing.Size(88, 23);
            this.btnShowACVs.TabIndex = 27;
            this.btnShowACVs.Text = "Show ACVs";
            this.btnShowACVs.UseVisualStyleBackColor = true;
            this.btnShowACVs.Click += new System.EventHandler(this.btnShowACVs_Click);
            // 
            // lblACVTime
            // 
            this.lblACVTime.AutoSize = true;
            this.lblACVTime.Location = new System.Drawing.Point(654, 251);
            this.lblACVTime.Name = "lblACVTime";
            this.lblACVTime.Size = new System.Drawing.Size(0, 13);
            this.lblACVTime.TabIndex = 29;
            // 
            // btnCheckSafety
            // 
            this.btnCheckSafety.Location = new System.Drawing.Point(560, 304);
            this.btnCheckSafety.Name = "btnCheckSafety";
            this.btnCheckSafety.Size = new System.Drawing.Size(119, 23);
            this.btnCheckSafety.TabIndex = 31;
            this.btnCheckSafety.Text = "Check patient safety";
            this.btnCheckSafety.UseVisualStyleBackColor = true;
            this.btnCheckSafety.Click += new System.EventHandler(this.btnCheckSafety_Click);
            // 
            // lblPatientSafety
            // 
            this.lblPatientSafety.AutoSize = true;
            this.lblPatientSafety.Location = new System.Drawing.Point(640, 334);
            this.lblPatientSafety.Name = "lblPatientSafety";
            this.lblPatientSafety.Size = new System.Drawing.Size(0, 13);
            this.lblPatientSafety.TabIndex = 32;
            // 
            // lblSafetyCheckTime
            // 
            this.lblSafetyCheckTime.AutoSize = true;
            this.lblSafetyCheckTime.Location = new System.Drawing.Point(685, 309);
            this.lblSafetyCheckTime.Name = "lblSafetyCheckTime";
            this.lblSafetyCheckTime.Size = new System.Drawing.Size(0, 13);
            this.lblSafetyCheckTime.TabIndex = 34;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 624);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 22);
            this.button1.TabIndex = 35;
            this.button1.Text = "Add visit to CM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listCM
            // 
            this.listCM.FormattingEnabled = true;
            this.listCM.Location = new System.Drawing.Point(382, 223);
            this.listCM.Name = "listCM";
            this.listCM.Size = new System.Drawing.Size(153, 381);
            this.listCM.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(379, 207);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 37;
            this.label13.Text = "CM";
            // 
            // btnAddToCM
            // 
            this.btnAddToCM.Location = new System.Drawing.Point(333, 283);
            this.btnAddToCM.Name = "btnAddToCM";
            this.btnAddToCM.Size = new System.Drawing.Size(33, 310);
            this.btnAddToCM.TabIndex = 38;
            this.btnAddToCM.Text = ">";
            this.btnAddToCM.UseVisualStyleBackColor = true;
            this.btnAddToCM.Click += new System.EventHandler(this.btnAddToCM_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(559, 460);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 39;
            this.button2.Text = "Match CM";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblMatchCMTime
            // 
            this.lblMatchCMTime.AutoSize = true;
            this.lblMatchCMTime.Location = new System.Drawing.Point(653, 507);
            this.lblMatchCMTime.Name = "lblMatchCMTime";
            this.lblMatchCMTime.Size = new System.Drawing.Size(0, 13);
            this.lblMatchCMTime.TabIndex = 41;
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(692, 220);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(425, 628);
            this.tbOutput.TabIndex = 42;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(689, 204);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "Output";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(232, 610);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 44;
            this.label16.Text = "^";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(453, 610);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 13);
            this.label17.TabIndex = 45;
            this.label17.Text = "^";
            // 
            // btnGetSafePatients
            // 
            this.btnGetSafePatients.Location = new System.Drawing.Point(560, 372);
            this.btnGetSafePatients.Name = "btnGetSafePatients";
            this.btnGetSafePatients.Size = new System.Drawing.Size(119, 23);
            this.btnGetSafePatients.TabIndex = 46;
            this.btnGetSafePatients.Text = "Get safe patients";
            this.btnGetSafePatients.UseVisualStyleBackColor = true;
            this.btnGetSafePatients.Click += new System.EventHandler(this.btnGetSafePatients_Click);
            // 
            // btnGetUnsafePatients
            // 
            this.btnGetUnsafePatients.Location = new System.Drawing.Point(560, 401);
            this.btnGetUnsafePatients.Name = "btnGetUnsafePatients";
            this.btnGetUnsafePatients.Size = new System.Drawing.Size(119, 23);
            this.btnGetUnsafePatients.TabIndex = 47;
            this.btnGetUnsafePatients.Text = "Get unsafe patients";
            this.btnGetUnsafePatients.UseVisualStyleBackColor = true;
            this.btnGetUnsafePatients.Click += new System.EventHandler(this.btnGetUnsafePatients_Click);
            // 
            // tbSafetyThreshold
            // 
            this.tbSafetyThreshold.Location = new System.Drawing.Point(644, 350);
            this.tbSafetyThreshold.Name = "tbSafetyThreshold";
            this.tbSafetyThreshold.Size = new System.Drawing.Size(30, 20);
            this.tbSafetyThreshold.TabIndex = 49;
            this.tbSafetyThreshold.Text = "4";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(559, 353);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(83, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Safety threshold";
            // 
            // lblGetSafePatientsTime
            // 
            this.lblGetSafePatientsTime.AutoSize = true;
            this.lblGetSafePatientsTime.Location = new System.Drawing.Point(686, 412);
            this.lblGetSafePatientsTime.Name = "lblGetSafePatientsTime";
            this.lblGetSafePatientsTime.Size = new System.Drawing.Size(0, 13);
            this.lblGetSafePatientsTime.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(622, 381);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 51;
            // 
            // lblGetUnsafePatientsTime
            // 
            this.lblGetUnsafePatientsTime.AutoSize = true;
            this.lblGetUnsafePatientsTime.Location = new System.Drawing.Point(685, 441);
            this.lblGetUnsafePatientsTime.Name = "lblGetUnsafePatientsTime";
            this.lblGetUnsafePatientsTime.Size = new System.Drawing.Size(0, 13);
            this.lblGetUnsafePatientsTime.TabIndex = 52;
            // 
            // tbPatientSafetyPercentage
            // 
            this.tbPatientSafetyPercentage.Location = new System.Drawing.Point(170, 151);
            this.tbPatientSafetyPercentage.Name = "tbPatientSafetyPercentage";
            this.tbPatientSafetyPercentage.Size = new System.Drawing.Size(40, 20);
            this.tbPatientSafetyPercentage.TabIndex = 53;
            this.tbPatientSafetyPercentage.Text = "10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 13);
            this.label12.TabIndex = 54;
            this.label12.Text = "Safe patients percentage";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(216, 154);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 13);
            this.label19.TabIndex = 55;
            this.label19.Text = "%";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 913);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbPatientSafetyPercentage);
            this.Controls.Add(this.lblGetUnsafePatientsTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblGetSafePatientsTime);
            this.Controls.Add(this.tbSafetyThreshold);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnGetUnsafePatients);
            this.Controls.Add(this.btnGetSafePatients);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.lblMatchCMTime);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAddToCM);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.listCM);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblSafetyCheckTime);
            this.Controls.Add(this.lblPatientSafety);
            this.Controls.Add(this.btnCheckSafety);
            this.Controls.Add(this.lblACVTime);
            this.Controls.Add(this.btnShowACVs);
            this.Controls.Add(this.btnGenerateACVs);
            this.Controls.Add(this.tbACVSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.calendarAddVisit);
            this.Controls.Add(this.btnAddVisitpatient);
            this.Controls.Add(this.listRationaleAddVisit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tmpGenerationButton);
            this.Controls.Add(this.listPatientVisits);
            this.Controls.Add(this.listPatients);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tbBrowse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerateScenario2);
            this.Controls.Add(this.btnGenerateScanario1);
            this.Controls.Add(this.label1);
            this.Name = "UI";
            this.Text = "Comp4004";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UI_FormClosed);
            this.Load += new System.EventHandler(this.UI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateScanario1;
        private System.Windows.Forms.Button btnGenerateScenario2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBrowse;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listPatients;
        private System.Windows.Forms.ListBox listPatientVisits;
        private System.Windows.Forms.Button tmpGenerationButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listRationaleAddVisit;
        private System.Windows.Forms.Button btnAddVisitpatient;
        private System.Windows.Forms.MonthCalendar calendarAddVisit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbACVSize;
        private System.Windows.Forms.Button btnGenerateACVs;
        private System.Windows.Forms.Button btnShowACVs;
        private System.Windows.Forms.Label lblACVTime;
        private System.Windows.Forms.Button btnCheckSafety;
        private System.Windows.Forms.Label lblPatientSafety;
        private System.Windows.Forms.Label lblSafetyCheckTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listCM;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAddToCM;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblMatchCMTime;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnGetSafePatients;
        private System.Windows.Forms.Button btnGetUnsafePatients;
        private System.Windows.Forms.TextBox tbSafetyThreshold;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblGetSafePatientsTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblGetUnsafePatientsTime;
        private System.Windows.Forms.TextBox tbPatientSafetyPercentage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label19;
    }
}