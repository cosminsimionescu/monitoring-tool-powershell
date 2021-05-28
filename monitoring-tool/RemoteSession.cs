using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace monitoring_tool
{
    class RemoteSession
    {
        public string NewPsSession(string ServerName, string command)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            PowerShell psSession = PowerShell.Create();

            psSession.Commands.AddScript("$sessions = New-PSSession -ComputerName " + ServerName + Environment.NewLine  
            + "Invoke-Command -session $sessions -ScriptBlock {" + command + "}" + Environment.NewLine                 
            + "Remove-PSSession -Session $sessions" + Environment.NewLine);

            psSession.Commands.AddCommand("Out-String");

            Collection<PSObject> results = new Collection<PSObject>();
            try
            {
                results = psSession.Invoke(); 
            }

            catch (Exception ex)
            {
                results.Add(new PSObject((object)ex.Message));
            }
            runspace.Close(); 

            StringBuilder stringBuilder = new StringBuilder();

            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString(); 
        }
    }
}
