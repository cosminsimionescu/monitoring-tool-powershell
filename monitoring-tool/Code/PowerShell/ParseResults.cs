using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Dictionary<string, double> driveInformations { get; set; }

        public void GetMemory()
        {
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();
            MainForm InstanceMainForm = MainForm.GetInstance();

            Scripts memS = new Scripts();
            string memoryUsage_script = memS.memory_Script(); //Memory usage script 
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_Mem = InstanceRemoteSession.NewPowerShell(targetServ, memoryUsage_script);

            string memoryUsage = " " + task_Mem.Result + "%";

            InstanceMainForm.UpdateMemoryLoad(memoryUsage);

            try
            {
                memoryPercentage = Convert.ToDouble(task_Mem.Result);
            }
            catch { }

        }

        public void GetCPU()
        {

            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();

            Scripts cpuS = new Scripts();
            string cpuUsage_script = cpuS.cpu_Script(); //CPU usage script
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_CPU = InstanceRemoteSession.NewPowerShell(targetServ, cpuUsage_script);

            string cpuUsage = " " + task_CPU.Result + "%";
            Thread.Sleep(3);
            InstanceMainForm.UpdateCpuLoad(cpuUsage);
            
            try
            {
                cpuPercentage = Convert.ToDouble(task_CPU.Result);
            }
            catch { }
        }

        public void GetProcessCPU()
        {
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();

            double processCpuLoadValue;
            Scripts procCPU = new Scripts();
            string processCPU_script = procCPU.processByCPU_Script("10"); //Procesess by CPU script
            string TargetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_ProcByCPU = InstanceRemoteSession.NewPowerShell(TargetServ, processCPU_script);
            string outputProcCPU = task_ProcByCPU.Result;
            List<string> corectLines = outputProcCPU.Split('\n')
                         .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                string processNameCPU = corectLines[i].Split(':')[1].Trim().Split('#')[0];
                string cpuLoad = corectLines[i + 1].Split(':')[1].Trim();
                string pid_cpu = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];

                processCpuLoadValue = Convert.ToDouble(cpuLoad);
                Thread.Sleep(3);
                if (i == 0)
                {
                    InstanceMainForm.ClearGridProcessCpuLoad();
                }
                try
                {
                    InstanceMainForm.UpdateProcessCpuLoad(pid_cpu, processNameCPU, processCpuLoadValue);
                }
                catch { }
            }
        }

        public void GetProcessMemory()
        {
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();

            Scripts procMem = new Scripts();
            string processMem_script = procMem.processByMem_Script("10"); //Process by memory script
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_ProcByMem = InstanceRemoteSession.NewPowerShell(targetServ, processMem_script);

            string outputProcMem = task_ProcByMem.Result;
            List<string> corectLines = outputProcMem.Split('\n')
                         .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                string processName_mem = corectLines[i].Split(':')[1].Trim();
                string processMem_load = corectLines[i + 1].Split(':')[1].Trim();
                string processPidMem = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
                Thread.Sleep(3);
                if (i == 0)
                {
                    InstanceMainForm.ClearGridProcessMemLoad();
                }
                try
                {
                    double processMemLoadValue = Convert.ToDouble(processMem_load);
                    InstanceMainForm.UpdateProcessMemLoad(processPidMem, processName_mem, processMemLoadValue);
                }
                catch { }
            }
        }

        public void GetVolume()
        {
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();

            Scripts volumeS = new Scripts();
            string volumeUsage_script = volumeS.volume_Script();//volume script
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            driveInformations = new Dictionary<string, double>();
            Task<string> task_Vol = InstanceRemoteSession.NewPowerShell(targetServ, volumeUsage_script);

            string outputVol = task_Vol.Result;

            List<string> corectLines = outputVol.Split('\n')
                         .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 4)
            {
                string driveId = corectLines[i + 3].Split(':')[1].Trim();
                string driveSize = corectLines[i].Split(':')[1].Trim();
                string driveSpace = corectLines[i + 1].Split(':')[1].Trim();
                string driveSpacePercentage = corectLines[i + 2].Split(':')[1].Trim().Split('\r', '%')[0];
                if (i == 0)
                {
                    InstanceMainForm.ClearGridUpdateFreeSpace();
                }
                try
                {
                    double driveSpacePercentageValue = Convert.ToDouble(driveSpacePercentage);
                    InstanceMainForm.UpdateFreeSpace(driveId, driveSize, driveSpace, driveSpacePercentageValue);
                    driveInformations.Add(driveId, driveSpacePercentageValue);
                }
                catch { }
            }
        }
    }
}
