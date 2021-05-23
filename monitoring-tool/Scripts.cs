﻿namespace monitoring_tool
{
    public class Scripts
    {
        public string MemoryScript()//memory usage
        {
            string memS = @"";
            return memS;
        }

        public string cpuScript()//cpu usage
        {
            string cpuS = "";

            return cpuS;
        }

        public string volumeScript()//volume usage
        {
            string volS = @"Get-CimInstance -Class CIM_LogicalDisk | Select-Object @{Name='Free(%)';
            Expression ={'{0,1:P0}' -f(($_.freespace / 1gb) / ($_.size / 1gb))}},  DeviceID | Format-List";
            return volS;        //maybe to chose from the main form wich drive/s to be monitored             //to be improved for multiple drives
        }

        public string procByCPU()//processes sorted by CPU usage
        {
            string volS = @""; //top 10 processes by CPU usage
            return volS;
        }

        public string procByMem()//processes sorted by Memory usage
        {
            string volS = @""; //top 10 processes by Memory usage
            return volS;
        }
    }
}
