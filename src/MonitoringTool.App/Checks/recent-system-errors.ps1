# @name: System log errors (last hour)
# @description: Critical and Error events in the System event log during the last 60 minutes.
# @interval: 600
# @timeout: 60

$since = (Get-Date).AddMinutes(-60)
$events = Get-WinEvent -FilterHashtable @{ LogName = 'System'; Level = 1, 2; StartTime = $since } -ErrorAction SilentlyContinue

if (-not $events) {
    [pscustomobject]@{ Status = 'Ok'; Message = 'No errors in the System log in the last hour'; Value = 0 }
    return
}

$critical = @($events | Where-Object { $_.Level -eq 1 }).Count
$top = $events | Group-Object -Property ProviderName | Sort-Object -Property Count -Descending | Select-Object -First 3 |
    ForEach-Object { "$($_.Name) x$($_.Count)" }

[pscustomobject]@{
    Status  = if ($critical -gt 0) { 'Critical' } else { 'Warning' }
    Message = "$(@($events).Count) error event(s) in the last hour: $($top -join ', ')"
    Value   = @($events).Count
}
