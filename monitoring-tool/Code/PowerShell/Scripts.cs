using System;
using System.Linq;
using System.Management;
using System.Threading;

namespace monitoring_tool
{
    public class Scripts
    {

        private static Scripts InstanceScripts;
        public static Scripts GetInstanceScripts()
        {
            if (InstanceScripts == null) InstanceScripts = new Scripts();
            return InstanceScripts;
        }

        public Scripts()
        {
            InstanceScripts = this;
        }

        public string memory_Script() //show memory usage in MB
        {
            string memS = @"$ComputerMemory = Get-CimInstance -Class win32_operatingsystem -ErrorAction Stop
            $Memory = ((($ComputerMemory.TotalVisibleMemorySize - $ComputerMemory.FreePhysicalMemory)*100)/ $ComputerMemory.TotalVisibleMemorySize)
            $RoundMemory = [math]::Round($Memory, 2)
            $RoundMemory";

            return memS;
        }

        public string cpu_Script() //show CPU usage time(%)
        {
            string sampleTime = "30";
            string cpuS = @"$CPU = (Get-Counter '\Processor(_total)\% Processor Time' -Sample #sampletime#).CounterSamples[0].CookedValue
            $RoundCPU = [math]::Round($CPU, 2)
            $RoundCPU";
            var cpu_script = cpuS.Replace("#sampletime#", sampleTime);

            return cpu_script;
        }

        public string processByCPU_Script(string processesToDisplay_CPU) //display top processes by CPU
        {
            processesToDisplay_CPU = "10";
            var processbyCPU = @"$cores = (Get-CimInstance Win32_Processor).NumberOfLogicalProcessors/2
            $CPU = Get-CimInstance Win32_PerfFormattedData_PerfProc_Process | 
            Select-Object -Property Name, @{Name = 'CPU'; Expression = {'{0:n2}' -f($_.PercentProcessorTime/$cores)}},
            @{Name = 'PID'; Expression = {$_.IDProcess}} |
            Where-Object {$_.Name -notlike '*conhost*' -and $_.Name -notlike '*svchost*' -and $_.Name -notlike '*Idle*' -and $_.Name -notlike '*_Total*'} |
            Sort-Object -Property CPU -Descending |
            Select-Object -First 10 
            $CPU | Format-List";
            var result_processbyCPU = processbyCPU.Replace("#number_of_processes#", processesToDisplay_CPU);

            return result_processbyCPU;
        }

        public string processByMem_Script(string processesToDisplay_Mem) //display top processes by memory
        {
            processesToDisplay_Mem = "10";
            string processbyMem = @"Get-Process | Sort-Object WorkingSet64 -Descending | Select-Object -First #number_of_processes# Name,
            @{Name='WorkingSet';Expression={'{0:n2}' -f($_.WorkingSet64/1MB)}}, Id | Format-List";
            var result_processbyMem = processbyMem.Replace("#number_of_processes#", processesToDisplay_Mem);

            return result_processbyMem;
        }

        public string volume_Script() // display drives from the checked system (Free space %, Free space in GB, Capacity in GB )
        {
            Thread.Sleep(200);
            string volume = @"Get-CimInstance -Class CIM_LogicalDisk | Select-Object @{Name='Size(GB)';
            Expression={'{0:n2}' -f ($_.size/1gb)}},@{Name='Free Space(GB)';Expression={'{0:n2}' -f($_.freespace/1gb)}},
            @{Name='Free (%)';Expression={'{0,1:P2}' -f(($_.freespace/1gb) / ($_.size/1gb))}}, DeviceID | Format-List";

            return volume;
        }

    }
}
