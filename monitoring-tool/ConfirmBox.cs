using System;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class ConfirmBox : Form
    {
        MainForm main = MainForm.GetInstance();
        public ConfirmBox window { get; set; }

        public ConfirmBox()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
        }

        private void ConfirmBox_Load(object sender, EventArgs e)
        {
            targetServer_name.Text = MainForm.serverName;
        }

        public void buttonConfirmServ_Click(object sender, EventArgs e)
        {
            main.Monitor_loop();

            if (!main.Visible)
            {
                main.Show();
            }
            else
            {
                main.BringToFront();
            }
            Close();
        }

        private void buttonRefuseServ_Click(object sender, EventArgs e)
        {
            main.targetServer.Enabled = true;
            main.btn_Server.Enabled = true;
            main.btn_Server.Visible = true;

            if (!main.Visible)
            {
                main.Show();
            }
            else
            {
                main.BringToFront();
            }
        }
    }
}
