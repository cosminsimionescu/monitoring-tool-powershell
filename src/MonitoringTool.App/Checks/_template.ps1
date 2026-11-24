# Template for a custom check. Copy this file to a new name WITHOUT the leading underscore
# (files starting with '_' are ignored), edit it, then press "Reload checks" in the app.
#
# The script runs ON THE MONITORED SERVER (Windows PowerShell 5.1 or PowerShell 7), via remoting.
#
# Metadata (all optional):
# @name: My custom check
# @description: What this check verifies.
# @interval: 300
# @timeout: 60
#
# Output contract - emit one or more objects with these properties:
#   Status  : 'Ok', 'Warning' or 'Critical'   (the worst one wins)
#   Message : short text shown in the app and in alert e-mails
#   Value   : optional number (count, days, MB…)
# Also accepted: $true / $false (Ok / Critical), plain strings (informational, Ok), or no output (Ok).
# A terminating error or timeout shows the check as Unknown; it does not raise an alert.

$service = Get-Service -Name 'Spooler' -ErrorAction Stop

if ($service.Status -eq 'Running') {
    [pscustomobject]@{ Status = 'Ok'; Message = 'Print spooler is running' }
} else {
    [pscustomobject]@{ Status = 'Critical'; Message = "Print spooler is $($service.Status)" }
}
