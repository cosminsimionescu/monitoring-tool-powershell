using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        string memoryUsage_script, outputMem;
        string cpuUsage_script, outputCpu;
        string volumeUsage_script, outputVol;
        string processCPU_script, outputProcCPU;
        string processName, processCPU_load, processDescription;
        string processMem_script, outputProcMem;
        string processName_mem, processMem_load, processPID_mem;
        string driveId, driveSize, driveSpace, driveSpacePercentage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void runCustomScript_Click(object sender, EventArgs e)
        {
            FormCustomScript f2 = new FormCustomScript();
            f2.Show();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (targetServer.Text.Trim() != "")
            {
                GetMemory();
                GetCPU();
                GetVolume();
                //GetProcessCPU();
                GetProcessMemory();
            }
            else
            {
                MessageBox.Show(@"Enter the server name or IP address");
            }
        }

        public void GetMemory()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts memS = new Scripts();

            memoryUsage_script = memS.memory_Script();
            outputMem = newSession.NewPsSession(targetServer.Text, memoryUsage_script);

            textBox1.Text = outputMem; //memory usage
        }
        public void GetCPU()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts cpuS = new Scripts();

            cpuUsage_script = cpuS.cpu_Script();
            outputCpu = newSession.NewPsSession(targetServer.Text, cpuUsage_script);

            txtCPU.Text = outputCpu; //CPU usage
        }

        public void GetProcessCPU()
        {
            dataGridViewProcessByCPU.Rows.Clear();
            RemoteSession newSession = new RemoteSession();
            Scripts procCPU = new Scripts();

            processCPU_script = procCPU.processByCPU_Script();
            outputProcCPU = newSession.NewPsSession(targetServer.Text, processCPU_script);

            var corectLines = outputProcCPU.Split('\n')
                             .Where(l => l != "\r").ToList();

            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                processName = corectLines[i].Split(':')[1].Trim();
                processCPU_load = corectLines[i + 1].Split(':')[1].Trim();
                processDescription = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];

                dataGridViewProcessByCPU.Rows.Add(processName, processCPU_load, processDescription);
            }
            dataGridViewProcessByCPU.ClearSelection();
        }

        public void GetProcessMemory()
        {
            dataGridViewProcessByMem.Rows.Clear();
            RemoteSession newSession = new RemoteSession();
            Scripts procMem = new Scripts();

            processMem_script = procMem.processByMem_Script();
            outputProcMem = newSession.NewPsSession(targetServer.Text, processMem_script);

            var corectLines = outputProcMem.Split('\n')
                             .Where(l => l != "\r").ToList();

            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                processName_mem = corectLines[i].Split(':')[1].Trim();
                processMem_load = corectLines[i + 1].Split(':')[1].Trim();
                processPID_mem = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];

                var processMem_load_value = Convert.ToDouble(processMem_load);
                dataGridViewProcessByMem.Rows.Add(processName_mem, processMem_load_value + " MB", processPID_mem);

            }
            dataGridViewProcessByMem.ClearSelection();
        }

        public void GetVolume()
        {
            dataGridViewFreeSpace.Rows.Clear();

            RemoteSession newSession = new RemoteSession();
            Scripts volumeS = new Scripts();

            Dictionary<string, string> driveInformations = new Dictionary<string, string>();

            volumeUsage_script = volumeS.volume_Script();
            outputVol = newSession.NewPsSession(targetServer.Text, volumeUsage_script);

            var corectLines = outputVol.Split('\n')
                             .Where(l => l != "\r").ToList();

            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 4)
            {
                driveId = corectLines[i + 3].Split(':')[1].Trim();
                driveSize = corectLines[i].Split(':')[1].Trim();
                driveSpace = corectLines[i + 1].Split(':')[1].Trim();
                driveSpacePercentage = corectLines[i + 2].Split(':')[1].Trim().Split('\r', '%')[0];
                var driveSpacePercentage_value = Convert.ToDouble(driveSpacePercentage);
                dataGridViewFreeSpace.Rows.Add(driveId, driveSize + " GB", driveSpace + " GB", driveSpacePercentage_value + " %");
                driveInformations.Add(driveId, driveSpacePercentage);
            }
            dataGridViewFreeSpace.ClearSelection();
        }
    }
}
