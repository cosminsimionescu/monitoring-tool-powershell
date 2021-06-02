using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        readonly DateTime Time = DateTime.Now; //time

        private static MainForm Instance; //MainForm instance
        public static MainForm GetInstance()
        {
            if (Instance == null) Instance = new MainForm();
            return Instance;
        }

        readonly ConfirmBox InstanceConfirmBox = ConfirmBox.GetInstanceConfirmBox();// Instanta forma confirmare server

        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false; //disable unsafe cross thread calls exception checks for debuging

            InitializeComponent();
            Instance = this; // Instance = curent MainForm

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert(); // Instanta forma setari alerte
            MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings(); // Instanta forma setari SMTP settings(mail)
            InstanceServerSettings.LoadConfigurationSMPTFile(); //Load saved e-mail configurations for sending e-mails
            InstanceAlert.LoadConfigurationFile(); //Load saved configurations for alerts prefferences
        }

        private void btn_Server_Click(object sender, EventArgs e) //Button to connect to the server filled in the respective TextBox from MainForm
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

        public void Monitor_loop() //initialization of the threads 
        {                          //settings the timer`s interval and enabling it
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
            triggerThreadsProcCheck.Interval = 8000;

            triggerThreadVol.Enabled = true;
            triggerThreadVol.Interval = 120000;

        }

        private void triggerThreadCPU_Tick(object sender, EventArgs e)//Timer for triggering the threads where the PowerShell sessions 
        {                                                             //and results will process
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            Thread CPUThread = new(InstanceResults.GetCPU);
            Thread MemThread = new(InstanceResults.GetMemory);

            CPUThread.Start();
            MemThread.Start();
        }

        private void triggerThreadVol_Tick(object sender, EventArgs e) //Timer for triggering the threads where the PowerShell sessions 
        {                                                              //and results will process
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            Thread VolThread = new(InstanceResults.GetVolume);
            VolThread.Start();
        }

        private void triggerThreadsProcCheck_Tick(object sender, EventArgs e) //Timer for triggering the threads where the PowerShell sessions 
        {                                                                     //and results will process
            ParseResults InstanceResults = ParseResults.GetInstanceResults();

            Thread ProcCPUThread = new(InstanceResults.GetProcessCPU);
            Thread ProcMemThread = new(InstanceResults.GetProcessMemory);

            ProcMemThread.Start();
            ProcCPUThread.Start();
        }

        private void alerts_menu_Click(object sender, EventArgs e) //Show the window form for selecting alerts preferences
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

        private void connectToNewSv_Click(object sender, EventArgs e) //Connect to a new server after the "Connect" btn was disabled
        {
            targetServer.Enabled = true; //Enable text box to enter the server/ip address again
            btn_Server.Enabled = true; //Enable the connect button
            btn_Server.Visible = true; //Make the connect button visible
            MessageBox.Show("Enter the name or IP address");
        }

        private void exit_Click(object sender, EventArgs e) //Close MainForm
        {
            MessageBox.Show("Application is closing");
            Environment.Exit(0);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) //Close MainForm
        {
            MessageBox.Show("Application is closing");
            Environment.Exit(0);
        }

        public void UpdateMemoryLoad(string memUsage) //Update mem load(%) on MainForm(TextBox)
        {
            txtMem.Text = memUsage;
        }

        public void UpdateCpuLoad(string cpuUsage) //Update cpu load(%) on MainForm(TextBox)
        {
            txtCPU.Text = cpuUsage;
        }

        public async void UpdateProcessCpuLoad(string pidCpu, string processNameCpu, double processCpuLoadValue) //Update processes by cpu on MainForm(Datagrid view)
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
        } //Clear process by cpu (Datagrid view)

        public async void UpdateProcessMemLoad(string pidMem, string processNameMem, double processMemLoadValue) //Update processes by mem on MainForm(Datagrid view)
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
        } //Clear process by mem (Datagrid view)

        public void UpdateFreeSpace(string driveId, string driveSize, string driveSpace, double driveSpacePercentageValue) //Update free space on MainForm(Datagrid view)
        {
            try
            {
                dataGridViewFreeSpace.Rows.Add(driveId, driveSize + " GB", driveSpace + " GB", driveSpacePercentageValue + " %");
            }
            catch { }
            dataGridViewFreeSpace.ClearSelection();
        }

        public void ClearGridUpdateFreeSpace() //Clear free space (Datagrid view)
        {
            dataGridViewFreeSpace.Rows.Clear();
        }

        public void UpdateMemoryAlert(string memoryAlert) //Update alerts for memory load on MainForm(Datagrid view)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            DateTime Time = DateTime.Now; //time
            string date = Time.ToString("F");
            dataGridViewAlerts.Rows.Add(date, memoryAlert, InstanceResults.memoryPercentage + "%");
        }

        public void UpdateCPUAlert(string cpuAlert) //Update alerts for cpu load on MainForm(Datagrid view)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            string date = Time.ToString("F");
            dataGridViewAlerts.Rows.Add(date, cpuAlert, InstanceResults.cpuPercentage + "%");
        }

        public void UpdateVolumeAlert(string volumeAlert, string driveName, double volumePercentage) //Update alerts for Free Space on MainForm(Datagrid view)
        {
            DateTime Time = DateTime.Now; //time
            string date = Time.ToString("F");
            dataGridViewAlerts.Rows.Add(date, volumeAlert + driveName, volumePercentage + "%");
        }


        private void checkCpuAlert_CheckStateChanged(object sender, EventArgs e)
        {
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            int time = Convert.ToInt32(InstanceAlert.triggerTime.Text.Trim());

            string format = InstanceAlert.timeFormatInternalAlerts.Text.Trim();

            if (format == "minutes")
            {
                int intervalAlertInApp = time * 60000;
                if (checkCpuAlert.CheckState == CheckState.Checked)
                {
                    timerCpuAlerts.Interval = intervalAlertInApp;
                    timerCpuAlerts.Enabled = true;
                }
                else
                {
                    timerCpuAlerts.Enabled = false;
                }

            }
            else
            {
                int intervalAlertInApp = time * 1000;
                if (checkCpuAlert.CheckState == CheckState.Checked)
                {
                    timerCpuAlerts.Interval = intervalAlertInApp;
                    timerCpuAlerts.Enabled = true;
                }
                else
                {
                    timerCpuAlerts.Enabled = false;
                }

            }
        }

        private void checkMemoryAlert_CheckStateChanged(object sender, EventArgs e)
        {
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            int time = Convert.ToInt32(InstanceAlert.triggerTime.Text.Trim());


            string format = InstanceAlert.timeFormatInternalAlerts.Text.Trim();

            if (format == "minutes")
            {
                int intervalAlertInApp = time * 60000;
                if (checkMemoryAlert.CheckState == CheckState.Checked)
                {
                    timerMemoryAlerts.Interval = intervalAlertInApp;
                    timerMemoryAlerts.Enabled = true;
                }
                else
                {
                    timerMemoryAlerts.Enabled = false;
                }
            }
            else
            {
                int intervalAlertInApp = time * 1000;
                if (checkMemoryAlert.CheckState == CheckState.Checked)
                {
                    timerMemoryAlerts.Interval = intervalAlertInApp;
                    timerMemoryAlerts.Enabled = true;
                }
                else
                {
                    timerMemoryAlerts.Enabled = false;
                }
            }

        }

        private void checkFreeSpaceAlert_CheckStateChanged(object sender, EventArgs e)
        {

            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            int time = Convert.ToInt32(InstanceAlert.triggerTime.Text.Trim());

            string format = InstanceAlert.timeFormatInternalAlerts.Text.Trim();

            if (format == "minutes")
            {
                int intervalAlertInApp = time * 60000;
                if (checkFreeSpaceAlert.CheckState == CheckState.Checked)
                {
                    timerVolumeAlerts.Interval = intervalAlertInApp;
                    timerVolumeAlerts.Enabled = true;
                }
                else
                {
                    timerVolumeAlerts.Enabled = false;
                }
            }
            else
            {
                int intervalAlertInApp = time * 1000;
                if (checkFreeSpaceAlert.CheckState == CheckState.Checked)
                {
                    timerVolumeAlerts.Interval = intervalAlertInApp;
                    timerVolumeAlerts.Enabled = true;
                }
                else
                {
                    timerVolumeAlerts.Enabled = false;
                }
            }
        }

        private void checkEmailAlerts_CheckStateChanged(object sender, EventArgs e)
        {
            AlertSettings InstanceAlert = AlertSettings.GetInstanceAlert();
            MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings();

            if (checkEmailAlerts.CheckState == CheckState.Checked &&
                InstanceAlert.checkBoxEmail.CheckState == CheckState.Checked &&
                InstanceServerSettings.txtUser.Text.Trim() != "" && InstanceAlert.timeFormat.Text.Trim() != "")
            {
                int intervalEmails;

                string format = InstanceAlert.timeFormat.Text.Trim();

                int time = Convert.ToInt32(InstanceAlert.intervalAlertEmail.Text.Trim());

                if (format == "minutes")
                {
                    intervalEmails = time * 60000;
                    timerEmailCpuAlerts.Interval = intervalEmails;
                    timerEmailCpuAlerts.Enabled = true;

                    timerEmailMemoryAlerts.Interval = intervalEmails;
                    timerEmailMemoryAlerts.Enabled = true;

                    timerEmailFreeSpaceAlerts.Interval = intervalEmails;
                    timerEmailFreeSpaceAlerts.Enabled = true;
                }
                else
                {
                    intervalEmails = time * 3600000;
                    timerEmailCpuAlerts.Interval = intervalEmails;
                    timerEmailCpuAlerts.Enabled = true;

                    timerEmailMemoryAlerts.Interval = intervalEmails;
                    timerEmailMemoryAlerts.Enabled = true;

                    timerEmailFreeSpaceAlerts.Interval = intervalEmails;
                    timerEmailFreeSpaceAlerts.Enabled = true;
                }
            }
            else
            {
                timerEmailCpuAlerts.Enabled = false;
                timerEmailMemoryAlerts.Enabled = false;
                timerEmailFreeSpaceAlerts.Enabled = false;
            }
        }

        private void timerCpuAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = false;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();

            InstanceCheck.AlertCpu(sendEmail);

        }

        private void timerMemoryAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = false;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();

            InstanceCheck.AlertMemory(sendEmail);

        }

        private void timerVolumeAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = false;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            InstanceCheck.AlertFreeSpace(sendEmail);

        }

        private void timerEmailCpuAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = true;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            if (checkEmailAlerts.CheckState == CheckState.Checked)
            {
                InstanceCheck.AlertCpu(sendEmail);
            }
        }

        private void timerEmailMemoryAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = true;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            if (checkEmailAlerts.CheckState == CheckState.Checked)
            {
                InstanceCheck.AlertMemory(sendEmail);
            }
        }

        private void timerEmailFreeSpaceAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = true;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            if (checkEmailAlerts.CheckState == CheckState.Checked)
            {
                InstanceCheck.AlertFreeSpace(sendEmail);
            }
        }

        private void clearAlertsDataGrid_Click(object sender, EventArgs e)
        {
            dataGridViewAlerts.Rows.Clear();
        }
    }
}
