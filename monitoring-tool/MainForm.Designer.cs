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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.txtMem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewFreeSpace = new System.Windows.Forms.DataGridView();
            this.columnDrive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFreePercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Drive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeSpace)).BeginInit();
            this.SuspendLayout();
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
            this.runCustomScript.Size = new System.Drawing.Size(148, 22);
            this.runCustomScript.Text = "Custom script";
            this.runCustomScript.Click += new System.EventHandler(this.runCustomScript_Click);
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
            this.menu.Size = new System.Drawing.Size(947, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuitem1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(617, 416);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(316, 106);
            this.textBox1.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(247, 37);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // server
            // 
            this.server.AutoSize = true;
            this.server.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.server.Location = new System.Drawing.Point(12, 40);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(142, 15);
            this.server.TabIndex = 4;
            this.server.Text = "&Server name or IP address";
            // 
            // targetServer
            // 
            this.targetServer.Location = new System.Drawing.Point(160, 37);
            this.targetServer.Name = "targetServer";
            this.targetServer.Size = new System.Drawing.Size(71, 23);
            this.targetServer.TabIndex = 5;
            this.targetServer.Text = "172.24.1.154";
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(727, 154);
            this.txtCmd.Multiline = true;
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(206, 18);
            this.txtCmd.TabIndex = 7;
            // 
            // txtCPU
            // 
            this.txtCPU.Location = new System.Drawing.Point(845, 77);
            this.txtCPU.Multiline = true;
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.Size = new System.Drawing.Size(75, 21);
            this.txtCPU.TabIndex = 8;
            // 
            // txtMem
            // 
            this.txtMem.Location = new System.Drawing.Point(738, 77);
            this.txtMem.Multiline = true;
            this.txtMem.Name = "txtMem";
            this.txtMem.Size = new System.Drawing.Size(91, 21);
            this.txtMem.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(845, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "CPU ussage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(738, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Memory ussage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(576, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Command line for testing";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(617, 394);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Output";
            // 
            // dataGridViewFreeSpace
            // 
            this.dataGridViewFreeSpace.AllowUserToAddRows = false;
            this.dataGridViewFreeSpace.AllowUserToDeleteRows = false;
            this.dataGridViewFreeSpace.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridViewFreeSpace.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFreeSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewFreeSpace.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFreeSpace.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(92)))));
            this.dataGridViewFreeSpace.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridViewFreeSpace.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(148)))), ((int)(((byte)(171)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(231)))), ((int)(((byte)(166)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFreeSpace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewFreeSpace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFreeSpace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDrive,
            this.columnSize,
            this.columnFreespace,
            this.columnFreePercentage});
            this.dataGridViewFreeSpace.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(186)))), ((int)(((byte)(186)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(175)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(255)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewFreeSpace.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewFreeSpace.GridColor = System.Drawing.SystemColors.InfoText;
            this.dataGridViewFreeSpace.Location = new System.Drawing.Point(12, 416);
            this.dataGridViewFreeSpace.Name = "dataGridViewFreeSpace";
            this.dataGridViewFreeSpace.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFreeSpace.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewFreeSpace.RowHeadersVisible = false;
            this.dataGridViewFreeSpace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFreeSpace.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewFreeSpace.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.dataGridViewFreeSpace.RowTemplate.Height = 25;
            this.dataGridViewFreeSpace.RowTemplate.ReadOnly = true;
            this.dataGridViewFreeSpace.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewFreeSpace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewFreeSpace.Size = new System.Drawing.Size(292, 98);
            this.dataGridViewFreeSpace.TabIndex = 16;
            this.dataGridViewFreeSpace.TabStop = false;
            // 
            // columnDrive
            // 
            this.columnDrive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDrive.FillWeight = 93.52285F;
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
            this.columnSize.FillWeight = 101.6426F;
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
            this.columnFreespace.FillWeight = 47.69386F;
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
            this.columnFreePercentage.FillWeight = 47.69386F;
            this.columnFreePercentage.HeaderText = "Free (%)";
            this.columnFreePercentage.Name = "columnFreePercentage";
            this.columnFreePercentage.ReadOnly = true;
            this.columnFreePercentage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnFreePercentage.Width = 75;
            // 
            // Drive
            // 
            this.Drive.HeaderText = "Drive";
            this.Drive.Name = "Drive";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Drive";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(12, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "Disk space";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(947, 526);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewFreeSpace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMem);
            this.Controls.Add(this.txtCPU);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.targetServer);
            this.Controls.Add(this.server);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Aplicatie pentru monitorizare";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
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
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtMem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridViewFreeSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Drive;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDrive;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFreePercentage;
        private System.Windows.Forms.Label label5;
    }
}

