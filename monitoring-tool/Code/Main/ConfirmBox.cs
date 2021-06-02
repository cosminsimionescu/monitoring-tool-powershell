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
            targetServer_name.Text = InstanceMain.targetServer.Text; // display on the ConfirmBox the name of the server chosen for connecting
        }

        public void buttonConfirmServ_Click(object sender, EventArgs e) //after confirming bring to front the MainForm and close curent window
        {
            MainForm InstanceMain = MainForm.GetInstance();
            InstanceMain.Monitor_loop();

            if (!InstanceMain.Visible)  //Bring to front MainForm
            {
                InstanceMain.Show();
            }
            else
            {
                InstanceMain.BringToFront();
            }
            Close();
        }

        private void buttonRefuseServ_Click(object sender, EventArgs e) //Button cancel
        {
            MainForm InstanceMain = MainForm.GetInstance();  
            InstanceMain.targetServer.Enabled = true; //enable back text box for server input
            InstanceMain.btn_Server.Enabled = true; // enable conect button
            InstanceMain.btn_Server.Visible = true; // show connect button

            if (!InstanceMain.Visible)   //Bring to front MainForm
            {
                InstanceMain.Show();
            }
            else
            {
                InstanceMain.BringToFront();
            }
        }
    }
}
