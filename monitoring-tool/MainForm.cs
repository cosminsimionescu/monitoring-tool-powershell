using System;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void runCustomScript_Click(object sender, EventArgs e)
        {
            FormCustomScript f2 = new FormCustomScript();
            f2.Show();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (targetServer.Text.Trim() != "" && txtCmd.Text.Trim() != "")
            {
                CreateSession();
            }
            else
            {
                textBox1.Text = "Please make sure you completed all fields";
            }
        }

        public void CreateSession()
        {
            RemoteSession newSession = new RemoteSession();
            string output = newSession.NewPsSession(targetServer.Text, txtCmd.Text);
            textBox1.Text = output;
        }
    }

    
}
