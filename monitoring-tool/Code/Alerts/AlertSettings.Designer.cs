
namespace monitoring_tool
{
    partial class AlertSettings
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
            this.memoryTrshld = new System.Windows.Forms.TextBox();
            this.cpuTrshld = new System.Windows.Forms.TextBox();
            this.volumeTrshld = new System.Windows.Forms.TextBox();
            this.btnConfirmConf = new System.Windows.Forms.Button();
            this.checkBoxEmail = new System.Windows.Forms.CheckBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timeFormat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancelConf = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.loadSaveFile = new System.Windows.Forms.OpenFileDialog();
            this.intervalAlertEmail = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.triggerTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.timeFormatInternalAlerts = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // memoryTrshld
            // 
            this.memoryTrshld.AutoCompleteCustomSource.AddRange(new string[] {
            "85"});
            this.memoryTrshld.Location = new System.Drawing.Point(175, 126);
            this.memoryTrshld.Name = "memoryTrshld";
            this.memoryTrshld.Size = new System.Drawing.Size(55, 23);
            this.memoryTrshld.TabIndex = 1;
            this.memoryTrshld.Text = "85";
            this.memoryTrshld.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.memoryTrshld_KeyPress);
            // 
            // cpuTrshld
            // 
            this.cpuTrshld.AutoCompleteCustomSource.AddRange(new string[] {
            "85"});
            this.cpuTrshld.Location = new System.Drawing.Point(175, 165);
            this.cpuTrshld.Name = "cpuTrshld";
            this.cpuTrshld.Size = new System.Drawing.Size(55, 23);
            this.cpuTrshld.TabIndex = 3;
            this.cpuTrshld.Text = "85";
            // 
            // volumeTrshld
            // 
            this.volumeTrshld.AutoCompleteCustomSource.AddRange(new string[] {
            "10"});
            this.volumeTrshld.Location = new System.Drawing.Point(175, 203);
            this.volumeTrshld.Name = "volumeTrshld";
            this.volumeTrshld.Size = new System.Drawing.Size(55, 23);
            this.volumeTrshld.TabIndex = 5;
            this.volumeTrshld.Text = "10";
            // 
            // btnConfirmConf
            // 
            this.btnConfirmConf.BackColor = System.Drawing.Color.White;
            this.btnConfirmConf.Location = new System.Drawing.Point(64, 323);
            this.btnConfirmConf.Name = "btnConfirmConf";
            this.btnConfirmConf.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmConf.TabIndex = 6;
            this.btnConfirmConf.Text = "Confirm";
            this.btnConfirmConf.UseVisualStyleBackColor = false;
            this.btnConfirmConf.Click += new System.EventHandler(this.btnConfirmConf_Click);
            // 
            // checkBoxEmail
            // 
            this.checkBoxEmail.AutoSize = true;
            this.checkBoxEmail.Location = new System.Drawing.Point(34, 254);
            this.checkBoxEmail.Name = "checkBoxEmail";
            this.checkBoxEmail.Size = new System.Drawing.Size(86, 19);
            this.checkBoxEmail.TabIndex = 7;
            this.checkBoxEmail.Text = "Email alerts";
            this.checkBoxEmail.UseVisualStyleBackColor = true;
            this.checkBoxEmail.CheckStateChanged += new System.EventHandler(this.checkBoxEmail_CheckStateChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new System.Drawing.Point(34, 279);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(152, 23);
            this.txtEmail.TabIndex = 8;
            this.txtEmail.Click += new System.EventHandler(this.txtEmail_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Memory usage(%)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = " CPU usage(%)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Free volume(%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "(0-100)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "(0-100)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "(0-100)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Frequency (minutes/hours)";
            // 
            // timeFormat
            // 
            this.timeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeFormat.Enabled = false;
            this.timeFormat.FormattingEnabled = true;
            this.timeFormat.Items.AddRange(new object[] {
            "minutes",
            "hours"});
            this.timeFormat.Location = new System.Drawing.Point(254, 279);
            this.timeFormat.MaxDropDownItems = 2;
            this.timeFormat.Name = "timeFormat";
            this.timeFormat.Size = new System.Drawing.Size(62, 23);
            this.timeFormat.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 21);
            this.label8.TabIndex = 17;
            this.label8.Text = "Configure alerts";
            // 
            // btnCancelConf
            // 
            this.btnCancelConf.BackColor = System.Drawing.Color.White;
            this.btnCancelConf.Location = new System.Drawing.Point(203, 323);
            this.btnCancelConf.Name = "btnCancelConf";
            this.btnCancelConf.Size = new System.Drawing.Size(75, 23);
            this.btnCancelConf.TabIndex = 18;
            this.btnCancelConf.Text = "Cancel";
            this.btnCancelConf.UseVisualStyleBackColor = false;
            this.btnCancelConf.Click += new System.EventHandler(this.btnCancelConf_Click);
            // 
            // saveFile
            // 
            this.saveFile.FileName = "AlertsConfigFile.txt";
            this.saveFile.InitialDirectory = "/";
            // 
            // loadSaveFile
            // 
            this.loadSaveFile.FileName = "AlertsConfigFile.txt";
            this.loadSaveFile.InitialDirectory = "/";
            // 
            // intervalAlertEmail
            // 
            this.intervalAlertEmail.Enabled = false;
            this.intervalAlertEmail.Location = new System.Drawing.Point(192, 279);
            this.intervalAlertEmail.Name = "intervalAlertEmail";
            this.intervalAlertEmail.Size = new System.Drawing.Size(56, 23);
            this.intervalAlertEmail.TabIndex = 19;
            this.intervalAlertEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.intervalAlertEmail_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 38);
            this.button1.TabIndex = 20;
            this.button1.Text = "Email server\r\n   settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // triggerTime
            // 
            this.triggerTime.Location = new System.Drawing.Point(175, 82);
            this.triggerTime.Name = "triggerTime";
            this.triggerTime.Size = new System.Drawing.Size(55, 23);
            this.triggerTime.TabIndex = 21;
            this.triggerTime.Text = "15";
            this.triggerTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.triggerTime_KeyPress_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(218, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "minutes/seconds";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 30);
            this.label10.TabIndex = 23;
            this.label10.Text = "Trigger time for alerts\r\n       inside the app";
            // 
            // timeFormatInternalAlerts
            // 
            this.timeFormatInternalAlerts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeFormatInternalAlerts.FormattingEnabled = true;
            this.timeFormatInternalAlerts.Items.AddRange(new object[] {
            "minutes",
            "seconds"});
            this.timeFormatInternalAlerts.Location = new System.Drawing.Point(236, 82);
            this.timeFormatInternalAlerts.MaxDropDownItems = 2;
            this.timeFormatInternalAlerts.Name = "timeFormatInternalAlerts";
            this.timeFormatInternalAlerts.Size = new System.Drawing.Size(62, 23);
            this.timeFormatInternalAlerts.TabIndex = 24;
            // 
            // AlertSettings
            // 
            this.AcceptButton = this.btnConfirmConf;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(195)))), ((int)(((byte)(196)))));
            this.CancelButton = this.btnCancelConf;
            this.ClientSize = new System.Drawing.Size(356, 354);
            this.ControlBox = false;
            this.Controls.Add(this.timeFormatInternalAlerts);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.triggerTime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.intervalAlertEmail);
            this.Controls.Add(this.btnCancelConf);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.timeFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.checkBoxEmail);
            this.Controls.Add(this.btnConfirmConf);
            this.Controls.Add(this.volumeTrshld);
            this.Controls.Add(this.cpuTrshld);
            this.Controls.Add(this.memoryTrshld);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AlertSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alert settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConfirmConf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancelConf;
        public System.Windows.Forms.TextBox memoryTrshld;
        public System.Windows.Forms.TextBox cpuTrshld;
        public System.Windows.Forms.TextBox volumeTrshld;
        public System.Windows.Forms.CheckBox checkBoxEmail;
        public System.Windows.Forms.TextBox txtEmail;
        public System.Windows.Forms.ComboBox timeFormat;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.OpenFileDialog loadSaveFile;
        public System.Windows.Forms.TextBox intervalAlertEmail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox triggerTime;
        public System.Windows.Forms.ComboBox timeFormatInternalAlerts;
    }
}