# @name: Failed scheduled tasks
# @description: Enabled, non-Microsoft scheduled tasks whose last run returned an error.
# @interval: 900
# @timeout: 90

# 0 = success, 0x41301 = still running, 0x41303 = has not run yet, 0x41325 = queued.
$okCodes = @(0, 267009, 267011, 267045)

$failed = Get-ScheduledTask -ErrorAction Stop |
    Where-Object { $_.State -ne 'Disabled' -and $_.TaskPath -notlike '\Microsoft\*' } |
    ForEach-Object {
        $info = $_ | Get-ScheduledTaskInfo -ErrorAction SilentlyContinue
        if ($info -and $info.LastRunTime -gt [datetime]'2000-01-01' -and $okCodes -notcontains $info.LastTaskResult) {
            '{0}{1} (0x{2:X})' -f $_.TaskPath, $_.TaskName, $info.LastTaskResult
        }
    }

if (-not $failed) {
    [pscustomobject]@{ Status = 'Ok'; Message = 'No failed scheduled tasks'; Value = 0 }
    return
}

[pscustomobject]@{
    Status  = 'Warning'
    Message = "$(@($failed).Count) task(s) failed on their last run: $($failed -join ', ')"
    Value   = @($failed).Count
}
