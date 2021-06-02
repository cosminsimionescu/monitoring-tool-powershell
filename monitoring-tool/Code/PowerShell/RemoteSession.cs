using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace monitoring_tool
{
    class RemoteSession
    {
        private static RemoteSession InstanceRemoteSession;
        public static RemoteSession GetInstanceRemoteSession()
        {
            if (InstanceRemoteSession == null) InstanceRemoteSession = new RemoteSession();
            return InstanceRemoteSession;
        }

        public RemoteSession()
        {
            InstanceRemoteSession = this;
        }

        public async Task<string> NewPowerShell(string ServerName, string command)
        {
            string result = await Task.Run(() =>
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                PowerShell psSession = PowerShell.Create();
                runspace.Open();
                psSession.Runspace = runspace;

                psSession.Commands.AddScript("$sessions = New-PSSession -ComputerName " + ServerName + Environment.NewLine
            + "Invoke-Command -session $sessions -ScriptBlock {" + command + "}" + Environment.NewLine
            + "Remove-PSSession -Session $sessions");

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
                psSession.Commands.AddCommand("Clear-Host");

                return stringBuilder.ToString();
            }
            );
            return result;
        }
    }
}
