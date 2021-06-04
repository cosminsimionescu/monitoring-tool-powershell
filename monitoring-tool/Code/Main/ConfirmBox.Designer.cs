
namespace monitoring_tool
{
    partial class ConfirmBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonConfirmServ = new System.Windows.Forms.Button();
            this.buttonRefuseServ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.targetServer_name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonConfirmServ
            // 
            this.buttonConfirmServ.BackColor = System.Drawing.Color.White;
            this.buttonConfirmServ.Location = new System.Drawing.Point(79, 76);
            this.buttonConfirmServ.Name = "buttonConfirmServ";
            this.buttonConfirmServ.Size = new System.Drawing.Size(59, 23);
            this.buttonConfirmServ.TabIndex = 0;
            this.buttonConfirmServ.Text = "Yes";
            this.buttonConfirmServ.UseVisualStyleBackColor = false;
            this.buttonConfirmServ.Click += new System.EventHandler(this.buttonConfirmServ_Click);
            // 
            // buttonRefuseServ
            // 
            this.buttonRefuseServ.BackColor = System.Drawing.Color.White;
            this.buttonRefuseServ.Location = new System.Drawing.Point(157, 76);
            this.buttonRefuseServ.Name = "buttonRefuseServ";
            this.buttonRefuseServ.Size = new System.Drawing.Size(63, 23);
            this.buttonRefuseServ.TabIndex = 1;
            this.buttonRefuseServ.Text = "No";
            this.buttonRefuseServ.UseVisualStyleBackColor = false;
            this.buttonRefuseServ.Click += new System.EventHandler(this.buttonRefuseServ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server you want to monitor is:";
            // 
            // targetServer_name
            // 
            this.targetServer_name.AutoSize = true;
            this.targetServer_name.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.targetServer_name.Location = new System.Drawing.Point(191, 41);
            this.targetServer_name.Name = "targetServer_name";
            this.targetServer_name.Size = new System.Drawing.Size(89, 15);
            this.targetServer_name.TabIndex = 3;
            this.targetServer_name.Text = "SERVER-NAME";
            // 
            // ConfirmBox
            // 
            this.AcceptButton = this.buttonConfirmServ;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(195)))), ((int)(((byte)(196)))));
            this.CancelButton = this.buttonRefuseServ;
            this.ClientSize = new System.Drawing.Size(301, 129);
            this.ControlBox = false;
            this.Controls.Add(this.targetServer_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRefuseServ);
            this.Controls.Add(this.buttonConfirmServ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ConfirmBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm";
            this.Load += new System.EventHandler(this.ConfirmBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConfirmServ;
        private System.Windows.Forms.Button buttonRefuseServ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label targetServer_name;
    }
}