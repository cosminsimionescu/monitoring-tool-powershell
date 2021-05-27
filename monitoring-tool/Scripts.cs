using System;

namespace monitoring_tool
{
    public class Scripts
    {
        public string memory_Script()//memory usage
        {
            string memS = @"";
            return memS;
        }

        public string cpu_Script()//cpu usage
        {
            string cpuS = @"";

            return cpuS;
        }

        public string processByCPU_Script()
        {
            string processesToDisplay = "10";
            var processbyCPU = @"$cores = (Get-CimInstance Win32_Processor).NumberOfLogicalProcessors
            $CPU = Get-CimInstance Win32_PerfFormattedData_PerfProc_Process | 
            Select-Object -Property Name, @{Name = 'CPU'; Expression = {'{0:n2}' -f($_.PercentProcessorTime/$cores)}},
            @{Name = 'PID'; Expression = {$_.IDProcess}} |
            Where-Object {$_.Name -notlike '*svchost*' -and $_.Name -notlike '*Idle*' -and $_.Name -notlike '*_Total*'} |
            Sort-Object -Property CPU -Descending |
            Select-Object -First #number_of_processes#
            $CPU | Format-List";
            var result_processbyCPU = processbyCPU.Replace("#number_of_processes#", processesToDisplay);
            return result_processbyCPU;
        }
        public string processByMem_Script()
        {
            string processbyMem = @"Get-Process | Sort-Object WorkingSet64 -Descending | Select-Object -first 10 Name,
            @{Name='WorkingSet';Expression={'{0:n2}' -f($_.WorkingSet64/1MB)}}, Id | Format-List";
            return processbyMem;
        }
        public string volume_Script()
        {
            string volume = @"Get-CimInstance -Class CIM_LogicalDisk | Select-Object @{Name='Size(GB)';
            Expression={'{0:n2}' -f ($_.size/1gb)}},@{Name='Free Space(GB)';Expression={'{0:n2}' -f($_.freespace/1gb)}},
            @{Name='Free (%)';Expression={'{0,1:P2}' -f(($_.freespace/1gb) / ($_.size/1gb))}}, DeviceID | Format-List";

            return volume;
        }

    }
}
