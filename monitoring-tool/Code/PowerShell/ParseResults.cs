using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    class ParseResults
    {

        private static ParseResults InstanceResults;
        public static ParseResults GetInstanceResults()
        {
            if (InstanceResults == null) InstanceResults = new ParseResults();
            return InstanceResults;
        }

        public ParseResults()
        {
            InstanceResults = this;
        }

        public double memoryPercentage { get; set; }
        public double cpuPercentage { get; set; }
        public Dictionary<string, double> driveInformations { get; set; } //dictionary for Drive name and freespace(%)



        public void ResultsMemory(string taskResultMemory)
        {
            string memoryUsage = taskResultMemory;
            MainForm InstanceMainForm = MainForm.GetInstance();

            Thread.Sleep(35);
            InstanceMainForm.UpdateMemoryLoad(" "+ memoryUsage + "%");
            
            Thread.Sleep(35);
            try {
            memoryPercentage = Convert.ToDouble(memoryUsage);
            }
            catch
            {
            }
        }

        public void ResultsCPU(string taskResultCPU)
        {
            string cpuUsage = taskResultCPU;
            MainForm InstanceMainForm = MainForm.GetInstance();

            Thread.Sleep(35);
            try {
            cpuPercentage = Convert.ToDouble(cpuUsage);
            }
            catch
            {
                MessageBox.Show(@"Check the name or if the server is running" + Environment.NewLine + 
                    Environment.NewLine  + "Server entered cannot be monitored");

                InstanceMainForm.ResetApp();
            }
            Thread.Sleep(35);
            InstanceMainForm.UpdateCpuLoad(" "+ cpuUsage + "%");
        }

        public void ResultProcessCPU(string resultTaskProcessCPU)
        {
            MainForm InstanceMainForm = MainForm.GetInstance(); 
            double processCpuLoadValue;
            List<string> corectLines = resultTaskProcessCPU.Split('\n') 
                         .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                string processNameCPU = corectLines[i].Split(':')[1].Trim().Split('#')[0];
                string cpuLoad = corectLines[i + 1].Split(':')[1].Trim();
                string pid_cpu = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];

                Thread.Sleep(35);
                if (i == 0)
                {
                    InstanceMainForm.ClearGridProcessCpuLoad(); 
                }
                Thread.Sleep(35);
                processCpuLoadValue = Convert.ToDouble(cpuLoad);
                InstanceMainForm.UpdateProcessCpuLoad(pid_cpu, processNameCPU, processCpuLoadValue); 
            }
        }


        public void ResultProcessMemory(string resultTaskProcessMemory)
        {
            MainForm InstanceMainForm = MainForm.GetInstance();

            List<string> corectLines = resultTaskProcessMemory.Split('\n')//creating a list with the PowerShell output for formating
                        .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3) //formating the PowerShell output
            {
                string processName_mem = corectLines[i].Split(':')[1].Trim();
                string processMem_load = corectLines[i + 1].Split(':')[1].Trim();
                string processPidMem = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
                Thread.Sleep(35);
                if (i == 0)
                {
                    InstanceMainForm.ClearGridProcessMemLoad(); //call for clearing the DataGrid on MainForm before writing again
                }

                Thread.Sleep(35);
                double processMemLoadValue = Convert.ToDouble(processMem_load);//Convert to double for later using the value for Alerting
                InstanceMainForm.UpdateProcessMemLoad(processPidMem, processName_mem, processMemLoadValue); //call method for updating the DataGrid on MainForm
            }
        }

        public void ResultVolume(string resultTaskVolume)
        {
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            
            List<string> corectLines = resultTaskVolume.Split('\n')  //creating a list with the PowerShell output for formating
                         .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 4) //formating the PowerShell output
            {
                string driveId = corectLines[i + 3].Split(':')[1].Trim();
                string driveSize = corectLines[i].Split(':')[1].Trim();
                string driveSpace = corectLines[i + 1].Split(':')[1].Trim();
                string driveSpacePercentage = corectLines[i + 2].Split(':')[1].Trim().Split('\r', '%')[0];

                if (i == 0)
                {
                    InstanceMainForm.ClearGridUpdateFreeSpace(); //call for clearing the DataGrid on MainForm before writing again
                }

                double driveSpacePercentageValue = Convert.ToDouble(driveSpacePercentage); //Convert to double for later using the value for Alerting
                InstanceMainForm.UpdateFreeSpace(driveId, driveSize, driveSpace, driveSpacePercentageValue); //call method for updating the DataGrid on MainForm

                driveInformations = new Dictionary<string, double>();
                driveInformations.Add(driveId, driveSpacePercentageValue);
            }
        }
    }
}
