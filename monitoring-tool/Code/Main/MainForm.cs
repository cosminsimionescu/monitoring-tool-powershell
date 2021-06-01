using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        DateTime Time = DateTime.Now;
        string date;

        private static MainForm Instance;
        public static MainForm GetInstance()
        {
            if (Instance == null) Instance = new MainForm();
            return Instance;
        }

        ConfirmBox InstanceConfirmBox = ConfirmBox.GetInstanceConfirmBox();// Instanta forma confirmare server

        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            Instance = this;
      
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert(); // Instanta forma setari alerte
            MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings(); // Instanta forma setari SMTP settings(mail)
            InstanceServerSettings.LoadConfigurationSMPTFile();
            InstanceAlert.LoadConfigurationFile();
        }

        private void btn_Server_Click(object sender, EventArgs e)
        {
            if (targetServer.Text.Trim() != "")
            {
                targetServer.Enabled = false;
                btn_Server.Enabled = false;
                btn_Server.Visible = false;

                InstanceConfirmBox.ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter the server name");
            }
        }

        public void Monitor_loop()
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            Thread CPUThread = new(InstanceResults.GetCPU);
            Thread MemThread = new(InstanceResults.GetMemory);

            Thread ProcCPUThread = new(InstanceResults.GetProcessCPU);
            Thread ProcMemThread = new(InstanceResults.GetProcessMemory);

            Thread VolThread = new(InstanceResults.GetVolume);
           

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
            triggerThreadVol.Interval = 120000;
        }

        private void triggerThreadCPU_Tick(object sender, EventArgs e)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            Thread CPUThread = new(InstanceResults.GetCPU);
            Thread MemThread = new(InstanceResults.GetMemory);

            CPUThread.Start();
            MemThread.Start();
        }

        private void triggerThreadVol_Tick(object sender, EventArgs e)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            Thread VolThread = new(InstanceResults.GetVolume);
            VolThread.Start();
        }

        private void triggerThreadsProcCheck_Tick(object sender, EventArgs e)
        {
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            InstanceCheck.Alerts();

            Thread ProcCPUThread = new(InstanceResults.GetProcessCPU);
            Thread ProcMemThread = new(InstanceResults.GetProcessMemory);

            ProcMemThread.Start();
            ProcCPUThread.Start();
        }

        private void alerts_menu_Click(object sender, EventArgs e)
        {
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();

            if (!InstanceAlert.Visible)
            {
                InstanceAlert.Show();
            }
            else
            {
                InstanceAlert.BringToFront();
            }
        }

        private void connectToNewSv_Click(object sender, EventArgs e)
        {
            targetServer.Enabled = true;
            btn_Server.Enabled = true;
            btn_Server.Visible = true;
            MessageBox.Show("Enter the name or IP address");
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Application is closing");
            Environment.Exit(0);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Application is closing");
            Environment.Exit(0);
        }

        public void UpdateMemoryAlert(string memoryAlert)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            date = Time.ToString("F");
            dataGridViewAlerts.Rows.Add(date, memoryAlert, InstanceResults.memoryPercentage + "%");
        }

        public void UpdateCPUAlert(string cpuAlert)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            date = Time.ToString("F");
            dataGridViewAlerts.Rows.Add(date, cpuAlert, InstanceResults.cpuPercentage + "%");
        }

        public void UpdateVolumeAlert(string volumeAlert, string driveName, double volumePercentage)
        {
            dataGridViewAlerts.Rows.Add(date, volumeAlert + driveName, volumePercentage + "%");
        }


        //public void GetMemory()
        //{
        //    RemoteSession newSession = new RemoteSession();
        //    Scripts memS = new Scripts();
        //    string memoryUsage_script = memS.memory_Script();

        //    Task<string> task_Mem = newSession.NewPsSession(targetServer.Text.Trim(), memoryUsage_script);

        //    txtMem.Text = " " + task_Mem.Result + "%";
        //    try
        //    {
        //        memoryPercentage = Convert.ToDouble(task_Mem.Result);
        //    }
        //    catch { }

        //}

        public void UpdateMemoryLoad(string memUsage)
        {
            txtMem.Text = memUsage;
        }

        //public void GetCPU()
        //{
        //    RemoteSession newSession = new RemoteSession();
        //    Scripts cpuS = new Scripts();
        //    string cpuUsage_script = cpuS.cpu_Script(); //CPU usage script

        //    Task<string> task_CPU = newSession.NewPsSession(targetServer.Text.Trim(), cpuUsage_script);

        //    txtCPU.Text = " " + task_CPU.Result + "%";
        //    try
        //    {
        //        cpuPercentage = Convert.ToDouble(task_CPU.Result);
        //    }
        //    catch { }
        //}
        public void UpdateCpuLoad(string cpuUsage)
        {
            txtCPU.Text = cpuUsage;
        }

        //public void GetProcessCPU()
        //{
        //    RemoteSession newSession = new RemoteSession();
        //    Scripts procCPU = new Scripts();
        //    string processCPU_script = procCPU.processByCPU_Script("10"); //Procesess by CPU script

        //    Task<string> task_ProcByCPU = newSession.NewPsSession(targetServer.Text.Trim(), processCPU_script);
        //    string outputProcCPU = task_ProcByCPU.Result;
        //    List<string> corectLines = outputProcCPU.Split('\n')
        //                 .Where(l => l != "\r").ToList();
        //    corectLines.Remove("");

        //    for (int i = 0; i < corectLines.Count(); i += 3)
        //    {
        //        string processName_cpu = corectLines[i].Split(':')[1].Trim().Split('#')[0];
        //        string processCPU_load = corectLines[i + 1].Split(':')[1].Trim();
        //        string processPID_cpu = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
        //        double processCPU_load_val = Convert.ToDouble(processCPU_load);
        //        if (i == 0)
        //        {
        //            dataGridViewProcessByCPU.Rows.Clear();
        //        }
        //        try
        //        {
        //            dataGridViewProcessByCPU.Rows.Add(processPID_cpu, processName_cpu, processCPU_load_val + "%");
        //        }
        //        catch { }
        //    }
        //}
        public async void UpdateProcessCpuLoad(string pidCpu, string processNameCpu, double processCpuLoadValue)
        {
            try
            {
                await Task.Run(() => dataGridViewProcessByCPU.Rows.Add(pidCpu, processNameCpu, processCpuLoadValue + "%"));
            }
            catch { }
            dataGridViewProcessByCPU.ClearSelection();
        }
        public void ClearGridProcessCpuLoad()
        {
            dataGridViewProcessByCPU.Rows.Clear();
        }

        //public void GetProcessMemory()
        //{
        //    RemoteSession newSession = new RemoteSession();
        //    Scripts procMem = new Scripts();
        //    string processMem_script = procMem.processByMem_Script("10"); //Process by memory script
        //    string targetSV = targetServer.Text.Trim();

        //    Task<string> task_ProcByMem = newSession.NewPsSession(targetSV, processMem_script);

        //    string outputProcMem = task_ProcByMem.Result;
        //    List<string> corectLines = outputProcMem.Split('\n')
        //                 .Where(l => l != "\r").ToList();
        //    corectLines.Remove("");

        //    for (int i = 0; i < corectLines.Count(); i += 3)
        //    {
        //        string processName_mem = corectLines[i].Split(':')[1].Trim();
        //        string processMem_load = corectLines[i + 1].Split(':')[1].Trim();
        //        string processPID_mem = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
        //        double processMem_load_val = Convert.ToDouble(processMem_load);
        //        if (i == 0)
        //        {
        //            dataGridViewProcessByMem.Rows.Clear();
        //        }
        //        try
        //        {
        //            dataGridViewProcessByMem.Rows.Add(processPID_mem, processName_mem, processMem_load_val + " MB");
        //        }
        //        catch { }

        //    }
        //    dataGridViewProcessByMem.ClearSelection();
        //}
        public async void UpdateProcessMemLoad(string pidMem, string processNameMem, double processMemLoadValue)
        {
            try
            {
                await Task.Run(() => dataGridViewProcessByMem.Rows.Add(pidMem, processNameMem, processMemLoadValue + " MB"));
            }
            catch { }
            dataGridViewProcessByMem.ClearSelection();
        }
        public void ClearGridProcessMemLoad()
        {
            dataGridViewProcessByMem.Rows.Clear();
        }

        //public void GetVolume()
        //{
        //    RemoteSession newSession = new RemoteSession();

        //    Scripts volumeS = new Scripts();
        //    string volumeUsage_script = volumeS.volume_Script();//volume script

        //    driveInformations = new Dictionary<string, double>();
        //    Task<string> task_Vol = newSession.NewPsSession(targetServer.Text.Trim(), volumeUsage_script);

        //    string outputVol = task_Vol.Result;

        //    List<string> corectLines = outputVol.Split('\n')
        //                 .Where(l => l != "\r").ToList();
        //    corectLines.Remove("");

        //    for (int i = 0; i < corectLines.Count(); i += 4)
        //    {
        //        string driveId = corectLines[i + 3].Split(':')[1].Trim();
        //        string driveSize = corectLines[i].Split(':')[1].Trim();
        //        string driveSpace = corectLines[i + 1].Split(':')[1].Trim();
        //        string driveSpacePercentage = corectLines[i + 2].Split(':')[1].Trim().Split('\r', '%')[0];
        //        double driveSpacePercentage_value = Convert.ToDouble(driveSpacePercentage);
        //        if (i == 0)
        //        {
        //            dataGridViewFreeSpace.Rows.Clear();
        //        }
        //        try
        //        {
        //            dataGridViewFreeSpace.Rows.Add(driveId, driveSize + " GB", driveSpace + " GB", driveSpacePercentage_value + " %");
        //        }
        //        catch { }

        //        driveInformations.Add(driveId, driveSpacePercentage_value);
        //    }
        //    dataGridViewFreeSpace.ClearSelection();
        //}
        public void UpdateFreeSpace(string driveId, string driveSize, string driveSpace, double driveSpacePercentageValue)
        {
            try
            {
                dataGridViewFreeSpace.Rows.Add(driveId, driveSize + " GB", driveSpace + " GB", driveSpacePercentageValue + " %");
            }
            catch { }
            dataGridViewFreeSpace.ClearSelection();
        }
        public void ClearGridUpdateFreeSpace()
        {
            dataGridViewFreeSpace.Rows.Clear();
        }
    }

}
