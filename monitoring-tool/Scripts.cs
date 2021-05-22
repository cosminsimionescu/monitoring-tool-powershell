using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring_tool
{
    public class Scripts
    {
        public string MemoryScript()//memory usage
        {
            string memS = "";
            return memS; 
        }

        public string cpuScript()//cpu usage
        {
            string cpuS = "";
            return cpuS;
        }

        public string volumeScript()//volume usage
        {
            string volS = "";   //to be improved for multiple drives
            return volS;        //maybe to chose from the main form wich drive/s to be monitored
        }

        public string procByCPU()//processes sorted by CPU usage
        {
            string volS = ""; //top 10 processes by CPU usage
            return volS;
        }

        public string procByMem()//processes sorted by Memory usage
        {
            string volS = ""; //top 10 processes by Memory usage
            return volS;
        }
    }
}
