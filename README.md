# monitoring-tool-powershell
Monitoring tool with PowerShell

This is an aplication wich represents a monitoring tool for Windows servers. It is based on PowerShell scripts integrated with C#, using .NET 5.0.


In order for the application to work properly, you`ll have to Enable PSRemoting on the target machine,
and also add the server from where the application is making requests to the trusted host list. 

You can run the following commands, one by one, in a PowerShell console with admin rights, filling out the correct IP/Hostname:

Enable-PSRemoting (https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/enable-psremoting?view=powershell-7.2) 

set-item wsman:\localhost\client\trustedhosts -Concatenate -value 'localhost'
