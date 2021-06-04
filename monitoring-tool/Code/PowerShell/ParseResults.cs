using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        string cpuUsage_script;

        public void GetMemory()
        {

            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            string memoryUsage_script = InstanceScripts.memory_Script(); //Memory usage script 
            string targetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_Mem = InstanceRemoteSession.NewPowerShell(targetServ, memoryUsage_script); //task running the PS session

            string memoryUsage = " " + task_Mem.Result + "%";
            Thread.Sleep(35);
            InstanceMainForm.UpdateMemoryLoad(memoryUsage); //call method for updating the DataGrid on MainForm

            Thread.Sleep(35);
            memoryPercentage = Convert.ToDouble(task_Mem.Result); //Convert to double for later using the value for Alerting

        }

        public void GetCPU()
        {
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            cpuUsage_script = InstanceScripts.cpu_Script("30");

            string targetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_CPU = InstanceRemoteSession.NewPowerShell(targetServ, cpuUsage_script); //task running the PS session

            string cpuUsage = " " + task_CPU.Result + "%"; //forwarding the PowerShell output to the method for updating on the MainForm the CPU usage
            Thread.Sleep(35);
            InstanceMainForm.UpdateCpuLoad(cpuUsage); //call method for updating the DataGrid on MainForm

            Thread.Sleep(35);
            cpuPercentage = Convert.ToDouble(task_CPU.Result); //Convert to double for later using the value for Alerting
        }

        public void GetProcessCPU()
        {

            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            double processCpuLoadValue;
            string processCPU_script = InstanceScripts.processByCPU_Script("10"); //Procesess by CPU script
            string TargetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_ProcByCPU = InstanceRemoteSession.NewPowerShell(TargetServ, processCPU_script); //task running the PS session

            string outputProcCPU = task_ProcByCPU.Result;//forwarding the PowerShell output to the list

            List<string> corectLines = outputProcCPU.Split('\n') //creating a list with the PowerShell output for formating
                         .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3) //formating the PowerShell output
            {
                string processNameCPU = corectLines[i].Split(':')[1].Trim().Split('#')[0];
                string cpuLoad = corectLines[i + 1].Split(':')[1].Trim();
                string pid_cpu = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];

                Thread.Sleep(35);
                if (i == 0)
                {
                    InstanceMainForm.ClearGridProcessCpuLoad(); //call for clearing the DataGrid on MainForm before writing again
                }
                Thread.Sleep(35);
                processCpuLoadValue = Convert.ToDouble(cpuLoad);//Convert to double for later using the value for Alerting
                InstanceMainForm.UpdateProcessCpuLoad(pid_cpu, processNameCPU, processCpuLoadValue); //call method for updating the DataGrid on MainForm
            }
        }

        public void GetProcessMemory()
        {

            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            string processMem_script = InstanceScripts.processByMem_Script("10"); //Process by memory script
            string targetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_ProcByMem = InstanceRemoteSession.NewPowerShell(targetServ, processMem_script); //task running the PS session

            string outputProcMem = task_ProcByMem.Result; //forwarding the PowerShell output to the list

            List<string> corectLines = outputProcMem.Split('\n')//creating a list with the PowerShell output for formating
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

        public void GetVolume()
        {

            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            string volumeUsage_script = InstanceScripts.volume_Script();//volume script
            string targetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            driveInformations = new Dictionary<string, double>();
            Task<string> task_Vol = InstanceRemoteSession.NewPowerShell(targetServ, volumeUsage_script); //task running the PS session

            string outputVol = task_Vol.Result;

            List<string> corectLines = outputVol.Split('\n')  //creating a list with the PowerShell output for formating
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
                driveInformations.Add(driveId, driveSpacePercentageValue);

            }


        }
    }
}
