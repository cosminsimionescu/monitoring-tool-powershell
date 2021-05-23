using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace monitoring_tool
{
    class RunCustomScript
    {
        public string Run(string ServerName, string command)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();   //open the runspace
            PowerShell ps1 = PowerShell.Create();

            ps1.Commands.AddScript("$sessions = New-PSSession -ComputerName " + ServerName + Environment.NewLine  //Script for remotely
            + "Invoke-Command -session $sessions -ScriptBlock {" + command + "}" + Environment.NewLine                 //running PS commands
            + "Remove-PSSession -Session $sessions");                                                                  //on servers from same domain;
            ps1.Commands.AddCommand("Out-String");

            IAsyncResult async = ps1.BeginInvoke();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject result in ps1.EndInvoke(async))
            {
                stringBuilder.AppendLine(result.ToString());
            }

            return stringBuilder.ToString();

           // runspace.Close();

        }
    }
}
