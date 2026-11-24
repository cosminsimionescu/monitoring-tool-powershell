# @name: Expiring certificates
# @description: Certificates with a private key in LocalMachine\My that expire within 30 days.
# @interval: 21600
# @timeout: 60

$warningDays = 30
$criticalDays = 7
$now = Get-Date

$certs = Get-ChildItem -Path Cert:\LocalMachine\My -ErrorAction Stop |
    Where-Object { $_.HasPrivateKey -and $_.NotAfter -lt $now.AddDays($warningDays) }

if (-not $certs) {
    [pscustomobject]@{ Status = 'Ok'; Message = "No certificates expire in the next $warningDays days" }
    return
}

foreach ($cert in $certs) {
    $days = [math]::Floor(($cert.NotAfter - $now).TotalDays)
    $name = if ($cert.FriendlyName) { $cert.FriendlyName } else { $cert.Subject }
    $text = if ($days -lt 0) { "$name expired $(-$days) day(s) ago" } else { "$name expires in $days day(s)" }
    [pscustomobject]@{
        Status  = if ($days -lt $criticalDays) { 'Critical' } else { 'Warning' }
        Message = $text
        Value   = $days
    }
}
