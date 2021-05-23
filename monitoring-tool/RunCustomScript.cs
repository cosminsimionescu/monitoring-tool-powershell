using System;
using System.Collections.ObjectModel;
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
            Pipeline pipeline = runspace.CreatePipeline();

            pipeline.Commands.AddScript("$sessions = New-PSSession -ComputerName " + ServerName + Environment.NewLine  //Script for remotely
            + "Invoke-Command -session $sessions -ScriptBlock {" + command + "}" + Environment.NewLine                 //running PS commands
            + "Remove-PSSession -Session $sessions" + Environment.NewLine);

            pipeline.Commands.Add("Out-String");

            Collection<PSObject> results = new Collection<PSObject>();
            try
            {
                results = pipeline.Invoke(); //Invoke command
            }

            catch (Exception ex)
            {
                results.Add(new PSObject((object)ex.Message));
            }
            runspace.Close(); //close the runspace

            StringBuilder stringBuilder = new StringBuilder();

            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            return stringBuilder.ToString(); //return output
        }
    }
}
