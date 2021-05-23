using System;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class FormCustomScript : Form
    {
        public FormCustomScript()
        {
            InitializeComponent();
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            CustomScript();
        }
        private void CustomScript()
        {
            RunCustomScript session = new RunCustomScript();
            string outScript = session.Run(serverName.Text, txtInput.Text);
            txtOutput.Text = outScript;
        }
    }
}
