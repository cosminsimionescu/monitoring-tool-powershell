using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring_tool
{
    class AlertsCheck
    {

        DateTime Time = DateTime.Now; //curent date/time for alerts

        private static AlertsCheck InstanceCheck;
        public static AlertsCheck GetInstanceCheck()
        {
            if (InstanceCheck == null) InstanceCheck = new AlertsCheck();
            return InstanceCheck;
        }

        public AlertsCheck()
        {
            InstanceCheck = this;
        }

        public void AlertMemory(bool sendEmail)
        {
   
            string date = Time.ToString("F"); 

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();//Alert settings class instance
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            ParseResults InstanceResults = ParseResults.GetInstanceResults();//Output processing class instance
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();//Send email class instance

            string messageMemoryHit = "Memory usage is over the set treshold";
            string messageMemoryEmail = "High memory load";

            double memoryTreshold = Convert.ToDouble(InstanceAlert.memoryTrshld.Text);
            if (InstanceResults.memoryPercentage > memoryTreshold && sendEmail == false)  //check for memory treshold
            {
                InstanceMainForm.UpdateMemoryAlert(messageMemoryHit);
            }
            if (InstanceResults.memoryPercentage > memoryTreshold && sendEmail == true)
            {
                InstanceSendEmail.SendAlertEmail(messageMemoryEmail, messageMemoryHit, InstanceResults.memoryPercentage);
            }
        }
        public void AlertCpu(bool sendEmail)
        {
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();//Alert settings class instance
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            ParseResults InstanceResults = ParseResults.GetInstanceResults();//Output processing class instance
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();//Send email class instance

            string date = Time.ToString("F");

            string messageCPUHit = "CPU usage is over the set treshold";
            string messageCpuEmail = "High CPU load";
           
            double cpuTreshold = Convert.ToDouble(InstanceAlert.cpuTrshld.Text);

            if (InstanceResults.cpuPercentage > cpuTreshold && sendEmail == false) //check for cpu treshold
            {
                InstanceMainForm.UpdateCPUAlert(messageCPUHit);
            }
            if (InstanceResults.cpuPercentage > cpuTreshold && sendEmail == true)
            {
                InstanceSendEmail.SendAlertEmail(messageCpuEmail, messageCPUHit, InstanceResults.memoryPercentage);
            }
        }

        public void AlertFreeSpace(bool sendEmail)
        {
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();//Alert settings class instance
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            ParseResults InstanceResults = ParseResults.GetInstanceResults();//Output processing class instance
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();//Send email class instance

            string date = Time.ToString("F");

            string messageVolumeHit = "Free space is low on drive ";
            string messageVolumeEmail = "Low free space on";


            double volumeTreshold = Convert.ToDouble(InstanceAlert.volumeTrshld.Text);

            foreach (KeyValuePair<string, double> pair in InstanceResults.driveInformations) //check for free space
            {
                if (pair.Value < volumeTreshold && sendEmail == false)
                {
                    InstanceMainForm.UpdateVolumeAlert(messageVolumeHit, pair.Key, pair.Value);
                }
                if (pair.Value < volumeTreshold && sendEmail == true)
                {
                    InstanceSendEmail.SendAlertEmail(messageVolumeEmail + " " + pair.Key, messageVolumeHit + " " + pair.Key, pair.Value);
                }
            }
        }
    }
}
