using System;
using System.Collections.Generic;

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

        public void AlertMemory(bool sendEmail)
        {

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();//Alert settings class instance
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            ParseResults InstanceResults = ParseResults.GetInstanceResults();//Output processing class instance
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();//Send email class instance

            string messageMemoryHit = "MEMORY USAGE IS OVER THE SET THRESHOLD";
            string messageMemoryEmail = "HIGH MEMORY LOAD";

            double memoryTreshold = Convert.ToDouble(InstanceAlert.memoryTrshld.Text);
            if (InstanceResults.memoryPercentage > memoryTreshold && sendEmail == false)
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
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            MainForm InstanceMainForm = MainForm.GetInstance();
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();

            string messageCPUHit = "CPU USAGE IS OVER THE SET TRESHOLD";
            string messageCpuEmail = "HIGH CPU LOAD";
           
            double cpuTreshold = Convert.ToDouble(InstanceAlert.cpuTrshld.Text);

            if (InstanceResults.cpuPercentage > cpuTreshold && sendEmail == false)
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
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            MainForm InstanceMainForm = MainForm.GetInstance();
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            SendEmail InstanceSendEmail = SendEmail.GetInstanceEmail();

            string messageVolumeHit = "FREE SPACE IS LOW ON DRIVE ";
            string messageVolumeEmail = "LOW FREE SPACE ON";

            double volumeTreshold = Convert.ToDouble(InstanceAlert.volumeTrshld.Text);

            foreach (KeyValuePair<string, double> pair in InstanceResults.driveInformations)
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
