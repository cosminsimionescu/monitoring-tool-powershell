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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.txtMem = new System.Windows.Forms.TextBox();
            this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.alerts_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runCustomScript = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.server = new System.Windows.Forms.Label();
            this.targetServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Drive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewFreeSpaceTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
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
            this.columnDrive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreePercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.email_alert = new System.Windows.Forms.CheckBox();
            this.vol_alert = new System.Windows.Forms.CheckBox();
            this.cpu_alert = new System.Windows.Forms.CheckBox();
            this.mem_alert = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_Server = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.triggerThreadsCPU = new System.Windows.Forms.Timer(this.components);
            this.triggerThreadVol = new System.Windows.Forms.Timer(this.components);
            this.triggerThreadsProcCheck = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.txtCPU.Location = new System.Drawing.Point(235, 67);
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.ReadOnly = true;
            this.txtCPU.Size = new System.Drawing.Size(55, 33);
            this.txtCPU.TabIndex = 8;
            // 
            // txtMem
            // 
            this.txtMem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMem.Location = new System.Drawing.Point(235, 12);
            this.txtMem.Name = "txtMem";
            this.txtMem.ReadOnly = true;
            this.txtMem.Size = new System.Drawing.Size(55, 33);
            this.txtMem.TabIndex = 9;
            // 
            // optionsMenu
            // 
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alerts_menu,
            this.exit});
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(61, 20);
            this.optionsMenu.Text = "&Options";
            // 
            // alerts_menu
            // 
            this.alerts_menu.Name = "alerts_menu";
            this.alerts_menu.Size = new System.Drawing.Size(158, 22);
            this.alerts_menu.Text = "Configure alerts";
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(158, 22);
            this.exit.Text = "&Exit application";
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
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsMenu,
            this.toolsMenu,
            this.helpMenu});
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
            this.targetServer.Text = "172.24.1.154";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(92, 70);
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
            this.label2.Location = new System.Drawing.Point(92, 15);
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
            this.label5.Location = new System.Drawing.Point(106, 9);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcessByCPU.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewProcessByCPU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewProcessByCPU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProcessID,
            this.columnProcess,
            this.columnProcessCPU});
            this.dataGridViewProcessByCPU.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewProcessByCPU.Location = new System.Drawing.Point(13, 28);
            this.dataGridViewProcessByCPU.Name = "dataGridViewProcessByCPU";
            this.dataGridViewProcessByCPU.ReadOnly = true;
            this.dataGridViewProcessByCPU.RowHeadersVisible = false;
            this.dataGridViewProcessByCPU.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProcessByCPU.RowTemplate.Height = 25;
            this.dataGridViewProcessByCPU.Size = new System.Drawing.Size(305, 278);
            this.dataGridViewProcessByCPU.TabIndex = 18;
            // 
            // columnProcessID
            // 
            this.columnProcessID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnProcessID.HeaderText = "Process ID";
            this.columnProcessID.Name = "columnProcessID";
            this.columnProcessID.ReadOnly = true;
            this.columnProcessID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcessID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnProcess
            // 
            this.columnProcess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnProcess.HeaderText = "       Process name";
            this.columnProcess.Name = "columnProcess";
            this.columnProcess.ReadOnly = true;
            this.columnProcess.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcess.Width = 130;
            // 
            // columnProcessCPU
            // 
            this.columnProcessCPU.HeaderText = "CPU usage (%)";
            this.columnProcessCPU.Name = "columnProcessCPU";
            this.columnProcessCPU.ReadOnly = true;
            this.columnProcessCPU.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewProcessByMem
            // 
            this.dataGridViewProcessByMem.AllowUserToAddRows = false;
            this.dataGridViewProcessByMem.AllowUserToDeleteRows = false;
            this.dataGridViewProcessByMem.AllowUserToResizeColumns = false;
            this.dataGridViewProcessByMem.AllowUserToResizeRows = false;
            this.dataGridViewProcessByMem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewProcessByMem.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProcessByMem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewProcessByMem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewProcessByMem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnPID_mem,
            this.columnProcessName_mem,
            this.columnProcess_mem});
            this.dataGridViewProcessByMem.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewProcessByMem.Location = new System.Drawing.Point(13, 346);
            this.dataGridViewProcessByMem.Name = "dataGridViewProcessByMem";
            this.dataGridViewProcessByMem.ReadOnly = true;
            this.dataGridViewProcessByMem.RowHeadersVisible = false;
            this.dataGridViewProcessByMem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProcessByMem.RowTemplate.Height = 25;
            this.dataGridViewProcessByMem.Size = new System.Drawing.Size(305, 277);
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
            this.columnProcessName_mem.HeaderText = "      Process name";
            this.columnProcessName_mem.Name = "columnProcessName_mem";
            this.columnProcessName_mem.ReadOnly = true;
            this.columnProcessName_mem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProcessName_mem.Width = 130;
            // 
            // columnProcess_mem
            // 
            this.columnProcess_mem.HeaderText = "Memory usage (MB)";
            this.columnProcess_mem.Name = "columnProcess_mem";
            this.columnProcess_mem.ReadOnly = true;
            this.columnProcess_mem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(46, 0);
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
            this.label7.Location = new System.Drawing.Point(26, 318);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFreeSpace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewFreeSpace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDrive,
            this.columnSize,
            this.columnFreespace,
            this.columnFreePercentage});
            this.dataGridViewFreeSpace.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewFreeSpace.Location = new System.Drawing.Point(11, 37);
            this.dataGridViewFreeSpace.Name = "dataGridViewFreeSpace";
            this.dataGridViewFreeSpace.ReadOnly = true;
            this.dataGridViewFreeSpace.RowHeadersVisible = false;
            this.dataGridViewFreeSpace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.RowTemplate.Height = 25;
            this.dataGridViewFreeSpace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewFreeSpace.Size = new System.Drawing.Size(292, 75);
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
            this.columnSize.Width = 62;
            // 
            // columnFreespace
            // 
            this.columnFreespace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnFreespace.HeaderText = "Free space (GB)";
            this.columnFreespace.Name = "columnFreespace";
            this.columnFreespace.ReadOnly = true;
            this.columnFreespace.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnFreespace.Width = 95;
            // 
            // columnFreePercentage
            // 
            this.columnFreePercentage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnFreePercentage.HeaderText = "Free (%)";
            this.columnFreePercentage.Name = "columnFreePercentage";
            this.columnFreePercentage.ReadOnly = true;
            this.columnFreePercentage.Width = 75;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel1.Controls.Add(this.email_alert);
            this.panel1.Controls.Add(this.vol_alert);
            this.panel1.Controls.Add(this.cpu_alert);
            this.panel1.Controls.Add(this.mem_alert);
            this.panel1.Location = new System.Drawing.Point(12, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 149);
            this.panel1.TabIndex = 23;
            // 
            // email_alert
            // 
            this.email_alert.AutoSize = true;
            this.email_alert.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.email_alert.Location = new System.Drawing.Point(148, 93);
            this.email_alert.Name = "email_alert";
            this.email_alert.Size = new System.Drawing.Size(106, 19);
            this.email_alert.TabIndex = 3;
            this.email_alert.Text = "Alert via e-mail";
            this.email_alert.UseVisualStyleBackColor = true;
            // 
            // vol_alert
            // 
            this.vol_alert.AutoSize = true;
            this.vol_alert.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.vol_alert.Location = new System.Drawing.Point(12, 93);
            this.vol_alert.Name = "vol_alert";
            this.vol_alert.Size = new System.Drawing.Size(66, 19);
            this.vol_alert.TabIndex = 2;
            this.vol_alert.Text = "Volume";
            this.vol_alert.UseVisualStyleBackColor = true;
            // 
            // cpu_alert
            // 
            this.cpu_alert.AutoSize = true;
            this.cpu_alert.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cpu_alert.Location = new System.Drawing.Point(12, 12);
            this.cpu_alert.Name = "cpu_alert";
            this.cpu_alert.Size = new System.Drawing.Size(49, 19);
            this.cpu_alert.TabIndex = 1;
            this.cpu_alert.Text = "CPU";
            this.cpu_alert.UseVisualStyleBackColor = true;
            // 
            // mem_alert
            // 
            this.mem_alert.AutoSize = true;
            this.mem_alert.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.mem_alert.Location = new System.Drawing.Point(12, 52);
            this.mem_alert.Name = "mem_alert";
            this.mem_alert.Size = new System.Drawing.Size(71, 19);
            this.mem_alert.TabIndex = 0;
            this.mem_alert.Text = "Memory";
            this.mem_alert.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 14);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(266, 130);
            this.dataGridView1.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel3.Controls.Add(this.dataGridViewFreeSpace);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(437, 540);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(313, 118);
            this.panel3.TabIndex = 26;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.dataGridViewProcessByCPU);
            this.panel4.Controls.Add(this.dataGridViewProcessByMem);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(765, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(332, 629);
            this.panel4.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Location = new System.Drawing.Point(12, 354);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(292, 149);
            this.panel5.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCPU);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtMem);
            this.panel2.Location = new System.Drawing.Point(448, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 107);
            this.panel2.TabIndex = 25;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_Server);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Controls.Add(this.targetServer);
            this.panel6.Controls.Add(this.server);
            this.panel6.Location = new System.Drawing.Point(24, 53);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(1108, 672);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Label server;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Drive;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewFreeSpaceTextBoxColumn1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewProcessByCPU;
        private System.Windows.Forms.DataGridView dataGridViewProcessByMem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPID_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessName_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcess_mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProcessCPU;
        private System.Windows.Forms.DataGridView dataGridViewFreeSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDrive;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreePercentage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox email_alert;
        private System.Windows.Forms.CheckBox vol_alert;
        private System.Windows.Forms.CheckBox cpu_alert;
        private System.Windows.Forms.CheckBox mem_alert;
        private System.Windows.Forms.DataGridView dataGridView1;
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
    }
}

