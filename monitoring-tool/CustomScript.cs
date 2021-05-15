using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class CustomScript : Form
    {
        public CustomScript()
        {
            InitializeComponent();
            //ActiveForm.Text = "Monitoring tool";
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            RunCustomScript psOutput = new RunCustomScript();
            txtOutput.Text = psOutput.Run(txtInput.Text);
        }
    }
}
