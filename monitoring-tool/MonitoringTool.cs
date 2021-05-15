using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MonitoringTool : Form
    {
        public MonitoringTool()
        {
            InitializeComponent();
            //ActiveForm.Text = "Monitoring tool";
        }

        private void runCustomScript_Click(object sender, EventArgs e)
        {

            CustomScript f2 = new CustomScript();
            f2.Show();
        }
    }

}
