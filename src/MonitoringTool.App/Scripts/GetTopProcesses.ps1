# Runs on the monitored server. Returns the top N processes by CPU and by memory in one round trip.
param([int]$Top = 10)

# Win32_ComputerSystem gives one total for all sockets; Win32_Processor returns one row per socket,
# which broke the per-core division in v1 on multi-socket servers.
$cores = [math]::Max(1, [int](Get-CimInstance -ClassName Win32_ComputerSystem -ErrorAction Stop).NumberOfLogicalProcessors)

Get-CimInstance -ClassName Win32_PerfFormattedData_PerfProc_Process -Filter "Name <> '_Total' AND Name <> 'Idle'" -ErrorAction Stop |
    Sort-Object -Property PercentProcessorTime -Descending |
    Select-Object -First $Top |
    ForEach-Object {
        [pscustomobject]@{
            Kind         = 'Cpu'
            Id           = [int]$_.IDProcess
            Name         = $_.Name -replace '#\d+$', ''
            CpuPercent   = [math]::Round($_.PercentProcessorTime / $cores, 2)
            WorkingSetMb = [math]::Round($_.WorkingSet / 1MB, 1)
        }
    }

Get-Process |
    Sort-Object -Property WorkingSet64 -Descending |
    Select-Object -First $Top |
    ForEach-Object {
        [pscustomobject]@{
            Kind         = 'Memory'
            Id           = [int]$_.Id
            Name         = $_.ProcessName
            CpuPercent   = [double]0
            WorkingSetMb = [math]::Round($_.WorkingSet64 / 1MB, 1)
        }
    }
