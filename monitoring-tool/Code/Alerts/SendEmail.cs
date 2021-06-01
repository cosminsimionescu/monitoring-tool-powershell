using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace monitoring_tool
{
    public class SendEmail
    {
        public void Email(string alertMessage)
        {
            DateTime Time = DateTime.Now;

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();

            string emailTO = InstanceAlert.txtEmail.Text;

            MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings();
            
            string userName = InstanceServerSettings.txtUser.Text;
            string SMTP = InstanceServerSettings.txtSMTP.Text;
            string password = InstanceServerSettings.txtPassword.Text;

            try
            {
                string date = Time.ToString();

                MainForm InstanceMainForm = MainForm.GetInstance();
                string serverHit = InstanceMainForm.targetServer.Text;

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTP);
                mail.From = new MailAddress(userName);
                mail.To.Add(emailTO);
                mail.Subject = "[ALERT]" + "[" + date + "] " + alertMessage +" "+"triggered on "+" "+ serverHit;
                mail.Body = " ";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(userName, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
