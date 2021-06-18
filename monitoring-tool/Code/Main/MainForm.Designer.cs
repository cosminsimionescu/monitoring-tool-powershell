namespace monitoring_tool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewExtension = new monitoring_tool.DataGridViewExtension();
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.txtMem = new System.Windows.Forms.TextBox();
            this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToNewSv = new System.Windows.Forms.ToolStripMenuItem();
            this.alerts_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.email_serverSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.tools = new System.Windows.Forms.ToolStripMenuItem();
            this.psISE = new System.Windows.Forms.ToolStripMenuItem();
            this.psConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Drive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewFreeSpaceTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewProcessByCPU = new monitoring_tool.DataGridViewExtension();
            this.columnProcessID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcessCPU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewProcessByMem = new monitoring_tool.DataGridViewExtension();
            this.columnPID_mem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcessName_mem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcess_mem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewAlerts = new monitoring_tool.DataGridViewExtension();
            this.dateTimeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textAlert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueAlert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.clearAlertsDataGrid = new System.Windows.Forms.Button();
            this.checkEmailAlerts = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkMemoryAlert = new System.Windows.Forms.CheckBox();
            this.checkFreeSpaceAlert = new System.Windows.Forms.CheckBox();
            this.checkCpuAlert = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.triggerTimerThreadsCPU = new System.Windows.Forms.Timer(this.components);
            this.triggerTimerThreadVol = new System.Windows.Forms.Timer(this.components);
            this.triggerTimerThreadsProcCheck = new System.Windows.Forms.Timer(this.components);
            this.server = new System.Windows.Forms.Label();
            this.targetServer = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Server = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_StopApp = new System.Windows.Forms.Button();
            this.btn_StartApp = new System.Windows.Forms.Button();
            this.timerCpuAlerts = new System.Windows.Forms.Timer(this.components);
            this.timerMemoryAlerts = new System.Windows.Forms.Timer(this.components);
            this.timerVolumeAlerts = new System.Windows.Forms.Timer(this.components);
            this.timerEmailCpuAlerts = new System.Windows.Forms.Timer(this.components);
            this.timerEmailMemoryAlerts = new System.Windows.Forms.Timer(this.components);
            this.timerEmailFreeSpaceAlerts = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewFreeSpace = new monitoring_tool.DataGridViewExtension();
            this.columnDrive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreePercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtension)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlerts)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewExtension
            // 
            this.dataGridViewExtension.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewExtension.Name = "dataGridViewExtension";
            this.dataGridViewExtension.Size = new System.Drawing.Size(240, 150);
            this.dataGridViewExtension.TabIndex = 0;
            // 
            // txtCPU
            // 
            this.txtCPU.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCPU.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCPU.Location = new System.Drawing.Point(198, 69);
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.ReadOnly = true;
            this.txtCPU.Size = new System.Drawing.Size(93, 33);
            this.txtCPU.TabIndex = 8;
            this.txtCPU.TabStop = false;
            this.txtCPU.Text = "      ---";
            // 
            // txtMem
            // 
            this.txtMem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMem.Location = new System.Drawing.Point(198, 14);
            this.txtMem.Name = "txtMem";
            this.txtMem.ReadOnly = true;
            this.txtMem.Size = new System.Drawing.Size(95, 33);
            this.txtMem.TabIndex = 9;
            this.txtMem.TabStop = false;
            this.txtMem.Text = "      ---";
            // 
            // optionsMenu
            // 
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToNewSv,
            this.alerts_menu,
            this.email_serverSettings,
            this.exit});
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(61, 20);
            this.optionsMenu.Text = "&Options";
            // 
            // connectToNewSv
            // 
            this.connectToNewSv.Name = "connectToNewSv";
            this.connectToNewSv.Size = new System.Drawing.Size(195, 22);
            this.connectToNewSv.Text = "Monitor another server";
            this.connectToNewSv.Click += new System.EventHandler(this.connectToNewSv_Click);
            // 
            // alerts_menu
            // 
            this.alerts_menu.Name = "alerts_menu";
            this.alerts_menu.Size = new System.Drawing.Size(195, 22);
            this.alerts_menu.Text = "Configure alerts";
            this.alerts_menu.Click += new System.EventHandler(this.alerts_menu_Click);
            // 
            // email_serverSettings
            // 
            this.email_serverSettings.Name = "email_serverSettings";
            this.email_serverSettings.Size = new System.Drawing.Size(195, 22);
            this.email_serverSettings.Text = "SMTP Server";
            this.email_serverSettings.Click += new System.EventHandler(this.email_serverSettings_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(195, 22);
            this.exit.Text = "&Exit application";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsMenu,
            this.tools});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1122, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuitem1";
            // 
            // tools
            // 
            this.tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.psISE,
            this.psConsole});
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(46, 20);
            this.tools.Text = "Tools";
            // 
            // psISE
            // 
            this.psISE.Name = "psISE";
            this.psISE.Size = new System.Drawing.Size(176, 22);
            this.psISE.Text = "PowerShell ISE";
            this.psISE.Click += new System.EventHandler(this.psISE_Click);
            // 
            // psConsole
            // 
            this.psConsole.Name = "psConsole";
            this.psConsole.Size = new System.Drawing.Size(176, 22);
            this.psConsole.Text = "PowerShell console";
            this.psConsole.Click += new System.EventHandler(this.psConsole_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(17, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 30);
            this.label1.TabIndex = 10;
            this.label1.Text = "CPU USAGE";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 30);
            this.label2.TabIndex = 11;
            this.label2.Text = "MEMORY USAGE";
            // 
            // Drive
            // 
            this.Drive.HeaderText = "Drive";
            this.Drive.Name = "Drive";
            // 
            // dataGridViewFreeSpaceTextBoxColumn1
            // 
            this.dataGridViewFreeSpaceTextBoxColumn1.HeaderText = "Drive";
            this.dataGridViewFreeSpaceTextBoxColumn1.Name = "dataGridViewFreeSpaceTextBoxColumn1";
            // 
            // dataGridViewProcessByCPU
            // 
            this.dataGridViewProcessByCPU.AllowUserToAddRows = false;
            this.dataGridViewProcessByCPU.AllowUserToDeleteRows = false;
            this.dataGridViewProcessByCPU.AllowUserToResizeColumns = false;
            this.dataGridViewProcessByCPU.AllowUserToResizeRows = false;
            this.dataGridViewProcessByCPU.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewProcessByCPU.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcessByCPU.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewProcessByCPU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewProcessByCPU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProcessID,
            this.columnProcess,
            this.columnProcessCPU});
            this.dataGridViewProcessByCPU.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dataGridViewProcessByCPU.Location = new System.Drawing.Point(22, 33);
            this.dataGridViewProcessByCPU.Name = "dataGridViewProcessByCPU";
            this.dataGridViewProcessByCPU.ReadOnly = true;
            this.dataGridViewProcessByCPU.RowHeadersVisible = false;
            this.dataGridViewProcessByCPU.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProcessByCPU.RowTemplate.Height = 25;
            this.dataGridViewProcessByCPU.Size = new System.Drawing.Size(305, 275);
            this.dataGridViewProcessByCPU.TabIndex = 18;
            this.dataGridViewProcessByCPU.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewProcessByCPU_CellFormatting);
            // 
            // columnProcessID
            // 
            this.columnProcessID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnProcessID.HeaderText = "Process ID";
            this.columnProcessID.Name = "columnProcessID";
            this.columnProcessID.ReadOnly = true;
            this.columnProcessID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcessID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnProcessID.Width = 78;
            // 
            // columnProcess
            // 
            this.columnProcess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnProcess.HeaderText = "       Process name";
            this.columnProcess.Name = "columnProcess";
            this.columnProcess.ReadOnly = true;
            this.columnProcess.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnProcess.Width = 130;
            // 
            // columnProcessCPU
            // 
            this.columnProcessCPU.HeaderText = "CPU usage (%)";
            this.columnProcessCPU.Name = "columnProcessCPU";
            this.columnProcessCPU.ReadOnly = true;
            this.columnProcessCPU.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcessCPU.Width = 94;
            // 
            // dataGridViewProcessByMem
            // 
            this.dataGridViewProcessByMem.AllowUserToAddRows = false;
            this.dataGridViewProcessByMem.AllowUserToDeleteRows = false;
            this.dataGridViewProcessByMem.AllowUserToResizeColumns = false;
            this.dataGridViewProcessByMem.AllowUserToResizeRows = false;
            this.dataGridViewProcessByMem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewProcessByMem.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcessByMem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewProcessByMem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewProcessByMem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnPID_mem,
            this.columnProcessName_mem,
            this.columnProcess_mem});
            this.dataGridViewProcessByMem.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dataGridViewProcessByMem.Location = new System.Drawing.Point(375, 33);
            this.dataGridViewProcessByMem.Name = "dataGridViewProcessByMem";
            this.dataGridViewProcessByMem.ReadOnly = true;
            this.dataGridViewProcessByMem.RowHeadersVisible = false;
            this.dataGridViewProcessByMem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProcessByMem.RowTemplate.Height = 25;
            this.dataGridViewProcessByMem.Size = new System.Drawing.Size(313, 275);
            this.dataGridViewProcessByMem.TabIndex = 19;
            // 
            // columnPID_mem
            // 
            this.columnPID_mem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnPID_mem.HeaderText = "Process ID";
            this.columnPID_mem.Name = "columnPID_mem";
            this.columnPID_mem.ReadOnly = true;
            this.columnPID_mem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnPID_mem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnProcessName_mem
            // 
            this.columnProcessName_mem.HeaderText = "  Process name";
            this.columnProcessName_mem.Name = "columnProcessName_mem";
            this.columnProcessName_mem.ReadOnly = true;
            this.columnProcessName_mem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcessName_mem.Width = 110;
            // 
            // columnProcess_mem
            // 
            this.columnProcess_mem.HeaderText = "Memory (MB)";
            this.columnProcess_mem.Name = "columnProcess_mem";
            this.columnProcess_mem.ReadOnly = true;
            this.columnProcess_mem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcess_mem.Width = 115;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(22, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(243, 30);
            this.label6.TabIndex = 20;
            this.label6.Text = "TOP PROCESSES BY CPU";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(375, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(293, 30);
            this.label7.TabIndex = 21;
            this.label7.Text = "TOP PROCESSES BY MEMORY";
            // 
            // dataGridViewAlerts
            // 
            this.dataGridViewAlerts.AllowUserToAddRows = false;
            this.dataGridViewAlerts.AllowUserToDeleteRows = false;
            this.dataGridViewAlerts.AllowUserToResizeColumns = false;
            this.dataGridViewAlerts.AllowUserToResizeRows = false;
            this.dataGridViewAlerts.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewAlerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateTimeCol,
            this.textAlert,
            this.valueAlert});
            this.dataGridViewAlerts.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dataGridViewAlerts.Location = new System.Drawing.Point(5, 118);
            this.dataGridViewAlerts.Name = "dataGridViewAlerts";
            this.dataGridViewAlerts.ReadOnly = true;
            this.dataGridViewAlerts.RowHeadersVisible = false;
            this.dataGridViewAlerts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewAlerts.RowTemplate.Height = 25;
            this.dataGridViewAlerts.Size = new System.Drawing.Size(400, 276);
            this.dataGridViewAlerts.TabIndex = 24;
            // 
            // dateTimeCol
            // 
            this.dateTimeCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateTimeCol.HeaderText = "       Date&Time";
            this.dateTimeCol.Name = "dateTimeCol";
            this.dateTimeCol.ReadOnly = true;
            this.dateTimeCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dateTimeCol.Width = 116;
            // 
            // textAlert
            // 
            this.textAlert.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.textAlert.FillWeight = 194.9239F;
            this.textAlert.HeaderText = "                           Alert";
            this.textAlert.Name = "textAlert";
            this.textAlert.ReadOnly = true;
            this.textAlert.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.textAlert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.textAlert.Width = 214;
            // 
            // valueAlert
            // 
            this.valueAlert.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.valueAlert.FillWeight = 5.076141F;
            this.valueAlert.HeaderText = " Value";
            this.valueAlert.Name = "valueAlert";
            this.valueAlert.ReadOnly = true;
            this.valueAlert.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.valueAlert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueAlert.Width = 65;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.dataGridViewProcessByCPU);
            this.panel4.Controls.Add(this.dataGridViewProcessByMem);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(423, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(703, 322);
            this.panel4.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Controls.Add(this.clearAlertsDataGrid);
            this.panel5.Controls.Add(this.checkEmailAlerts);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.checkMemoryAlert);
            this.panel5.Controls.Add(this.dataGridViewAlerts);
            this.panel5.Controls.Add(this.checkFreeSpaceAlert);
            this.panel5.Controls.Add(this.checkCpuAlert);
            this.panel5.Location = new System.Drawing.Point(4, 171);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(413, 398);
            this.panel5.TabIndex = 28;
            // 
            // clearAlertsDataGrid
            // 
            this.clearAlertsDataGrid.BackColor = System.Drawing.Color.White;
            this.clearAlertsDataGrid.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.clearAlertsDataGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.clearAlertsDataGrid.Location = new System.Drawing.Point(5, 42);
            this.clearAlertsDataGrid.Name = "clearAlertsDataGrid";
            this.clearAlertsDataGrid.Size = new System.Drawing.Size(80, 27);
            this.clearAlertsDataGrid.TabIndex = 34;
            this.clearAlertsDataGrid.Text = "Clear view";
            this.clearAlertsDataGrid.UseVisualStyleBackColor = false;
            this.clearAlertsDataGrid.Click += new System.EventHandler(this.clearAlertsDataGrid_Click);
            // 
            // checkEmailAlerts
            // 
            this.checkEmailAlerts.AutoSize = true;
            this.checkEmailAlerts.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkEmailAlerts.ForeColor = System.Drawing.Color.Black;
            this.checkEmailAlerts.Location = new System.Drawing.Point(290, 88);
            this.checkEmailAlerts.Name = "checkEmailAlerts";
            this.checkEmailAlerts.Size = new System.Drawing.Size(115, 24);
            this.checkEmailAlerts.TabIndex = 33;
            this.checkEmailAlerts.Text = "E-mail alerts";
            this.checkEmailAlerts.UseVisualStyleBackColor = true;
            this.checkEmailAlerts.CheckStateChanged += new System.EventHandler(this.checkEmailAlerts_CheckStateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(0, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 30);
            this.label3.TabIndex = 30;
            this.label3.Text = "ALERTS";
            // 
            // checkMemoryAlert
            // 
            this.checkMemoryAlert.AutoSize = true;
            this.checkMemoryAlert.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkMemoryAlert.ForeColor = System.Drawing.Color.Black;
            this.checkMemoryAlert.Location = new System.Drawing.Point(66, 88);
            this.checkMemoryAlert.Name = "checkMemoryAlert";
            this.checkMemoryAlert.Size = new System.Drawing.Size(88, 24);
            this.checkMemoryAlert.TabIndex = 32;
            this.checkMemoryAlert.Text = "Memory";
            this.checkMemoryAlert.UseVisualStyleBackColor = true;
            this.checkMemoryAlert.CheckStateChanged += new System.EventHandler(this.checkMemoryAlert_CheckStateChanged);
            // 
            // checkFreeSpaceAlert
            // 
            this.checkFreeSpaceAlert.AutoSize = true;
            this.checkFreeSpaceAlert.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkFreeSpaceAlert.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkFreeSpaceAlert.ForeColor = System.Drawing.Color.Black;
            this.checkFreeSpaceAlert.Location = new System.Drawing.Point(155, 88);
            this.checkFreeSpaceAlert.Name = "checkFreeSpaceAlert";
            this.checkFreeSpaceAlert.Size = new System.Drawing.Size(101, 24);
            this.checkFreeSpaceAlert.TabIndex = 31;
            this.checkFreeSpaceAlert.Text = "Free space";
            this.checkFreeSpaceAlert.UseVisualStyleBackColor = true;
            this.checkFreeSpaceAlert.CheckStateChanged += new System.EventHandler(this.checkFreeSpaceAlert_CheckStateChanged);
            // 
            // checkCpuAlert
            // 
            this.checkCpuAlert.AutoSize = true;
            this.checkCpuAlert.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkCpuAlert.ForeColor = System.Drawing.Color.Black;
            this.checkCpuAlert.Location = new System.Drawing.Point(5, 88);
            this.checkCpuAlert.Name = "checkCpuAlert";
            this.checkCpuAlert.Size = new System.Drawing.Size(57, 24);
            this.checkCpuAlert.TabIndex = 30;
            this.checkCpuAlert.Text = "CPU";
            this.checkCpuAlert.UseVisualStyleBackColor = true;
            this.checkCpuAlert.CheckStateChanged += new System.EventHandler(this.checkCpuAlert_CheckStateChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCPU);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtMem);
            this.panel2.Location = new System.Drawing.Point(445, 380);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(305, 109);
            this.panel2.TabIndex = 25;
            // 
            // triggerTimerThreadsCPU
            // 
            this.triggerTimerThreadsCPU.Interval = 1300;
            this.triggerTimerThreadsCPU.Tick += new System.EventHandler(this.triggerThreadMemCPU_Tick);
            // 
            // triggerTimerThreadVol
            // 
            this.triggerTimerThreadVol.Interval = 1;
            this.triggerTimerThreadVol.Tick += new System.EventHandler(this.triggerThreadVol_Tick);
            // 
            // triggerTimerThreadsProcCheck
            // 
            this.triggerTimerThreadsProcCheck.Tick += new System.EventHandler(this.triggerThreadsProcCheck_Tick);
            // 
            // server
            // 
            this.server.AutoSize = true;
            this.server.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.server.ForeColor = System.Drawing.Color.Black;
            this.server.Location = new System.Drawing.Point(15, 15);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(201, 21);
            this.server.TabIndex = 4;
            this.server.Text = "&Server name or IP address";
            // 
            // targetServer
            // 
            this.targetServer.BackColor = System.Drawing.Color.White;
            this.targetServer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.targetServer.Location = new System.Drawing.Point(61, 44);
            this.targetServer.Name = "targetServer";
            this.targetServer.Size = new System.Drawing.Size(127, 29);
            this.targetServer.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 29);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Server
            // 
            this.btn_Server.BackColor = System.Drawing.Color.White;
            this.btn_Server.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Server.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btn_Server.Location = new System.Drawing.Point(205, 44);
            this.btn_Server.Name = "btn_Server";
            this.btn_Server.Size = new System.Drawing.Size(74, 29);
            this.btn_Server.TabIndex = 31;
            this.btn_Server.Text = "Confirm";
            this.btn_Server.UseVisualStyleBackColor = false;
            this.btn_Server.Click += new System.EventHandler(this.btn_Server_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_StopApp);
            this.panel6.Controls.Add(this.btn_StartApp);
            this.panel6.Controls.Add(this.btn_Server);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Controls.Add(this.targetServer);
            this.panel6.Controls.Add(this.server);
            this.panel6.Location = new System.Drawing.Point(12, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(292, 95);
            this.panel6.TabIndex = 29;
            // 
            // btn_StopApp
            // 
            this.btn_StopApp.BackColor = System.Drawing.Color.White;
            this.btn_StopApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_StopApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btn_StopApp.Location = new System.Drawing.Point(205, 44);
            this.btn_StopApp.Name = "btn_StopApp";
            this.btn_StopApp.Size = new System.Drawing.Size(74, 29);
            this.btn_StopApp.TabIndex = 33;
            this.btn_StopApp.Text = "Stop";
            this.btn_StopApp.UseVisualStyleBackColor = false;
            this.btn_StopApp.Visible = false;
            this.btn_StopApp.Click += new System.EventHandler(this.btn_StopApp_Click);
            // 
            // btn_StartApp
            // 
            this.btn_StartApp.BackColor = System.Drawing.Color.White;
            this.btn_StartApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_StartApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.btn_StartApp.Location = new System.Drawing.Point(205, 44);
            this.btn_StartApp.Name = "btn_StartApp";
            this.btn_StartApp.Size = new System.Drawing.Size(74, 29);
            this.btn_StartApp.TabIndex = 32;
            this.btn_StartApp.Text = "Start";
            this.btn_StartApp.UseVisualStyleBackColor = false;
            this.btn_StartApp.Visible = false;
            this.btn_StartApp.Click += new System.EventHandler(this.button_StartApp_Click);
            // 
            // timerCpuAlerts
            // 
            this.timerCpuAlerts.Tick += new System.EventHandler(this.timerCpuAlerts_Tick);
            // 
            // timerMemoryAlerts
            // 
            this.timerMemoryAlerts.Tick += new System.EventHandler(this.timerMemoryAlerts_Tick);
            // 
            // timerVolumeAlerts
            // 
            this.timerVolumeAlerts.Tick += new System.EventHandler(this.timerVolumeAlerts_Tick);
            // 
            // timerEmailCpuAlerts
            // 
            this.timerEmailCpuAlerts.Tick += new System.EventHandler(this.timerEmailCpuAlerts_Tick);
            // 
            // timerEmailMemoryAlerts
            // 
            this.timerEmailMemoryAlerts.Tick += new System.EventHandler(this.timerEmailMemoryAlerts_Tick);
            // 
            // timerEmailFreeSpaceAlerts
            // 
            this.timerEmailFreeSpaceAlerts.Tick += new System.EventHandler(this.timerEmailFreeSpaceAlerts_Tick);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(30, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 30);
            this.label5.TabIndex = 17;
            this.label5.Text = "DISK SPACE";
            // 
            // dataGridViewFreeSpace
            // 
            this.dataGridViewFreeSpace.AllowUserToAddRows = false;
            this.dataGridViewFreeSpace.AllowUserToDeleteRows = false;
            this.dataGridViewFreeSpace.AllowUserToResizeColumns = false;
            this.dataGridViewFreeSpace.AllowUserToResizeRows = false;
            this.dataGridViewFreeSpace.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dataGridViewFreeSpace.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFreeSpace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewFreeSpace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDrive,
            this.columnSize,
            this.columnFreespace,
            this.columnFreePercentage});
            this.dataGridViewFreeSpace.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dataGridViewFreeSpace.Location = new System.Drawing.Point(30, 43);
            this.dataGridViewFreeSpace.Name = "dataGridViewFreeSpace";
            this.dataGridViewFreeSpace.ReadOnly = true;
            this.dataGridViewFreeSpace.RowHeadersVisible = false;
            this.dataGridViewFreeSpace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.RowTemplate.Height = 25;
            this.dataGridViewFreeSpace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewFreeSpace.Size = new System.Drawing.Size(305, 75);
            this.dataGridViewFreeSpace.TabIndex = 22;
            // 
            // columnDrive
            // 
            this.columnDrive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDrive.HeaderText = "Drive letter";
            this.columnDrive.Name = "columnDrive";
            this.columnDrive.ReadOnly = true;
            this.columnDrive.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnDrive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnDrive.Width = 71;
            // 
            // columnSize
            // 
            this.columnSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnSize.HeaderText = "Size (GB)";
            this.columnSize.Name = "columnSize";
            this.columnSize.ReadOnly = true;
            this.columnSize.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnSize.Width = 62;
            // 
            // columnFreespace
            // 
            this.columnFreespace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnFreespace.HeaderText = "Free space (GB)";
            this.columnFreespace.Name = "columnFreespace";
            this.columnFreespace.ReadOnly = true;
            this.columnFreespace.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnFreespace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnFreespace.Width = 95;
            // 
            // columnFreePercentage
            // 
            this.columnFreePercentage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnFreePercentage.HeaderText = "Free (%)";
            this.columnFreePercentage.Name = "columnFreePercentage";
            this.columnFreePercentage.ReadOnly = true;
            this.columnFreePercentage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnFreePercentage.Width = 85;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Controls.Add(this.dataGridViewFreeSpace);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(768, 370);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(346, 140);
            this.panel3.TabIndex = 26;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1122, 574);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menu);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitoring tool - PowerShell";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtension)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlerts)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Drive;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewFreeSpaceTextBoxColumn1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem alerts_menu;
        private System.Windows.Forms.Timer triggerTimerThreadsCPU;
        private System.Windows.Forms.Timer triggerTimerThreadsProcCheck;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.TextBox txtMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPID_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessName_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcess_mem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem connectToNewSv;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessCPU;
        private System.Windows.Forms.Label server;
        public System.Windows.Forms.TextBox targetServer;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button btn_Server;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Timer timerCpuAlerts;
        private System.Windows.Forms.Timer timerMemoryAlerts;
        private System.Windows.Forms.Timer timerVolumeAlerts;
        private System.Windows.Forms.Timer timerEmailCpuAlerts;
        private System.Windows.Forms.Timer timerEmailMemoryAlerts;
        private System.Windows.Forms.Timer timerEmailFreeSpaceAlerts;
        public System.Windows.Forms.CheckBox checkCpuAlert;
        public System.Windows.Forms.CheckBox checkFreeSpaceAlert;
        public System.Windows.Forms.CheckBox checkMemoryAlert;
        public System.Windows.Forms.CheckBox checkEmailAlerts;
        private System.Windows.Forms.Button clearAlertsDataGrid;
        private DataGridViewExtension dataGridViewExtension;
        private DataGridViewExtension dataGridViewProcessByCPU;
        private DataGridViewExtension dataGridViewProcessByMem;
        private DataGridViewExtension dataGridViewAlerts;
        private System.Windows.Forms.ToolStripMenuItem tools;
        private System.Windows.Forms.ToolStripMenuItem psISE;
        private System.Windows.Forms.ToolStripMenuItem psConsole;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn textAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueAlert;
        private System.Windows.Forms.Label label5;
        public DataGridViewExtension dataGridViewFreeSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDrive;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreePercentage;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button button_StartApp;
        public System.Windows.Forms.Button btn_StartApp;
        public System.Windows.Forms.Button btn_StopApp;
        private System.Windows.Forms.ToolStripMenuItem email_serverSettings;
        private System.Windows.Forms.Timer triggerTimerThreadVol;
    }
}

