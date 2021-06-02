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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.txtMem = new System.Windows.Forms.TextBox();
            this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToNewSv = new System.Windows.Forms.ToolStripMenuItem();
            this.alerts_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runCustomScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.server = new System.Windows.Forms.Label();
            this.targetServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Drive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewFreeSpaceTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewProcessByCPU = new System.Windows.Forms.DataGridView();
            this.columnProcessID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcessCPU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewProcessByMem = new System.Windows.Forms.DataGridView();
            this.columnPID_mem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcessName_mem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProcess_mem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewFreeSpace = new System.Windows.Forms.DataGridView();
            this.dataGridViewAlerts = new System.Windows.Forms.DataGridView();
            this.dateTimeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textAlert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueAlert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_Server = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.triggerThreadsCPU = new System.Windows.Forms.Timer(this.components);
            this.triggerThreadVol = new System.Windows.Forms.Timer(this.components);
            this.triggerThreadsProcCheck = new System.Windows.Forms.Timer(this.components);
            this.timerAlerts = new System.Windows.Forms.Timer(this.components);
            this.columnDrive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreePercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlerts)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCPU
            // 
            this.txtCPU.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCPU.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCPU.Location = new System.Drawing.Point(189, 53);
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.ReadOnly = true;
            this.txtCPU.Size = new System.Drawing.Size(77, 33);
            this.txtCPU.TabIndex = 8;
            // 
            // txtMem
            // 
            this.txtMem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMem.Location = new System.Drawing.Point(21, 53);
            this.txtMem.Name = "txtMem";
            this.txtMem.ReadOnly = true;
            this.txtMem.Size = new System.Drawing.Size(77, 33);
            this.txtMem.TabIndex = 9;
            // 
            // optionsMenu
            // 
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToNewSv,
            this.alerts_menu,
            this.exit});
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(61, 20);
            this.optionsMenu.Text = "&Options";
            // 
            // connectToNewSv
            // 
            this.connectToNewSv.Name = "connectToNewSv";
            this.connectToNewSv.Size = new System.Drawing.Size(211, 22);
            this.connectToNewSv.Text = "Connect to another server";
            this.connectToNewSv.Click += new System.EventHandler(this.connectToNewSv_Click);
            // 
            // alerts_menu
            // 
            this.alerts_menu.Name = "alerts_menu";
            this.alerts_menu.Size = new System.Drawing.Size(211, 22);
            this.alerts_menu.Text = "Configure alerts";
            this.alerts_menu.Click += new System.EventHandler(this.alerts_menu_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(211, 22);
            this.exit.Text = "&Exit application";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runCustomScript});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(46, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // runCustomScript
            // 
            this.runCustomScript.Name = "runCustomScript";
            this.runCustomScript.Size = new System.Drawing.Size(67, 22);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsMenu,
            this.toolsMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1108, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuitem1";
            // 
            // server
            // 
            this.server.AutoSize = true;
            this.server.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.server.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.server.Location = new System.Drawing.Point(12, 17);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(192, 21);
            this.server.TabIndex = 4;
            this.server.Text = "&Server name or IP address";
            // 
            // targetServer
            // 
            this.targetServer.BackColor = System.Drawing.Color.White;
            this.targetServer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.targetServer.Location = new System.Drawing.Point(58, 59);
            this.targetServer.Name = "targetServer";
            this.targetServer.Size = new System.Drawing.Size(127, 29);
            this.targetServer.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(180, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "CPU usage";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Memory usage";
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
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(11, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "Disk space";
            // 
            // dataGridViewProcessByCPU
            // 
            this.dataGridViewProcessByCPU.AllowUserToAddRows = false;
            this.dataGridViewProcessByCPU.AllowUserToDeleteRows = false;
            this.dataGridViewProcessByCPU.AllowUserToResizeColumns = false;
            this.dataGridViewProcessByCPU.AllowUserToResizeRows = false;
            this.dataGridViewProcessByCPU.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewProcessByCPU.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcessByCPU.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewProcessByCPU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewProcessByCPU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProcessID,
            this.columnProcess,
            this.columnProcessCPU});
            this.dataGridViewProcessByCPU.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewProcessByCPU.Location = new System.Drawing.Point(33, 40);
            this.dataGridViewProcessByCPU.Name = "dataGridViewProcessByCPU";
            this.dataGridViewProcessByCPU.ReadOnly = true;
            this.dataGridViewProcessByCPU.RowHeadersVisible = false;
            this.dataGridViewProcessByCPU.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProcessByCPU.RowTemplate.Height = 25;
            this.dataGridViewProcessByCPU.Size = new System.Drawing.Size(305, 275);
            this.dataGridViewProcessByCPU.TabIndex = 18;
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
            this.dataGridViewProcessByMem.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcessByMem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewProcessByMem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewProcessByMem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnPID_mem,
            this.columnProcessName_mem,
            this.columnProcess_mem});
            this.dataGridViewProcessByMem.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewProcessByMem.Location = new System.Drawing.Point(33, 350);
            this.dataGridViewProcessByMem.Name = "dataGridViewProcessByMem";
            this.dataGridViewProcessByMem.ReadOnly = true;
            this.dataGridViewProcessByMem.RowHeadersVisible = false;
            this.dataGridViewProcessByMem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProcessByMem.RowTemplate.Height = 25;
            this.dataGridViewProcessByMem.Size = new System.Drawing.Size(304, 275);
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
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(33, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "Top processes by CPU usage";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(33, 318);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(283, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "Top processes by memory usage";
            // 
            // dataGridViewFreeSpace
            // 
            this.dataGridViewFreeSpace.AllowUserToAddRows = false;
            this.dataGridViewFreeSpace.AllowUserToDeleteRows = false;
            this.dataGridViewFreeSpace.AllowUserToResizeColumns = false;
            this.dataGridViewFreeSpace.AllowUserToResizeRows = false;
            this.dataGridViewFreeSpace.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dataGridViewFreeSpace.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFreeSpace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewFreeSpace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDrive,
            this.columnSize,
            this.columnFreespace,
            this.columnFreePercentage});
            this.dataGridViewFreeSpace.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewFreeSpace.Location = new System.Drawing.Point(11, 43);
            this.dataGridViewFreeSpace.Name = "dataGridViewFreeSpace";
            this.dataGridViewFreeSpace.ReadOnly = true;
            this.dataGridViewFreeSpace.RowHeadersVisible = false;
            this.dataGridViewFreeSpace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.RowTemplate.Height = 25;
            this.dataGridViewFreeSpace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewFreeSpace.Size = new System.Drawing.Size(293, 75);
            this.dataGridViewFreeSpace.TabIndex = 22;
            // 
            // dataGridViewAlerts
            // 
            this.dataGridViewAlerts.AllowUserToAddRows = false;
            this.dataGridViewAlerts.AllowUserToDeleteRows = false;
            this.dataGridViewAlerts.AllowUserToResizeRows = false;
            this.dataGridViewAlerts.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateTimeCol,
            this.textAlert,
            this.valueAlert});
            this.dataGridViewAlerts.Location = new System.Drawing.Point(11, 32);
            this.dataGridViewAlerts.MultiSelect = false;
            this.dataGridViewAlerts.Name = "dataGridViewAlerts";
            this.dataGridViewAlerts.ReadOnly = true;
            this.dataGridViewAlerts.RowHeadersVisible = false;
            this.dataGridViewAlerts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewAlerts.RowTemplate.Height = 25;
            this.dataGridViewAlerts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewAlerts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAlerts.Size = new System.Drawing.Size(382, 279);
            this.dataGridViewAlerts.TabIndex = 24;
            this.dataGridViewAlerts.TabStop = false;
            // 
            // dateTimeCol
            // 
            this.dateTimeCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateTimeCol.HeaderText = "Date&Time";
            this.dateTimeCol.Name = "dateTimeCol";
            this.dateTimeCol.ReadOnly = true;
            this.dateTimeCol.Width = 121;
            // 
            // textAlert
            // 
            this.textAlert.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.textAlert.FillWeight = 194.9239F;
            this.textAlert.HeaderText = "                           Alert";
            this.textAlert.Name = "textAlert";
            this.textAlert.ReadOnly = true;
            this.textAlert.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.textAlert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.textAlert.Width = 211;
            // 
            // valueAlert
            // 
            this.valueAlert.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.valueAlert.FillWeight = 5.076141F;
            this.valueAlert.HeaderText = "Value";
            this.valueAlert.Name = "valueAlert";
            this.valueAlert.ReadOnly = true;
            this.valueAlert.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.valueAlert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueAlert.Width = 65;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel3.Controls.Add(this.dataGridViewFreeSpace);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(423, 538);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(313, 128);
            this.panel3.TabIndex = 26;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.dataGridViewProcessByCPU);
            this.panel4.Controls.Add(this.dataGridViewProcessByMem);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(752, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(356, 629);
            this.panel4.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.dataGridViewAlerts);
            this.panel5.Location = new System.Drawing.Point(12, 345);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(405, 321);
            this.panel5.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(11, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 30;
            this.label3.Text = "Alerts";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCPU);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtMem);
            this.panel2.Location = new System.Drawing.Point(423, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 100);
            this.panel2.TabIndex = 25;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_Server);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Controls.Add(this.targetServer);
            this.panel6.Controls.Add(this.server);
            this.panel6.Location = new System.Drawing.Point(32, 55);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(292, 100);
            this.panel6.TabIndex = 29;
            // 
            // btn_Server
            // 
            this.btn_Server.Location = new System.Drawing.Point(202, 59);
            this.btn_Server.Name = "btn_Server";
            this.btn_Server.Size = new System.Drawing.Size(59, 29);
            this.btn_Server.TabIndex = 31;
            this.btn_Server.Text = "Confirm";
            this.btn_Server.UseVisualStyleBackColor = true;
            this.btn_Server.Click += new System.EventHandler(this.btn_Server_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 29);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // triggerThreadsCPU
            // 
            this.triggerThreadsCPU.Interval = 1300;
            this.triggerThreadsCPU.Tick += new System.EventHandler(this.triggerThreadCPU_Tick);
            // 
            // triggerThreadVol
            // 
            this.triggerThreadVol.Interval = 1;
            this.triggerThreadVol.Tick += new System.EventHandler(this.triggerThreadVol_Tick);
            // 
            // triggerThreadsProcCheck
            // 
            this.triggerThreadsProcCheck.Tick += new System.EventHandler(this.triggerThreadsProcCheck_Tick);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(1108, 666);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Monitoring tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlerts)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem runCustomScript;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Label server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Drive;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewFreeSpaceTextBoxColumn1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewProcessByCPU;
        private System.Windows.Forms.DataGridView dataGridViewProcessByMem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewAlerts;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox targetServer;
        public System.Windows.Forms.Button btn_Server;
        private System.Windows.Forms.ToolStripMenuItem alerts_menu;
        private System.Windows.Forms.Timer triggerThreadsCPU;
        private System.Windows.Forms.Timer triggerThreadVol;
        private System.Windows.Forms.Timer triggerThreadsProcCheck;
        public System.Windows.Forms.DataGridView dataGridViewFreeSpace;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.TextBox txtMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPID_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessName_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcess_mem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem connectToNewSv;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn textAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueAlert;
        private System.Windows.Forms.Timer timerAlerts;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessCPU;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDrive;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreePercentage;
    }
}

