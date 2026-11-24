# @name: Stopped automatic services
# @description: Services set to start automatically that are not running.
# @interval: 300
# @timeout: 60

# Services that are "Automatic" but legitimately stop on their own (trigger-start / update helpers).
$ignore = @(
    'gupdate', 'gupdatem', 'edgeupdate', 'edgeupdatem', 'MapsBroker', 'sppsvc', 'RemoteRegistry',
    'ShellHWDetection', 'tiledatamodelsvc', 'WbioSrvc', 'CDPSvc', 'BITS', 'wuauserv', 'TrustedInstaller',
    'GoogleUpdaterService*', 'GoogleUpdaterInternalService*', 'OneSyncSvc*', 'CDPUserSvc*'
)

$stopped = Get-CimInstance -ClassName Win32_Service -Filter "StartMode = 'Auto' AND State <> 'Running'" -ErrorAction Stop |
    Where-Object { $name = $_.Name; -not ($ignore | Where-Object { $name -like $_ }) }

if (-not $stopped) {
    [pscustomobject]@{ Status = 'Ok'; Message = 'All automatic services are running'; Value = 0 }
    return
}

$names = ($stopped | Select-Object -ExpandProperty DisplayName | Sort-Object) -join ', '
[pscustomobject]@{
    Status  = 'Critical'
    Message = "$(@($stopped).Count) automatic service(s) not running: $names"
    Value   = @($stopped).Count
}
