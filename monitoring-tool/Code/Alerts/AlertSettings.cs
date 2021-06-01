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
 
            int valMemTrshld = Convert.ToInt32(memoryTrshld.Text);
            int valcpuTrshld = Convert.ToInt32(cpuTrshld.Text);
            int valvolumeTrshld = Convert.ToInt32(volumeTrshld.Text);

            checkInput.MemoryThreshold = valMemTrshld;
            checkInput.CpuThreshold = valcpuTrshld;
            checkInput.VolumeThreshold = valvolumeTrshld;

            if (checkBoxEmail.CheckState == CheckState.Checked) { checkInput.Email = txtEmail.Text;} else { }

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
                timeFormat.Text == "minutes" || timeFormat.Text == "hours" && valid != false)
            {
                SaveFile();
                MessageBox.Show("Configuration has been saved");
                Hide();
            }
            else
            {
                if (valMemTrshld <= 100 && valcpuTrshld <= 100 && valvolumeTrshld <= 100 && checkBoxEmail.CheckState == CheckState.Unchecked)
                {
                    SaveFile();

                    MessageBox.Show("Configuration has been saved");
                    Hide();
                }
                else
                {
                    MessageBox.Show("Complete all the fields");
                }
            }

        }

        public void SaveFile()
        {
            using (StreamWriter sw = new StreamWriter(saveFile.FileName))
            {

                if (checkBoxEmail.CheckState == CheckState.Checked &&
                    intervalAlertEmail.Text.Trim() != "" && txtEmail.Text.Trim() != "")
                {
                    sendEmailIsChecked = "true";
                    sw.WriteLine(sendEmailIsChecked);
                    sw.WriteLine(txtEmail.Text);
                    sw.WriteLine(intervalAlertEmail.Text);
                    sw.WriteLine(timeFormat.Text);
                }
                else
                {
                    //MessageBox.Show("Complete all the fields");
                }
                if (checkBoxEmail.CheckState == CheckState.Unchecked)
                {
                    sendEmailIsChecked = "false";
                    sw.WriteLine(sendEmailIsChecked);
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("");
                }

                sw.WriteLine(memoryTrshld.Text);
                sw.WriteLine(cpuTrshld.Text);
                sw.WriteLine(volumeTrshld.Text);

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
        }

        public void LoadConfigurationFile()
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
                    sr.Close();
                }
            }

            else
            {
                DefaultConfiguration();
            }
        }


        private void btnConfirmConf_Click(object sender, EventArgs e)
        {
            ValidateConfiguration();
        }

        private void btnCancelConf_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void checkBoxEmail_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxEmail.CheckState == CheckState.Checked)
            {
                txtEmail.Enabled = true;
                intervalAlertEmail.Enabled = true;
                timeFormat.Enabled = true;
                txtEmail.Text = "Enter e-mail address";
            }
            else
            {
                timeFormat.Text="";
                intervalAlertEmail.Text = "";
                txtEmail.Text = "";
                txtEmail.Enabled = false;
                intervalAlertEmail.Enabled = false;
                timeFormat.Enabled = false;
            }
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
        }

        private void memoryTrshld_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void CPUTrshld_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void volumeTrshld_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void intervalAlertEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) & e.KeyChar != (char)Keys.Back;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings();

            if (!InstanceServerSettings.Visible)
            {
                InstanceServerSettings.Show();
            }
            else
            {
                InstanceServerSettings.BringToFront();
            }
        }
    }
}
