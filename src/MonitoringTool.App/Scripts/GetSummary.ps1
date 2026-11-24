# Runs on the monitored server (Windows PowerShell 5.1 or PowerShell 7+).
# Returns raw numbers as a single object; all formatting happens in the client so results do not
# depend on the server's culture (v1 parsed culture-formatted "Format-List" text).
$os = Get-CimInstance -ClassName Win32_OperatingSystem -ErrorAction Stop

# Win32_Processor.LoadPercentage is averaged over ~1 second and is not localized
# (unlike Get-Counter paths, which are translated on non-English Windows).
$cpu = (Get-CimInstance -ClassName Win32_Processor -ErrorAction Stop |
    Measure-Object -Property LoadPercentage -Average).Average
if ($null -eq $cpu) {
    $cpu = (Get-CimInstance -ClassName Win32_PerfFormattedData_PerfOS_Processor -Filter "Name = '_Total'" -ErrorAction Stop).PercentProcessorTime
}

[pscustomobject]@{
    ComputerName  = $env:COMPUTERNAME
    CpuPercent    = [double]$cpu
    TotalMemoryKb = [double]$os.TotalVisibleMemorySize
    FreeMemoryKb  = [double]$os.FreePhysicalMemory
    UptimeSeconds = [double]((Get-Date) - $os.LastBootUpTime).TotalSeconds
}
