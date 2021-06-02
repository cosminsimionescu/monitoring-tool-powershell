$cores = (Get-CimInstance Win32_Processor).NumberOfLogicalProcessors/2
            $CPU = Get-CimInstance Win32_PerfFormattedData_PerfProc_Process | 
            Select-Object -Property Name, @{Name = 'CPU'; Expression = {'{0:n2}' -f($_.PercentProcessorTime/$cores)}},
            @{Name = 'PID'; Expression = {$_.IDProcess}} |
            Where-Object {$_.Name -notlike '*conhost*' -and $_.Name -notlike '*svchost*' -and $_.Name -notlike '*Idle*' -and $_.Name -notlike '*_Total*'} |
            Sort-Object -Property CPU -Descending |
            Select-Object -First 10 
            $CPU | Format-List