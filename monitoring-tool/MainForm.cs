using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        private static MainForm Instance;
        public static MainForm GetInstance()
        {
            if (Instance == null) Instance = new MainForm();
            return Instance;
        }

        public static string serverName = "";

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            Instance = this;
        }

        private void Alerting()
        {

        }

        public void Monitor_loop()
        {
            Thread CPUThread = new(GetCPU);
            Thread ProcCPUThread = new(GetProcessCPU);
            Thread ProcMemThread = new(GetProcessMemory);
            Thread VolThread = new(GetVolume);
            Thread MemThread = new(GetMemory);

            CPUThread.Start();
            MemThread.Start();
            ProcMemThread.Start();
            ProcCPUThread.Start();
            VolThread.Start();

            triggerThreadsCPU.Interval = 15000;
            triggerThreadsCPU.Enabled = true;

            triggerThreadsProcCheck.Enabled = true;
            triggerThreadsProcCheck.Interval = 5000;

            triggerThreadVol.Enabled = true;
            triggerThreadVol.Interval = 60000;

        }

        public void GetMemory()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts memS = new Scripts();
            string memoryUsage_script = memS.memory_Script();

            Task<string> task_Mem = newSession.NewPsSession(targetServer.Text.Trim(), memoryUsage_script);

            txtMem.Text = task_Mem.Result;

        }

        public void GetCPU()
        {

            RemoteSession newSession = new RemoteSession();
            Scripts cpuS = new Scripts();
            string cpuUsage_script = cpuS.cpu_Script();

            Task<string> task_CPU = newSession.NewPsSession(targetServer.Text.Trim(), cpuUsage_script);

            txtCPU.Text = task_CPU.Result;
        }


        public void GetProcessCPU()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts procCPU = new Scripts();
            string processCPU_script = procCPU.processByCPU_Script("10");

            Task<string> task_ProcByCPU = newSession.NewPsSession(targetServer.Text.Trim(), processCPU_script);
            string outputProcCPU = task_ProcByCPU.Result;
            var corectLines = outputProcCPU.Split('\n')
                             .Where(l => l != "\r").ToList();

            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                string processName_cpu = corectLines[i].Split(':')[1].Trim().Split('#')[0];
                string processCPU_load = corectLines[i + 1].Split(':')[1].Trim();
                string processPID_cpu = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
                var processCPU_load_val = Convert.ToDouble(processCPU_load);
                if (i == 0)
                {
                    dataGridViewProcessByCPU.Rows.Clear();
                }

                dataGridViewProcessByCPU.Rows.Add(processPID_cpu, processName_cpu, processCPU_load_val);


            }
            dataGridViewProcessByCPU.ClearSelection();

        }

        public void GetProcessMemory()
        {

            RemoteSession newSession = new RemoteSession();
            Scripts procMem = new Scripts();
            string processMem_script = procMem.processByMem_Script("10");

            Task<string> task_ProcByMem = newSession.NewPsSession(targetServer.Text.Trim(), processMem_script);

            string outputProcMem = task_ProcByMem.Result;
            var corectLines = outputProcMem.Split('\n')
                             .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 3)
            {
                string processName_mem = corectLines[i].Split(':')[1].Trim();
                string processMem_load = corectLines[i + 1].Split(':')[1].Trim();
                string processPID_mem = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
                var processMem_load_val = Convert.ToDouble(processMem_load);
                if (i == 0)
                {
                    dataGridViewProcessByMem.Rows.Clear();
                }

                dataGridViewProcessByMem.Rows.Add(processPID_mem, processName_mem, processMem_load_val + " MB");

            }
            dataGridViewProcessByMem.ClearSelection();


        }


        public void GetVolume()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts volumeS = new Scripts();

            string volumeUsage_script = volumeS.volume_Script();

            Task<string> task_Vol = newSession.NewPsSession(targetServer.Text.Trim(), volumeUsage_script);
            string outputVol = task_Vol.Result;

            var corectLines = outputVol.Split('\n')
                             .Where(l => l != "\r").ToList();
            corectLines.Remove("");

            for (int i = 0; i < corectLines.Count(); i += 4)
            {
                string driveId = corectLines[i + 3].Split(':')[1].Trim();
                string driveSize = corectLines[i].Split(':')[1].Trim();
                string driveSpace = corectLines[i + 1].Split(':')[1].Trim();
                string driveSpacePercentage = corectLines[i + 2].Split(':')[1].Trim().Split('\r', '%')[0];
                var driveSpacePercentage_value = Convert.ToDouble(driveSpacePercentage);
                if (i == 0)
                {
                    dataGridViewFreeSpace.Rows.Clear();
                }

                dataGridViewFreeSpace.Rows.Add(driveId, driveSize + " GB", driveSpace + " GB", driveSpacePercentage_value + " %");

            }
            dataGridViewFreeSpace.ClearSelection();

        }

        private void btn_Server_Click(object sender, EventArgs e)
        {
            if (targetServer.Text.Trim() != "")
            {
                targetServer.Enabled = false;
                btn_Server.Enabled = false;
                btn_Server.Visible = false;
                serverName = targetServer.Text.Trim();
                ConfirmBox confirm = new ConfirmBox();
                confirm.ShowDialog();
            }
        }



        private void triggerThreadCPU_Tick(object sender, EventArgs e) //thread CPU
        {

            Thread CPUThread = new(GetCPU);
            Thread MemThread = new(GetMemory);
           
            CPUThread.Start();
            MemThread.Start();
        }
        private void triggerThreadVol_Tick(object sender, EventArgs e)
        {
            Thread VolThread = new(GetVolume);
            VolThread.Start();
        }

        private void triggerThreadsProcCheck_Tick(object sender, EventArgs e)
        {

            Thread ProcCPUThread = new(GetProcessCPU);
            Thread ProcMemThread = new(GetProcessMemory);

            ProcMemThread.Start();
            ProcCPUThread.Start();

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

    }

}
