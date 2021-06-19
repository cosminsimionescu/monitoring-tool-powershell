using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public async Task<string> NewPowerShell(string serverName, string command)
        {
            string result = await Task.Run(() =>
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                PowerShell powerShell = PowerShell.Create();
                runspace.Open();
                powerShell.Runspace = runspace;

                powerShell.Commands.AddScript("$remoteSession = New-PSSession -ComputerName " + serverName + Environment.NewLine
                 + "Invoke-Command -Session $remoteSession -ScriptBlock {" + command + "}" + Environment.NewLine
                 + "Remove-PSSession -Session $remoteSession");

                powerShell.Commands.AddCommand("Out-String");

                Collection<PSObject> output = new Collection<PSObject>();
                try
                {
                    output = powerShell.Invoke();
                }
                catch { }

                runspace.Close();

                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject obj in output)
                {
                    stringBuilder.AppendLine(obj.ToString());
                }
                powerShell.Commands.AddCommand("Clear-Host");

                return stringBuilder.ToString();
            }
            );
            return result;
        }
    }
}
