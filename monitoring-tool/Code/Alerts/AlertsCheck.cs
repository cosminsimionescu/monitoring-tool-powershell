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

            DateTime Time = DateTime.Now;
            string date = Time.ToString("F");

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            MainForm InstanceMainForm = MainForm.GetInstance();
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            double memoryTreshold = Convert.ToDouble(InstanceAlert.memoryTrshld.Text);
            double cpuTreshold = Convert.ToDouble(InstanceAlert.cpuTrshld.Text);
            double volumeTreshold = Convert.ToDouble(InstanceAlert.volumeTrshld.Text);

            string messageMemoryHit = "Memory usage is over the set treshold";
            string messageCPUHit = "CPU usage is over the set treshold";
            string messageVolumeHit = "Free space is low on drive ";

            if (InstanceResults.memoryPercentage > memoryTreshold)
            {
                InstanceMainForm.UpdateMemoryAlert(messageMemoryHit);
            }

            if (InstanceResults.cpuPercentage > cpuTreshold)
            {
                InstanceMainForm.UpdateCPUAlert(messageCPUHit);
            }

            foreach (KeyValuePair<string, double> pair in InstanceResults.driveInformations)
            {
                if (pair.Value < volumeTreshold)
                {
                    InstanceMainForm.UpdateVolumeAlert(messageVolumeHit, pair.Key, pair.Value);
                }
            }
        }
    }
}
