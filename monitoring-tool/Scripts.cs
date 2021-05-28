using System;
using System.Linq;
using System.Management;

namespace monitoring_tool
{
    public class Scripts
    {
        public string memory_Script()
        {
            string memS = @"$ComputerMemory = Get-CimInstance -Class win32_operatingsystem -ErrorAction Stop
            $Memory = ((($ComputerMemory.TotalVisibleMemorySize - $ComputerMemory.FreePhysicalMemory)*100)/ $ComputerMemory.TotalVisibleMemorySize)
            $RoundMemory = [math]::Round($Memory, 2)
            $RoundMemory";
            return memS;
        }

        public string cpu_Script()
        {
            string sampleTime = "60";
            string cpuS = @"$CPU = (Get-Counter '\Processor(_total)\% Processor Time' -Sample 60).CounterSamples[0].CookedValue
            $RoundCPU = [math]::Round($CPU, 2)
            $RoundCPU";
            var cpu_script = cpuS.Replace("#number_of_processes#", sampleTime);
            return cpu_script;
        }

        public string processByCPU_Script()
        {
            string processesToDisplay_CPU = "10";
            var processbyCPU = @"$cores = (Get-CimInstance Win32_Processor).NumberOfLogicalProcessors
            $CPU = Get-CimInstance Win32_PerfFormattedData_PerfProc_Process | 
            Select-Object -Property Name, @{Name = 'CPU'; Expression = {'{0:n2}' -f($_.PercentProcessorTime/$cores)}},
            @{Name = 'PID'; Expression = {$_.IDProcess}} |
            Where-Object {$_.Name -notlike '*svchost*' -and $_.Name -notlike '*Idle*' -and $_.Name -notlike '*_Total*'} |
            Sort-Object -Property CPU -Descending |
            Select-Object -First #number_of_processes#
            $CPU | Format-List";
            var result_processbyCPU = processbyCPU.Replace("#number_of_processes#", processesToDisplay_CPU);
            return result_processbyCPU;
        }

        public string processByMem_Script()
        {
            string processesToDisplay_Mem = "10";
            string processbyMem = @"Get-Process | Sort-Object WorkingSet64 -Descending | Select-Object -First #number_of_processes# Name,
            @{Name='WorkingSet';Expression={'{0:n2}' -f($_.WorkingSet64/1MB)}}, Id | Format-List";
            var result_processbyMem = processbyMem.Replace("#number_of_processes#", processesToDisplay_Mem);
            return result_processbyMem;
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
