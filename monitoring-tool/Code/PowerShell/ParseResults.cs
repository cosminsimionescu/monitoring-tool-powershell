using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public double memoryPercentage { get; set; }
        public double cpuPercentage { get; set; }

        public void GetMemory()
        {
            InstanceResults = this;

            MainForm InstanceMainForm = MainForm.GetInstance();
            RemoteSession newSession = new RemoteSession();
            Scripts memS = new Scripts();
            string memoryUsage_script = memS.memory_Script(); //Memory usage script 

            Task<string> task_Mem = newSession.NewPsSession(InstanceMainForm.targetServer.Text.Trim(), memoryUsage_script);

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
            RemoteSession newSession = new RemoteSession();
            Scripts cpuS = new Scripts();
            string cpuUsage_script = cpuS.cpu_Script(); //CPU usage script

            Task<string> task_CPU = newSession.NewPsSession(InstanceMainForm.targetServer.Text.Trim(), cpuUsage_script);

            string cpuUsage = " " + task_CPU.Result + "%";
            InstanceMainForm.UpdateCpuLoad(cpuUsage);
            try
            {
                cpuPercentage = Convert.ToDouble(task_CPU.Result);
            }
            catch { }
        }



    }
}
