using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring_tool
{
    class AlertsCheck
    {
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

        public void Alerts()
        {

            DateTime Time = DateTime.Now; //curent date/time for alerts
            string date = Time.ToString("F"); 

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();//Alert settings class instance
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            ParseResults InstanceResults = ParseResults.GetInstanceResults();//Output processing class instance
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();//Send email class instance

            double memoryTreshold = Convert.ToDouble(InstanceAlert.memoryTrshld.Text);
            double cpuTreshold = Convert.ToDouble(InstanceAlert.cpuTrshld.Text);
            double volumeTreshold = Convert.ToDouble(InstanceAlert.volumeTrshld.Text);

            string messageMemoryHit = "Memory usage is over the set treshold";
            string messageCPUHit = "CPU usage is over the set treshold";
            string messageVolumeHit = "Free space is low on drive ";

            string messageMemoryEmail = "High memory load";
            string messageCpuEmail = "High CPU load";
            string messageVolumeEmail = "Low free space on";


            if (InstanceResults.memoryPercentage > memoryTreshold)  //check for memory treshold
            {
                InstanceMainForm.UpdateMemoryAlert(messageMemoryHit);
                InstanceSendEmail.SendAlertEmail(messageMemoryEmail, messageMemoryHit, InstanceResults.memoryPercentage);
            }

            if (InstanceResults.cpuPercentage > cpuTreshold) //check for cpu treshold
            {
                InstanceMainForm.UpdateCPUAlert(messageCPUHit);
                InstanceSendEmail.SendAlertEmail(messageCpuEmail, messageCPUHit, InstanceResults.cpuPercentage);
            }

            foreach (KeyValuePair<string, double> pair in InstanceResults.driveInformations) //check for free space
            {
                if (pair.Value < volumeTreshold)
                {
                    InstanceMainForm.UpdateVolumeAlert(messageVolumeHit, pair.Key, pair.Value);
                    InstanceSendEmail.SendAlertEmail(messageVolumeEmail + " "+ pair.Key, messageVolumeHit + " " + pair.Key, pair.Value);
                }
            }
        }
    }
}
