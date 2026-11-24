# @name: Pending reboot
# @description: Windows Update, component servicing or a file rename is waiting for a restart.
# @interval: 1800
# @timeout: 30

$reasons = @()
if (Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending') {
    $reasons += 'component servicing'
}
if (Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired') {
    $reasons += 'Windows Update'
}
$renames = (Get-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager' -Name PendingFileRenameOperations -ErrorAction SilentlyContinue).PendingFileRenameOperations
if ($renames) {
    $reasons += 'pending file renames'
}

if ($reasons.Count -eq 0) {
    [pscustomobject]@{ Status = 'Ok'; Message = 'No reboot pending' }
} else {
    [pscustomobject]@{ Status = 'Warning'; Message = "Reboot pending ($($reasons -join ', '))" }
}
