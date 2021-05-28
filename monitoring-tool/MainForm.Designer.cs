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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.txtMem = new System.Windows.Forms.TextBox();
            this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runCustomScript = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.server = new System.Windows.Forms.Label();
            this.targetServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCPU
            // 
            this.txtCPU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCPU.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCPU.Location = new System.Drawing.Point(468, 458);
            this.txtCPU.Multiline = true;
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.ReadOnly = true;
            this.txtCPU.Size = new System.Drawing.Size(55, 35);
            this.txtCPU.TabIndex = 8;
            // 
            // txtMem
            // 
            this.txtMem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMem.Location = new System.Drawing.Point(468, 412);
            this.txtMem.Multiline = true;
            this.txtMem.Name = "txtMem";
            this.txtMem.ReadOnly = true;
            this.txtMem.Size = new System.Drawing.Size(55, 34);
            this.txtMem.TabIndex = 9;
            // 
            // optionsMenu
            // 
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exit});
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(61, 20);
            this.optionsMenu.Text = "&Options";
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(93, 22);
            this.exit.Text = "E&xit";
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
            this.menu.Size = new System.Drawing.Size(960, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuitem1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(12, 94);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 58);
            this.textBox1.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(237, 56);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(67, 24);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // server
            // 
            this.server.AutoSize = true;
            this.server.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.server.Location = new System.Drawing.Point(12, 59);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(142, 15);
            this.server.TabIndex = 4;
            this.server.Text = "&Server name or IP address";
            // 
            // targetServer
            // 
            this.targetServer.Location = new System.Drawing.Point(160, 57);
            this.targetServer.Name = "targetServer";
            this.targetServer.Size = new System.Drawing.Size(71, 23);
            this.targetServer.TabIndex = 5;
            this.targetServer.Text = "172.24.1.154";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(325, 461);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "CPU usage";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(325, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Memory usage";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(-97, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Output";
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
            this.label5.Location = new System.Drawing.Point(325, 525);
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
            this.dataGridViewProcessByCPU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProcessByCPU.BackgroundColor = System.Drawing.Color.White;
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
            this.dataGridViewProcessByCPU.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewProcessByCPU.Location = new System.Drawing.Point(643, 61);
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
            this.dataGridViewProcessByMem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProcessByMem.BackgroundColor = System.Drawing.Color.White;
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
            this.dataGridViewProcessByMem.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewProcessByMem.Location = new System.Drawing.Point(643, 379);
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
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(676, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "Top processes by CPU usage";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(656, 351);
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
            this.dataGridViewFreeSpace.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridViewFreeSpace.Location = new System.Drawing.Point(325, 553);
            this.dataGridViewFreeSpace.Name = "dataGridViewFreeSpace";
            this.dataGridViewFreeSpace.ReadOnly = true;
            this.dataGridViewFreeSpace.RowHeadersVisible = false;
            this.dataGridViewFreeSpace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFreeSpace.RowTemplate.Height = 25;
            this.dataGridViewFreeSpace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewFreeSpace.Size = new System.Drawing.Size(292, 103);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(960, 686);
            this.Controls.Add(this.dataGridViewFreeSpace);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridViewProcessByMem);
            this.Controls.Add(this.dataGridViewProcessByCPU);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMem);
            this.Controls.Add(this.txtCPU);
            this.Controls.Add(this.targetServer);
            this.Controls.Add(this.server);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Aplicatie pentru monitorizare";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProcessByMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).EndInit();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label server;
        private System.Windows.Forms.TextBox targetServer;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
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
    }
}

