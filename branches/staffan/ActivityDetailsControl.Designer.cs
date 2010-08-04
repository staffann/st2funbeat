
namespace Janohl.ST2Funbeat
{
#if !ST_2_1
    partial class ActivityDetailsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ExportedCheckBox1 = new System.Windows.Forms.CheckBox();
            this.InputGroupBox = new System.Windows.Forms.GroupBox();
            this.SetsInputTextBox = new System.Windows.Forms.TextBox();
            this.RepInputTextBox = new System.Windows.Forms.TextBox();
            this.TEInputTextBox = new System.Windows.Forms.TextBox();
            this.SetLabel = new System.Windows.Forms.Label();
            this.RepLabel = new System.Windows.Forms.Label();
            this.RPETextBox = new System.Windows.Forms.TextBox();
            this.TELabel = new System.Windows.Forms.Label();
            this.RPEComboBox = new System.Windows.Forms.ComboBox();
            this.StatusGroupBox1 = new System.Windows.Forms.GroupBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.UserInputTabPage = new System.Windows.Forms.TabPage();
            this.ExportPreviewTabPage = new System.Windows.Forms.TabPage();
            this.ExportGroupBox = new System.Windows.Forms.GroupBox();
            this.StartTimeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GPSCheckBox = new System.Windows.Forms.CheckBox();
            this.HRCheckBox = new System.Windows.Forms.CheckBox();
            this.AltitudeCheckBox = new System.Windows.Forms.CheckBox();
            this.DistanceCheckBox = new System.Windows.Forms.CheckBox();
            this.PrivCommentTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SetsTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.RepetitionsTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.RPEExportTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TETextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CadenceTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CaloriesTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.HRMaxTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.HRAvgTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DistanceTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DurationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DateTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusGroupBox2 = new System.Windows.Forms.GroupBox();
            this.ExportedCheckBox2 = new System.Windows.Forms.CheckBox();
            this.InputGroupBox.SuspendLayout();
            this.StatusGroupBox1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.UserInputTabPage.SuspendLayout();
            this.ExportPreviewTabPage.SuspendLayout();
            this.ExportGroupBox.SuspendLayout();
            this.StatusGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportedCheckBox1
            // 
            this.ExportedCheckBox1.AutoSize = true;
            this.ExportedCheckBox1.Enabled = false;
            this.ExportedCheckBox1.Location = new System.Drawing.Point(10, 19);
            this.ExportedCheckBox1.Name = "ExportedCheckBox1";
            this.ExportedCheckBox1.Size = new System.Drawing.Size(122, 17);
            this.ExportedCheckBox1.TabIndex = 0;
            this.ExportedCheckBox1.Text = "Exported to Funbeat";
            this.ExportedCheckBox1.UseVisualStyleBackColor = true;
            // 
            // InputGroupBox
            // 
            this.InputGroupBox.Controls.Add(this.SetsInputTextBox);
            this.InputGroupBox.Controls.Add(this.RepInputTextBox);
            this.InputGroupBox.Controls.Add(this.TEInputTextBox);
            this.InputGroupBox.Controls.Add(this.SetLabel);
            this.InputGroupBox.Controls.Add(this.RepLabel);
            this.InputGroupBox.Controls.Add(this.RPETextBox);
            this.InputGroupBox.Controls.Add(this.TELabel);
            this.InputGroupBox.Controls.Add(this.RPEComboBox);
            this.InputGroupBox.Location = new System.Drawing.Point(3, 77);
            this.InputGroupBox.Name = "InputGroupBox";
            this.InputGroupBox.Size = new System.Drawing.Size(583, 167);
            this.InputGroupBox.TabIndex = 2;
            this.InputGroupBox.TabStop = false;
            this.InputGroupBox.Text = "User Input - for info used by Funbeat that otherwise isn\'t in ST";
            // 
            // SetsInputTextBox
            // 
            this.SetsInputTextBox.Location = new System.Drawing.Point(190, 136);
            this.SetsInputTextBox.Name = "SetsInputTextBox";
            this.SetsInputTextBox.Size = new System.Drawing.Size(75, 20);
            this.SetsInputTextBox.TabIndex = 12;
            this.SetsInputTextBox.Leave += new System.EventHandler(this.NewUserInput);
            // 
            // RepInputTextBox
            // 
            this.RepInputTextBox.Location = new System.Drawing.Point(190, 99);
            this.RepInputTextBox.Name = "RepInputTextBox";
            this.RepInputTextBox.Size = new System.Drawing.Size(75, 20);
            this.RepInputTextBox.TabIndex = 11;
            this.RepInputTextBox.Leave += new System.EventHandler(this.NewUserInput);
            // 
            // TEInputTextBox
            // 
            this.TEInputTextBox.Location = new System.Drawing.Point(190, 62);
            this.TEInputTextBox.Name = "TEInputTextBox";
            this.TEInputTextBox.Size = new System.Drawing.Size(75, 20);
            this.TEInputTextBox.TabIndex = 7;
            this.TEInputTextBox.Leave += new System.EventHandler(this.NewUserInput);
            // 
            // SetLabel
            // 
            this.SetLabel.AutoSize = true;
            this.SetLabel.Location = new System.Drawing.Point(7, 139);
            this.SetLabel.Name = "SetLabel";
            this.SetLabel.Size = new System.Drawing.Size(28, 13);
            this.SetLabel.TabIndex = 10;
            this.SetLabel.Text = "Sets";
            // 
            // RepLabel
            // 
            this.RepLabel.AutoSize = true;
            this.RepLabel.Location = new System.Drawing.Point(7, 102);
            this.RepLabel.Name = "RepLabel";
            this.RepLabel.Size = new System.Drawing.Size(60, 13);
            this.RepLabel.TabIndex = 9;
            this.RepLabel.Text = "Repetitions";
            // 
            // RPETextBox
            // 
            this.RPETextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.RPETextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RPETextBox.Location = new System.Drawing.Point(10, 23);
            this.RPETextBox.Multiline = true;
            this.RPETextBox.Name = "RPETextBox";
            this.RPETextBox.ReadOnly = true;
            this.RPETextBox.Size = new System.Drawing.Size(174, 33);
            this.RPETextBox.TabIndex = 0;
            this.RPETextBox.Text = "Intensity (Rated Percieved Exertion), Borg Scale";
            // 
            // TELabel
            // 
            this.TELabel.AutoSize = true;
            this.TELabel.Location = new System.Drawing.Point(6, 64);
            this.TELabel.Name = "TELabel";
            this.TELabel.Size = new System.Drawing.Size(119, 13);
            this.TELabel.TabIndex = 6;
            this.TELabel.Text = "Training Effect (Suunto)";
            // 
            // RPEComboBox
            // 
            this.RPEComboBox.FormattingEnabled = true;
            this.RPEComboBox.Items.AddRange(new object[] {
            "",
            "6 - No exertion at all",
            "7 - Extremely light",
            "8 - ",
            "9 - Very light",
            "10 -",
            "11 - Light",
            "12 - ",
            "13 - Somewhat hard",
            "14 - ",
            "15 - Hard (heavy)",
            "16 - ",
            "17 - Very hard",
            "18 -",
            "19 - Extremely hard",
            "20 - Maximal exertion"});
            this.RPEComboBox.Location = new System.Drawing.Point(190, 23);
            this.RPEComboBox.Name = "RPEComboBox";
            this.RPEComboBox.Size = new System.Drawing.Size(175, 21);
            this.RPEComboBox.TabIndex = 3;
            this.RPEComboBox.Leave += new System.EventHandler(this.NewUserInput);
            // 
            // StatusGroupBox1
            // 
            this.StatusGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.StatusGroupBox1.Controls.Add(this.ExportedCheckBox1);
            this.StatusGroupBox1.Location = new System.Drawing.Point(3, 6);
            this.StatusGroupBox1.Name = "StatusGroupBox1";
            this.StatusGroupBox1.Size = new System.Drawing.Size(583, 65);
            this.StatusGroupBox1.TabIndex = 6;
            this.StatusGroupBox1.TabStop = false;
            this.StatusGroupBox1.Text = "Export Status";
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.UserInputTabPage);
            this.TabControl.Controls.Add(this.ExportPreviewTabPage);
            this.TabControl.Location = new System.Drawing.Point(3, 3);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(600, 366);
            this.TabControl.TabIndex = 8;
            // 
            // UserInputTabPage
            // 
            this.UserInputTabPage.BackColor = System.Drawing.Color.Transparent;
            this.UserInputTabPage.Controls.Add(this.StatusGroupBox1);
            this.UserInputTabPage.Controls.Add(this.InputGroupBox);
            this.UserInputTabPage.Location = new System.Drawing.Point(4, 22);
            this.UserInputTabPage.Name = "UserInputTabPage";
            this.UserInputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.UserInputTabPage.Size = new System.Drawing.Size(592, 340);
            this.UserInputTabPage.TabIndex = 0;
            this.UserInputTabPage.Text = "User Input";
            // 
            // ExportPreviewTabPage
            // 
            this.ExportPreviewTabPage.BackColor = System.Drawing.Color.Transparent;
            this.ExportPreviewTabPage.Controls.Add(this.ExportGroupBox);
            this.ExportPreviewTabPage.Controls.Add(this.StatusGroupBox2);
            this.ExportPreviewTabPage.Location = new System.Drawing.Point(4, 22);
            this.ExportPreviewTabPage.Name = "ExportPreviewTabPage";
            this.ExportPreviewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ExportPreviewTabPage.Size = new System.Drawing.Size(592, 340);
            this.ExportPreviewTabPage.TabIndex = 1;
            this.ExportPreviewTabPage.Text = "Export Preview";
            // 
            // ExportGroupBox
            // 
            this.ExportGroupBox.Controls.Add(this.StartTimeTextBox);
            this.ExportGroupBox.Controls.Add(this.label4);
            this.ExportGroupBox.Controls.Add(this.GPSCheckBox);
            this.ExportGroupBox.Controls.Add(this.HRCheckBox);
            this.ExportGroupBox.Controls.Add(this.AltitudeCheckBox);
            this.ExportGroupBox.Controls.Add(this.DistanceCheckBox);
            this.ExportGroupBox.Controls.Add(this.PrivCommentTextBox);
            this.ExportGroupBox.Controls.Add(this.label14);
            this.ExportGroupBox.Controls.Add(this.CommentTextBox);
            this.ExportGroupBox.Controls.Add(this.label13);
            this.ExportGroupBox.Controls.Add(this.SetsTextBox);
            this.ExportGroupBox.Controls.Add(this.label12);
            this.ExportGroupBox.Controls.Add(this.RepetitionsTextBox);
            this.ExportGroupBox.Controls.Add(this.label11);
            this.ExportGroupBox.Controls.Add(this.RPEExportTextBox);
            this.ExportGroupBox.Controls.Add(this.label10);
            this.ExportGroupBox.Controls.Add(this.TETextBox);
            this.ExportGroupBox.Controls.Add(this.label9);
            this.ExportGroupBox.Controls.Add(this.CadenceTextBox);
            this.ExportGroupBox.Controls.Add(this.label8);
            this.ExportGroupBox.Controls.Add(this.CaloriesTextBox);
            this.ExportGroupBox.Controls.Add(this.label7);
            this.ExportGroupBox.Controls.Add(this.HRMaxTextBox);
            this.ExportGroupBox.Controls.Add(this.label6);
            this.ExportGroupBox.Controls.Add(this.HRAvgTextBox);
            this.ExportGroupBox.Controls.Add(this.label5);
            this.ExportGroupBox.Controls.Add(this.DistanceTextBox);
            this.ExportGroupBox.Controls.Add(this.label3);
            this.ExportGroupBox.Controls.Add(this.DurationTextBox);
            this.ExportGroupBox.Controls.Add(this.label2);
            this.ExportGroupBox.Controls.Add(this.DateTextBox);
            this.ExportGroupBox.Controls.Add(this.label1);
            this.ExportGroupBox.Location = new System.Drawing.Point(3, 77);
            this.ExportGroupBox.Name = "ExportGroupBox";
            this.ExportGroupBox.Size = new System.Drawing.Size(583, 257);
            this.ExportGroupBox.TabIndex = 8;
            this.ExportGroupBox.TabStop = false;
            this.ExportGroupBox.Text = "Export Info Preview";
            // 
            // StartTimeTextBox
            // 
            this.StartTimeTextBox.Location = new System.Drawing.Point(91, 44);
            this.StartTimeTextBox.Name = "StartTimeTextBox";
            this.StartTimeTextBox.ReadOnly = true;
            this.StartTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.StartTimeTextBox.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Start time:";
            // 
            // GPSCheckBox
            // 
            this.GPSCheckBox.AutoSize = true;
            this.GPSCheckBox.Enabled = false;
            this.GPSCheckBox.Location = new System.Drawing.Point(406, 98);
            this.GPSCheckBox.Name = "GPSCheckBox";
            this.GPSCheckBox.Size = new System.Drawing.Size(109, 17);
            this.GPSCheckBox.TabIndex = 31;
            this.GPSCheckBox.Text = "GPS Coord. track";
            this.GPSCheckBox.UseVisualStyleBackColor = true;
            // 
            // HRCheckBox
            // 
            this.HRCheckBox.AutoSize = true;
            this.HRCheckBox.Enabled = false;
            this.HRCheckBox.Location = new System.Drawing.Point(406, 72);
            this.HRCheckBox.Name = "HRCheckBox";
            this.HRCheckBox.Size = new System.Drawing.Size(69, 17);
            this.HRCheckBox.TabIndex = 30;
            this.HRCheckBox.Text = "HR track";
            this.HRCheckBox.UseVisualStyleBackColor = true;
            // 
            // AltitudeCheckBox
            // 
            this.AltitudeCheckBox.AutoSize = true;
            this.AltitudeCheckBox.Enabled = false;
            this.AltitudeCheckBox.Location = new System.Drawing.Point(406, 46);
            this.AltitudeCheckBox.Name = "AltitudeCheckBox";
            this.AltitudeCheckBox.Size = new System.Drawing.Size(88, 17);
            this.AltitudeCheckBox.TabIndex = 29;
            this.AltitudeCheckBox.Text = "Altitude track";
            this.AltitudeCheckBox.UseVisualStyleBackColor = true;
            // 
            // DistanceCheckBox
            // 
            this.DistanceCheckBox.AutoSize = true;
            this.DistanceCheckBox.Enabled = false;
            this.DistanceCheckBox.Location = new System.Drawing.Point(406, 20);
            this.DistanceCheckBox.Name = "DistanceCheckBox";
            this.DistanceCheckBox.Size = new System.Drawing.Size(95, 17);
            this.DistanceCheckBox.TabIndex = 28;
            this.DistanceCheckBox.Text = "Distance track";
            this.DistanceCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrivCommentTextBox
            // 
            this.PrivCommentTextBox.Location = new System.Drawing.Point(393, 144);
            this.PrivCommentTextBox.Multiline = true;
            this.PrivCommentTextBox.Name = "PrivCommentTextBox";
            this.PrivCommentTextBox.ReadOnly = true;
            this.PrivCommentTextBox.Size = new System.Drawing.Size(184, 107);
            this.PrivCommentTextBox.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(390, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Priv comment:";
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Location = new System.Drawing.Point(207, 144);
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.ReadOnly = true;
            this.CommentTextBox.Size = new System.Drawing.Size(178, 107);
            this.CommentTextBox.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(204, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Comment:";
            // 
            // SetsTextBox
            // 
            this.SetsTextBox.Location = new System.Drawing.Point(285, 96);
            this.SetsTextBox.Name = "SetsTextBox";
            this.SetsTextBox.ReadOnly = true;
            this.SetsTextBox.Size = new System.Drawing.Size(100, 20);
            this.SetsTextBox.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(204, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Sets:";
            // 
            // RepetitionsTextBox
            // 
            this.RepetitionsTextBox.Location = new System.Drawing.Point(285, 70);
            this.RepetitionsTextBox.Name = "RepetitionsTextBox";
            this.RepetitionsTextBox.ReadOnly = true;
            this.RepetitionsTextBox.Size = new System.Drawing.Size(100, 20);
            this.RepetitionsTextBox.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(204, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Repetitions:";
            // 
            // RPEExportTextBox
            // 
            this.RPEExportTextBox.Location = new System.Drawing.Point(285, 44);
            this.RPEExportTextBox.Name = "RPEExportTextBox";
            this.RPEExportTextBox.ReadOnly = true;
            this.RPEExportTextBox.Size = new System.Drawing.Size(100, 20);
            this.RPEExportTextBox.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(204, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Intensity (Borg):";
            // 
            // TETextBox
            // 
            this.TETextBox.Location = new System.Drawing.Point(285, 18);
            this.TETextBox.Name = "TETextBox";
            this.TETextBox.ReadOnly = true;
            this.TETextBox.Size = new System.Drawing.Size(100, 20);
            this.TETextBox.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(204, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "TE:";
            // 
            // CadenceTextBox
            // 
            this.CadenceTextBox.Location = new System.Drawing.Point(91, 200);
            this.CadenceTextBox.Name = "CadenceTextBox";
            this.CadenceTextBox.ReadOnly = true;
            this.CadenceTextBox.Size = new System.Drawing.Size(100, 20);
            this.CadenceTextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Cadence (avg):";
            // 
            // CaloriesTextBox
            // 
            this.CaloriesTextBox.Location = new System.Drawing.Point(91, 174);
            this.CaloriesTextBox.Name = "CaloriesTextBox";
            this.CaloriesTextBox.ReadOnly = true;
            this.CaloriesTextBox.Size = new System.Drawing.Size(100, 20);
            this.CaloriesTextBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Calories:";
            // 
            // HRMaxTextBox
            // 
            this.HRMaxTextBox.Location = new System.Drawing.Point(91, 148);
            this.HRMaxTextBox.Name = "HRMaxTextBox";
            this.HRMaxTextBox.ReadOnly = true;
            this.HRMaxTextBox.Size = new System.Drawing.Size(100, 20);
            this.HRMaxTextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "HR (max):";
            // 
            // HRAvgTextBox
            // 
            this.HRAvgTextBox.Location = new System.Drawing.Point(91, 122);
            this.HRAvgTextBox.Name = "HRAvgTextBox";
            this.HRAvgTextBox.ReadOnly = true;
            this.HRAvgTextBox.Size = new System.Drawing.Size(100, 20);
            this.HRAvgTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "HR (avg):";
            // 
            // DistanceTextBox
            // 
            this.DistanceTextBox.Location = new System.Drawing.Point(91, 96);
            this.DistanceTextBox.Name = "DistanceTextBox";
            this.DistanceTextBox.ReadOnly = true;
            this.DistanceTextBox.Size = new System.Drawing.Size(100, 20);
            this.DistanceTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Distance:";
            // 
            // DurationTextBox
            // 
            this.DurationTextBox.Location = new System.Drawing.Point(91, 70);
            this.DurationTextBox.Name = "DurationTextBox";
            this.DurationTextBox.ReadOnly = true;
            this.DurationTextBox.Size = new System.Drawing.Size(100, 20);
            this.DurationTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Duration:";
            // 
            // DateTextBox
            // 
            this.DateTextBox.Location = new System.Drawing.Point(91, 18);
            this.DateTextBox.Name = "DateTextBox";
            this.DateTextBox.ReadOnly = true;
            this.DateTextBox.Size = new System.Drawing.Size(100, 20);
            this.DateTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // StatusGroupBox2
            // 
            this.StatusGroupBox2.Controls.Add(this.ExportedCheckBox2);
            this.StatusGroupBox2.Location = new System.Drawing.Point(3, 6);
            this.StatusGroupBox2.Name = "StatusGroupBox2";
            this.StatusGroupBox2.Size = new System.Drawing.Size(583, 65);
            this.StatusGroupBox2.TabIndex = 7;
            this.StatusGroupBox2.TabStop = false;
            this.StatusGroupBox2.Text = "Export Status";
            // 
            // ExportedCheckBox2
            // 
            this.ExportedCheckBox2.AutoSize = true;
            this.ExportedCheckBox2.Enabled = false;
            this.ExportedCheckBox2.Location = new System.Drawing.Point(10, 19);
            this.ExportedCheckBox2.Name = "ExportedCheckBox2";
            this.ExportedCheckBox2.Size = new System.Drawing.Size(122, 17);
            this.ExportedCheckBox2.TabIndex = 0;
            this.ExportedCheckBox2.Text = "Exported to Funbeat";
            this.ExportedCheckBox2.UseVisualStyleBackColor = true;
            // 
            // ActivityDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.TabControl);
            this.Name = "ActivityDetailsControl";
            this.Size = new System.Drawing.Size(606, 372);
            this.InputGroupBox.ResumeLayout(false);
            this.InputGroupBox.PerformLayout();
            this.StatusGroupBox1.ResumeLayout(false);
            this.StatusGroupBox1.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.UserInputTabPage.ResumeLayout(false);
            this.ExportPreviewTabPage.ResumeLayout(false);
            this.ExportGroupBox.ResumeLayout(false);
            this.ExportGroupBox.PerformLayout();
            this.StatusGroupBox2.ResumeLayout(false);
            this.StatusGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ExportedCheckBox1;
        private System.Windows.Forms.GroupBox InputGroupBox;
        private System.Windows.Forms.ComboBox RPEComboBox;
        private System.Windows.Forms.Label TELabel;
        private System.Windows.Forms.GroupBox StatusGroupBox1;
        private System.Windows.Forms.TextBox RPETextBox;
        private System.Windows.Forms.Label SetLabel;
        private System.Windows.Forms.Label RepLabel;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage UserInputTabPage;
        private System.Windows.Forms.TabPage ExportPreviewTabPage;
        private System.Windows.Forms.GroupBox ExportGroupBox;
        private System.Windows.Forms.GroupBox StatusGroupBox2;
        private System.Windows.Forms.CheckBox ExportedCheckBox2;
        private System.Windows.Forms.TextBox DateTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HRMaxTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox HRAvgTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DistanceTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DurationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CaloriesTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CadenceTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TETextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox RPEExportTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox SetsTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox RepetitionsTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox PrivCommentTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox CommentTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox HRCheckBox;
        private System.Windows.Forms.CheckBox AltitudeCheckBox;
        private System.Windows.Forms.CheckBox DistanceCheckBox;
        private System.Windows.Forms.CheckBox GPSCheckBox;
        private System.Windows.Forms.TextBox StartTimeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TEInputTextBox;
        private System.Windows.Forms.TextBox RepInputTextBox;
        private System.Windows.Forms.TextBox SetsInputTextBox;
    }
#endif
}
