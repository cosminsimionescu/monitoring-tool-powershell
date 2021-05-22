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
            RunCustomScript psOutput = new RunCustomScript();

            txtOutput.Text = psOutput.Run(txtInput.Text);
        }
    }
}
