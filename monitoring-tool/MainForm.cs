using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        string memoryUsage_script;
        string cpuUsage_script;
        string processName_cpu, processCPU_load, processPID_cpu, processCPU_script, outputProcCPU;
        string processName_mem, processMem_load, processPID_mem, processMem_script, outputProcMem;
        string driveId, driveSize, driveSpace, driveSpacePercentage, volumeUsage_script, outputVol;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            targetServer.Enabled = false;
            if (targetServer.Text.Trim() != "")
            {
                Thread CPUThread = new Thread(new ThreadStart(GetCPU));
                Thread MemThread = new Thread(new ThreadStart(GetMemory));
                Thread VolThread = new Thread(new ThreadStart(GetVolume));
                Thread ProcCPUThread = new Thread(new ThreadStart(GetProcessCPU));
                Thread ProcMemThread = new Thread(new ThreadStart(GetProcessMemory));

                CPUThread.Start();
                MemThread.Start();
                VolThread.Start();
                ProcCPUThread.Start();
                ProcMemThread.Start();

            }
            else
            {
                MessageBox.Show(@"Enter the server name or IP address");
            }

            btnConnect.Enabled = true;
            targetServer.Enabled = true;
        }

        public void GetMemory()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts memS = new Scripts();

            memoryUsage_script = memS.memory_Script();
            txtMem.Text = newSession.NewPsSession(targetServer.Text, memoryUsage_script);

        }

        public void GetCPU()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts cpuS = new Scripts();

            cpuUsage_script = cpuS.cpu_Script();
            txtCPU.Text = newSession.NewPsSession(targetServer.Text, cpuUsage_script);
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
                processName_cpu = corectLines[i].Split(':')[1].Trim().Split('#')[0];
                processCPU_load = corectLines[i + 1].Split(':')[1].Trim();
                processPID_cpu = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
                var processCPU_load_val = Convert.ToDouble(processCPU_load); //cpuload per process
                dataGridViewProcessByCPU.Rows.Add(processPID_cpu,  processName_cpu, processCPU_load_val);
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
                var processMem_load_val = Convert.ToDouble(processMem_load);//mem per process
                dataGridViewProcessByMem.Rows.Add(processPID_mem, processName_mem , processMem_load_val + " MB");

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
