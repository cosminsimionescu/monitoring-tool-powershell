
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
            this.buttonConfirmServ.Location = new System.Drawing.Point(77, 76);
            this.buttonConfirmServ.Name = "buttonConfirmServ";
            this.buttonConfirmServ.Size = new System.Drawing.Size(59, 23);
            this.buttonConfirmServ.TabIndex = 0;
            this.buttonConfirmServ.Text = "Yes";
            this.buttonConfirmServ.UseVisualStyleBackColor = true;
            this.buttonConfirmServ.Click += new System.EventHandler(this.buttonConfirmServ_Click);
            // 
            // buttonRefuseServ
            // 
            this.buttonRefuseServ.Location = new System.Drawing.Point(161, 76);
            this.buttonRefuseServ.Name = "buttonRefuseServ";
            this.buttonRefuseServ.Size = new System.Drawing.Size(55, 23);
            this.buttonRefuseServ.TabIndex = 1;
            this.buttonRefuseServ.Text = "No";
            this.buttonRefuseServ.UseVisualStyleBackColor = true;
            this.buttonRefuseServ.Click += new System.EventHandler(this.buttonRefuseServ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server you want to monitor is:";
            // 
            // targetServer_name
            // 
            this.targetServer_name.AutoSize = true;
            this.targetServer_name.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.targetServer_name.Location = new System.Drawing.Point(194, 36);
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.CancelButton = this.buttonRefuseServ;
            this.ClientSize = new System.Drawing.Size(290, 129);
            this.ControlBox = false;
            this.Controls.Add(this.targetServer_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRefuseServ);
            this.Controls.Add(this.buttonConfirmServ);
            this.Name = "ConfirmBox";
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