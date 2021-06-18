using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public class SendEmail
    {
        private static SendEmail InstanceSendEmail;
        public static SendEmail GetInstanceEmail()
        {
            if (InstanceSendEmail == null) InstanceSendEmail = new SendEmail();
            return InstanceSendEmail;
        }

        public SendEmail()
        {
            InstanceSendEmail = this;
        }
        
        public async void SendAlertEmail(string alertMessage, string bodyMessage, double value)
        {
            try
            {
                await Task.Run(() =>
                {

                    DateTime Time = DateTime.Now; //used for time in the e-mail

                    AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
                    MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings();

                    string emailTO = InstanceAlert.txtEmail.Text +
                    InstanceAlert.txtEmail2.Text;
                    string userName = InstanceServerSettings.txtUser.Text; //get username for the e-mail configuration from Mail Configuration window
                    string SMTP = InstanceServerSettings.txtSMTP.Text; //get SMTP server for the e-mail configuration from Mail Configuration window
                    string password = InstanceServerSettings.txtPassword.Text; //get password for the e-mail configuration from Mail Configuration window

                    try
                    {
                        string date = Time.ToString();
                        string valueMetric = value.ToString();
                        MainForm InstanceMainForm = MainForm.GetInstance();

                        string serverHit = InstanceMainForm.targetServer.Text; //take name of the server for e-mail from the MainForm TextBox

                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient(SMTP);
                        mail.From = new MailAddress(userName, "MONITORING TOOL - LICENTA");
                        mail.To.Add(emailTO);
                        mail.Subject = "[ALERT]" + "[" + date + "] " + alertMessage + "   TRIGGERED ON " + " " + serverHit;
                        mail.Body = "SERVER: " + serverHit + Environment.NewLine + "ALERT MESSAGE: " + bodyMessage + Environment.NewLine + "CURENT STATUS: " + value + "%" + " AT " + date;

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(userName, password); // credentials
                        SmtpServer.EnableSsl = true; //enable SSL

                        SmtpServer.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                });
            }
            catch { };
        }

    }
}
