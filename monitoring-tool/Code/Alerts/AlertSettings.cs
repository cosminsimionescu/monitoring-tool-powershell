using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class AlertSettings : Form
    {
        string sendEmailIsChecked;
        private static AlertSettings InstanceAlert;
        public static AlertSettings GetInstanceAlert()
        {
            if (InstanceAlert == null) InstanceAlert = new AlertSettings();
            return InstanceAlert;
        }

        public AlertSettings()
        {
            InitializeComponent();
            InstanceAlert = this;
        }

        public void ValidateConfiguration()
        {
            ValidateInput checkInput = new ValidateInput();

            int valMemTrshld = Convert.ToInt32(memoryTrshld.Text.Trim());
            int valcpuTrshld = Convert.ToInt32(cpuTrshld.Text.Trim());
            int valvolumeTrshld = Convert.ToInt32(volumeTrshld.Text.Trim());

            checkInput.MemoryThreshold = valMemTrshld;
            checkInput.CpuThreshold = valcpuTrshld;
            checkInput.VolumeThreshold = valvolumeTrshld;

            if (checkBoxEmail.CheckState == CheckState.Checked)
            {
                checkInput.Email = txtEmail.Text.Trim();
            }
            ValidationContext context = new ValidationContext(checkInput, null, null);
            List<ValidationResult> validationRes = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(checkInput, context, validationRes, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationRes)
                {
                    MessageBox.Show(validationResult.ErrorMessage);
                }
            }
            if (checkBoxEmail.CheckState == CheckState.Checked && intervalAlertEmail.Text.Trim() != "" &&
                valMemTrshld <= 100 && valcpuTrshld <= 100 && valvolumeTrshld <= 100 &&
                timeFormat.Text.Trim() == "minutes" || timeFormat.Text.Trim() == "hours" && valid != false)
            {
                SaveFile();
                MessageBox.Show("Configuration was saved");
                Hide();
            }
            else
            {
                if (valMemTrshld <= 100 && valcpuTrshld <= 100 && valvolumeTrshld <= 100 && checkBoxEmail.CheckState == CheckState.Unchecked)
                {
                    SaveFile();

                    MessageBox.Show("Configuration was saved");
                    Hide();
                }
                else
                {
                    MessageBox.Show("Complete all the fields");
                }
            }
        }

        public void SaveFile() //save config file for alerts prefferences
        {
            using (StreamWriter sw = new StreamWriter(saveFile.FileName))
            {

                if (checkBoxEmail.CheckState == CheckState.Checked &&
                    intervalAlertEmail.Text.Trim() != "" && txtEmail.Text.Trim() != "")
                {
                    sendEmailIsChecked = "true";
                    sw.WriteLine(sendEmailIsChecked);
                    sw.WriteLine(txtEmail.Text.Trim());
                    sw.WriteLine(intervalAlertEmail.Text.Trim());
                    sw.WriteLine(timeFormat.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Complete all the fields");
                }
                if (checkBoxEmail.CheckState == CheckState.Unchecked)
                {
                    sendEmailIsChecked = "false";
                    sw.WriteLine(sendEmailIsChecked);
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("");
                }

                sw.WriteLine(memoryTrshld.Text.Trim());
                sw.WriteLine(cpuTrshld.Text.Trim());
                sw.WriteLine(volumeTrshld.Text.Trim());
                sw.WriteLine(triggerTime.Text.Trim());
                sw.WriteLine(timeFormatInternalAlerts.Text.Trim());

                sw.Close();
            }
        }

        public void DefaultConfiguration()
        {
            checkBoxEmail.CheckState = CheckState.Unchecked;
            txtEmail.Text = "";
            intervalAlertEmail.Text = "";
            timeFormat.Text = "";

            memoryTrshld.Text = "85";
            cpuTrshld.Text = "85";
            volumeTrshld.Text = "10";
            triggerTime.Text = "15";
            timeFormatInternalAlerts.Text = "minutes";
        }

        public void LoadConfigurationFile() // load config file for alerts prefferences
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\AlertsConfigFile.txt"))
            {
                using (StreamReader sr = new StreamReader(loadSaveFile.FileName))
                {
                    string checkEmailSettings = sr.ReadLine();
                    if (checkEmailSettings == "true")
                    {
                        checkBoxEmail.CheckState = CheckState.Checked;
                        txtEmail.Text = sr.ReadLine();
                        intervalAlertEmail.Text = sr.ReadLine();
                        timeFormat.Text = sr.ReadLine();
                    }
                    else
                    {
                        checkBoxEmail.CheckState = CheckState.Unchecked;
                        txtEmail.Text = "";
                        intervalAlertEmail.Text = "";
                        timeFormat.Text = "";
                    }

                    memoryTrshld.Text = sr.ReadLine();
                    cpuTrshld.Text = sr.ReadLine();
                    volumeTrshld.Text = sr.ReadLine();
                    triggerTime.Text = sr.ReadLine();
                    timeFormatInternalAlerts.Text = sr.ReadLine();

                    sr.Close();
                }
            }

            else
            {
                DefaultConfiguration();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void btnConfirmConf_Click(object sender, EventArgs e)
        {
                ValidateConfiguration();
                MainForm Instance = MainForm.GetInstance();
                Instance.checkEmailAlerts.CheckState = CheckState.Checked;
                Instance.checkEmailAlerts.CheckState = CheckState.Unchecked;
                Instance.checkCpuAlert.CheckState = CheckState.Checked;
                Instance.checkCpuAlert.CheckState = CheckState.Unchecked;
                Instance.checkMemoryAlert.CheckState = CheckState.Checked;
                Instance.checkMemoryAlert.CheckState = CheckState.Unchecked;
                Instance.checkFreeSpaceAlert.CheckState = CheckState.Checked;
                Instance.checkFreeSpaceAlert.CheckState = CheckState.Unchecked;
        }

        private void btnCancelConf_Click(object sender, EventArgs e)
        {
            LoadConfigurationFile();
            Hide();
        }

        private void checkBoxEmail_CheckStateChanged(object sender, EventArgs e)
        {
            MainForm Instance = MainForm.GetInstance();

            if (checkBoxEmail.CheckState == CheckState.Checked)
            {
                txtEmail.Enabled = true;
                intervalAlertEmail.Enabled = true;
                timeFormat.Enabled = true;
                Instance.checkEmailAlerts.Enabled = true;
            }
            else
            {
                Instance.checkEmailAlerts.Enabled = false;

                timeFormat.Text = "";
                intervalAlertEmail.Text = "";
                txtEmail.Text = "";
                txtEmail.Enabled = false;
                intervalAlertEmail.Enabled = false;
                timeFormat.Enabled = false;
            }
        }

        private void memoryTrshld_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & !char.IsWhiteSpace(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void CPUTrshld_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & !char.IsWhiteSpace(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void volumeTrshld_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & !char.IsWhiteSpace(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void intervalAlertEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & !char.IsWhiteSpace(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void triggerTime_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & !char.IsWhiteSpace(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }
    }
}
