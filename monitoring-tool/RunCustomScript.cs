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
            Pipeline pipeline = runspace.CreatePipeline();

            pipeline.Commands.AddScript("$sessions = New-PSSession -ComputerName " + ServerName + Environment.NewLine  //Script for remotely
            + "Invoke-Command -session $sessions -ScriptBlock {" + command + "}" + Environment.NewLine                 //running PS commands
            + "Remove-PSSession -Session $sessions");                                                                  //on servers from same domain;
            pipeline.Commands.Add("Out-String");

            pipeline.InvokeAsync();

            var results = pipeline.Output; //Invoke command

            

            runspace.Close(); //close the runspace

            return results.ToString(); //return output

        }
    }
}
