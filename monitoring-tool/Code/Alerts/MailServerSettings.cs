using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MailServerSettings : Form
    {

        private static MailServerSettings InstanceConfigSMTP;
        public static MailServerSettings GetInstanceServerSettings()
        {
            if (InstanceConfigSMTP == null) InstanceConfigSMTP = new MailServerSettings();
            return InstanceConfigSMTP;
        }

        string userName, SMTP, password;
        string configurationSavedPrev;

        public MailServerSettings()
        {
            InitializeComponent();
            InstanceConfigSMTP = this;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveFile();
  
        }

        private void btnCancel_Click(object sender, EventArgs e) // Button for canceling the config
        {
            Hide();
            if (File.Exists(Directory.GetCurrentDirectory() + @"\configSMTP.txt")) { LoadConfigurationSMPTFile(); } 
            else
            {
                txtUser.Text = "";
                txtPassword.Text = "";
                txtSMTP.Text = "";
            }
        }

        public void SaveFile() //saving user configuration on the settings window
        {

            using (StreamWriter sw = new StreamWriter(saveConfigSMTP.FileName))
            {

                userName = txtUser.Text;
                password = txtPassword.Text;
                SMTP = txtSMTP.Text;

                if (txtSMTP.Text.Trim() != "" && txtUser.Text.Trim() != "" &&
                    txtPassword.Text.Trim() != "")
                {
                    configurationSavedPrev = "true";
                    sw.WriteLine(configurationSavedPrev);
                    sw.WriteLine(userName);
                    sw.WriteLine(password);
                    sw.WriteLine(SMTP);

                    MessageBox.Show("Configuration was saved");
                    Hide();
                }
                else
                {
                    configurationSavedPrev = "false";
                    sw.WriteLine(configurationSavedPrev);
                    MessageBox.Show("Complete all the fields");
                }
                sw.Close();
            }
        }

        public void LoadConfigurationSMPTFile() //loading user configuration on the settings window
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\configSMTP.txt"))
            {
                using (StreamReader sr = new StreamReader(openConfigSMTP.FileName))
                {
                    userName = txtUser.Text;
                    password = txtPassword.Text;
                    SMTP = txtSMTP.Text;

                    configurationSavedPrev = sr.ReadLine();
                    if (configurationSavedPrev == "true")
                    {
                        txtUser.Text = sr.ReadLine();
                        txtPassword.Text = sr.ReadLine();
                        txtSMTP.Text = sr.ReadLine();
                    }
                    else
                    {
                        txtUser.Text = "";
                        txtPassword.Text = "";
                        txtSMTP.Text = "";
                    }
                    sr.Close();
                }
            }
        }
    }
}
