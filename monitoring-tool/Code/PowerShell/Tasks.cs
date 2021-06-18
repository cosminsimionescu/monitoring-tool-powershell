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
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            string memoryUsage_script = InstanceScripts.memory_Script(); //Memory usage script 
            string targetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_Mem = InstanceRemoteSession.NewPowerShell(targetServ, memoryUsage_script); //task running the PS session

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
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            string processCPU_script = InstanceScripts.processByCPU_Script(); //Procesess by CPU script
            string TargetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_ProcByCPU = InstanceRemoteSession.NewPowerShell(TargetServ, processCPU_script); //task running the PS session
            string resultTaskProcessCPU = task_ProcByCPU.Result;//forwarding the PowerShell output to the list

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
            MainForm InstanceMainForm = MainForm.GetInstance(); //MainForm class instance
            RemoteSession InstanceRemoteSession = RemoteSession.GetInstanceRemoteSession();//RemoteSession class instance
            Scripts InstanceScripts = Scripts.GetInstanceScripts();//Scripts class instance

            string volumeUsage_script = InstanceScripts.volume_Script();//volume script
            string targetServ = InstanceMainForm.targetServer.Text.Trim();//server for new PS session

            Task<string> task_Vol = InstanceRemoteSession.NewPowerShell(targetServ, volumeUsage_script); //task running the PS session
            string resultTaskVolume = task_Vol.Result;

            InstanceResults.ResultVolume(resultTaskVolume);
        }
    }
}
