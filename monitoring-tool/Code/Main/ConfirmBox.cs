using System;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class ConfirmBox : Form
    {
        private static ConfirmBox InstanceConfirmBox;
        public static ConfirmBox GetInstanceConfirmBox()
        {
            if (InstanceConfirmBox == null) InstanceConfirmBox = new ConfirmBox();
            return InstanceConfirmBox;
        }

        public ConfirmBox()
        {
            InitializeComponent();
            InstanceConfirmBox = this;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
        }

        private void ConfirmBox_Load(object sender, EventArgs e)
        {
            MainForm InstanceMain = MainForm.GetInstance();
            targetServer_name.Text = InstanceMain.targetServer.Text;
        }

        public void buttonConfirmServ_Click(object sender, EventArgs e)
        {
            MainForm InstanceMain = MainForm.GetInstance();
            InstanceMain.Monitor_loop();

            if (!InstanceMain.Visible)
            {
                InstanceMain.Show();
            }
            else
            {
                InstanceMain.BringToFront();
            }
            Close();
        }

        private void buttonRefuseServ_Click(object sender, EventArgs e)
        {
            MainForm main = MainForm.GetInstance();
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
