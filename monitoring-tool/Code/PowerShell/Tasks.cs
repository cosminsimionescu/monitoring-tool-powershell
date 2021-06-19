using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring_tool
{
    class Tasks
    {
        private static Tasks InstanceTask;
        public static Tasks GetInstanceTask()
        {
            if (InstanceTask == null) InstanceTask = new Tasks();
            return InstanceTask;
        }

        public Tasks()
        {
            InstanceTask = this;
        }

        public void GetMemory()
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();
            Scripts InstanceScripts = Scripts.GetInstanceScripts();

            string memoryUsage_script = InstanceScripts.memory_Script();
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_Mem = InstanceRemoteSession.NewPowerShell(targetServ, memoryUsage_script);
            string taskResultMemory = task_Mem.Result;

            InstanceResults.ResultsMemory(taskResultMemory);
        }

        public void GetCPU()
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();
            Scripts InstanceScripts = Scripts.GetInstanceScripts();

            string cpuUsage_script = InstanceScripts.cpu_Script();
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_CPU = InstanceRemoteSession.NewPowerShell(targetServ, cpuUsage_script);
            string taskResultCPU = task_CPU.Result;

            InstanceResults.ResultsCPU(taskResultCPU);
        }

        public void GetProcessCPU()
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();
            Scripts InstanceScripts = Scripts.GetInstanceScripts();

            string processCPU_script = InstanceScripts.processByCPU_Script();
            string TargetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_ProcByCPU = InstanceRemoteSession.NewPowerShell(TargetServ, processCPU_script);
            string resultTaskProcessCPU = task_ProcByCPU.Result;

            InstanceResults.ResultProcessCPU(resultTaskProcessCPU);
        }

        public void GetProcessMemory()
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            MainForm InstanceMainForm = MainForm.GetInstance(); 
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();
            Scripts InstanceScripts = Scripts.GetInstanceScripts();

            string processMem_script = InstanceScripts.processByMem_Script();
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_ProcByMem = InstanceRemoteSession.NewPowerShell(targetServ, processMem_script); 
            string resultTaskProcessMemory = task_ProcByMem.Result; 

            InstanceResults.ResultProcessMemory(resultTaskProcessMemory);
        }

        public void GetVolume()
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();
            Scripts InstanceScripts = Scripts.GetInstanceScripts();

            string volumeUsage_script = InstanceScripts.volume_Script();
            string targetServ = InstanceMainForm.targetServer.Text.Trim();

            Task<string> task_Vol = InstanceRemoteSession.NewPowerShell(targetServ, volumeUsage_script);
            string resultTaskVolume = task_Vol.Result;

            InstanceResults.ResultVolume(resultTaskVolume);
        }

        internal RemoteSession RemoteSession
        {
            get => default;
            set
            {
            }
        }

        internal ParseResults ParseResults
        {
            get => default;
            set
            {
            }
        }
    }
}
