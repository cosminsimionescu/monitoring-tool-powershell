using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        readonly DateTime Time = DateTime.Now; //time

        private static MainForm Instance; // MainForm instance
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
            Instance = this;

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

        private void button_StartApp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Server monitoring was started");
            Initial_run(); //first run
            Monitor_loop(); //timer initialisation
            btn_StartApp.Visible = false;
            btn_StopApp.Visible = true;
            
        }
        private void btn_StopApp_Click(object sender, EventArgs e)
        {
            StopApp();
            btn_StopApp.Visible = false;
            targetServer.Enabled = true; 
            btn_Server.Enabled = true; 
            btn_Server.Visible = true; 
            MessageBox.Show("Server monitoring was stopped");
        }
        public void StopApp()
        {
            triggerTimerThreadsCPU.Enabled = false;
            triggerTimerThreadsProcCheck.Enabled = false;
            triggerTimerThreadVol.Enabled = false;
            txtCPU.Text = "";
            txtMem.Text = "";
            ClearGridProcessCpuLoad();
            ClearGridProcessMemLoad();
            ClearGridUpdateFreeSpace();  
        }

        public void ResetApp()
        {
            StopApp();
            targetServer.Enabled = true;
            btn_Server.Enabled = true;
            btn_Server.Visible = true;
            btn_StartApp.Visible = false;
            btn_StopApp.Visible = false;
            targetServer.Text = "";
        }

        public void Monitor_loop() //initialization of the threads 
        {                          //setting the timer`s interval and enabling it
            triggerTimerThreadsCPU.Interval = 9000; //setare timp
            triggerTimerThreadsCPU.Enabled = true; //activare

            triggerTimerThreadsProcCheck.Interval = 10000;
            triggerTimerThreadsProcCheck.Enabled = true;

            triggerTimerThreadVol.Interval = 120000;
            triggerTimerThreadVol.Enabled = true;
        }

        public void Initial_run()
        {
            Tasks Task = Tasks.GetInstanceTask();

            Thread CPUThread = new(Task.GetCPU); //CPU usage
            Thread MemThread = new(Task.GetMemory); //Memory usage
            Thread ProcCPUThread = new(Task.GetProcessCPU); //Processes by CPU
            Thread ProcMemThread = new(Task.GetProcessMemory); //Processes by Memory
            Thread VolThread = new(Task.GetVolume); //Volume

            CPUThread.Start();
            MemThread.Start();
            Thread.Sleep(150);

            ProcMemThread.Start();
            Thread.Sleep(100);

            ProcCPUThread.Start();
            Thread.Sleep(50);

            VolThread.Start();
        }

        private void triggerThreadMemCPU_Tick(object sender, EventArgs e)//Timer for triggering the threads where the PowerShell sessions 
        {
            Tasks Task = Tasks.GetInstanceTask();

            Thread CPUThread = new(Task.GetCPU);
            Thread MemThread = new(Task.GetMemory);

            CPUThread.Start();
            MemThread.Start();
        }

        private void triggerThreadVol_Tick(object sender, EventArgs e) //Timer for triggering the threads where the PowerShell sessions 
        {
            Tasks Task = Tasks.GetInstanceTask();

            Thread VolThread = new(Task.GetVolume);
            VolThread.Start();
        }

        private void triggerThreadsProcCheck_Tick(object sender, EventArgs e) 
        {
            Tasks Task = Tasks.GetInstanceTask();

            Thread ProcCPUThread = new(Task.GetProcessCPU);
            Thread ProcMemThread = new(Task.GetProcessMemory);

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
            ResetApp();
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

            await Task.Run(() => dataGridViewProcessByCPU.Rows.Add(pidCpu, processNameCpu, processCpuLoadValue));

            dataGridViewProcessByCPU.ClearSelection();

        }
        public void ClearGridProcessCpuLoad()
        {
            dataGridViewProcessByCPU.Rows.Clear();
        } //Clear process by cpu (Datagrid view)

        public void UpdateProcessMemLoad(string pidMem, string processNameMem, double processMemLoadValue) //Update processes by mem on MainForm(Datagrid view)
        {

            dataGridViewProcessByMem.Rows.Add(pidMem, processNameMem, processMemLoadValue + " MB");

            dataGridViewProcessByMem.ClearSelection();
        }


        public void ClearGridProcessMemLoad()
        {
            dataGridViewProcessByMem.Rows.Clear();
        } //Clear process by mem (Datagrid view)

        public void UpdateFreeSpace(string driveId, string driveSize, string driveSpace, double driveSpacePercentageValue) //Update free space on MainForm(Datagrid view)
        {

            dataGridViewFreeSpace.Rows.Add(driveId, driveSize + " GB", driveSpace + " GB", driveSpacePercentageValue + " %");

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
            string date = Time.ToString();
            dataGridViewAlerts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridViewAlerts.RowHeadersVisible = false;
            dataGridViewAlerts.Rows.Add(date, memoryAlert, InstanceResults.memoryPercentage + "%");
        }

        public void UpdateCPUAlert(string cpuAlert) //Update alerts for cpu load on MainForm(Datagrid view)
        {
            ParseResults InstanceResults = ParseResults.GetInstanceResults();
            DateTime Time = DateTime.Now; //time
            string date = Time.ToString();
            dataGridViewAlerts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridViewAlerts.RowHeadersVisible = false;
            dataGridViewAlerts.Rows.Add(date, cpuAlert, InstanceResults.cpuPercentage + "%");
        }

        public void UpdateVolumeAlert(string volumeAlert, string driveName, double volumePercentage) //Update alerts for Free Space on MainForm(Datagrid view)
        {
            DateTime Time = DateTime.Now; //time
            string date = Time.ToString();
            dataGridViewAlerts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridViewAlerts.RowHeadersVisible = false;
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
            if (checkEmailAlerts.CheckState == CheckState.Checked) {

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
                checkEmailAlerts.CheckState = CheckState.Unchecked;
                timerEmailCpuAlerts.Enabled = false;
                timerEmailMemoryAlerts.Enabled = false;
                timerEmailFreeSpaceAlerts.Enabled = false;
                MessageBox.Show("Check e-mail settings");
                }
            }
            else { }
          
        }

        private async void timerCpuAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = false;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();

            await Task.Run(() =>
           {
               InstanceCheck.AlertCpu(sendEmail);
           });
        }

        private async void timerMemoryAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = false;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();

            await Task.Run(() =>
            {
                InstanceCheck.AlertMemory(sendEmail);
            });
        }

        private async void timerVolumeAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = false;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            await Task.Run(() =>
            {
                InstanceCheck.AlertFreeSpace(sendEmail);
            });
        }

        private async void timerEmailCpuAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = true;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            if (checkEmailAlerts.CheckState == CheckState.Checked)
            {
                await Task.Run(() =>
                {
                    InstanceCheck.AlertCpu(sendEmail);
                });
            }
        }

        private async void timerEmailMemoryAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = true;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();
            if (checkEmailAlerts.CheckState == CheckState.Checked)
            {

                await Task.Run(() =>
                {
                    InstanceCheck.AlertMemory(sendEmail);
                });
            }
        }

        private async void timerEmailFreeSpaceAlerts_Tick(object sender, EventArgs e)
        {
            bool sendEmail = true;
            AlertsCheck InstanceCheck = AlertsCheck.GetInstanceCheck();


            if (checkEmailAlerts.CheckState == CheckState.Checked)
            {
                await Task.Run(() =>
                {
                    InstanceCheck.AlertFreeSpace(sendEmail);
                });
            }
        }

        private void clearAlertsDataGrid_Click(object sender, EventArgs e)
        {
            dataGridViewAlerts.Rows.Clear();
        }

        private void psISE_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell_ise.exe";
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.Verb = "runas";
                p.Start();
            }
            catch { };
        }

        private void psConsole_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe";
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.Verb = "runas";
                p.Start();
            }
            catch { };
        }

        private void dataGridViewProcessByCPU_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
                if (e.RowIndex==0)
                {
                    double red = 60;
                    double orange = 40;
                    double green = 40;
                    if (Convert.ToDouble(dataGridViewProcessByCPU.Rows[0].Cells[2].Value) > red)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    if (Convert.ToDouble(dataGridViewProcessByCPU.Rows[0].Cells[2].Value) < red &&
                        Convert.ToDouble(dataGridViewProcessByCPU.Rows[0].Cells[2].Value) > orange)
                    {
                        e.CellStyle.ForeColor = Color.Orange;
                    }
                    if (Convert.ToDouble(dataGridViewProcessByCPU.Rows[0].Cells[2].Value) < green)
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                }
        }

        private void email_serverSettings_Click(object sender, EventArgs e)
        {
            MailServerSettings InstanceServerSettings = MailServerSettings.GetInstanceServerSettings();

            if (!InstanceServerSettings.Visible)
            {
                InstanceServerSettings.Show();
            }
            else
            {
                InstanceServerSettings.BringToFront();
            }
        }
    }
}
