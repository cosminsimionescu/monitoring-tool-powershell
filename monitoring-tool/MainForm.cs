using System;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        string memoryUsage;
        string outputMem;

        string cpuUsage;
        string outputCpu;
        
        string volumeUsage;
        string outputVol;

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

            if (targetServer.Text.Trim() != "")
            {
                GetMemory();
                GetCPU();
                GetVolume();
            }
            else
            {
                textBox1.Text = "Please make sure you completed all fields";
            }
        }

        public void GetMemory()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts memS = new Scripts();

            memoryUsage = memS.MemoryScript();
            outputMem = newSession.NewPsSession(targetServer.Text, memoryUsage);

            textBox1.Text = outputMem; //memory usage
        }
        public void GetCPU()
        {
            RemoteSession newSession = new RemoteSession();
            Scripts cpuS = new Scripts();

            cpuUsage = cpuS.cpuScript();
            outputCpu = newSession.NewPsSession(targetServer.Text, cpuUsage);

            txtCPU.Text = outputCpu; //CPU usage
        }
        public void GetVolume() 
        {
            RemoteSession newSession = new RemoteSession();
            Scripts volumeS = new Scripts();

            volumeUsage = volumeS.volumeScript();
            outputVol = newSession.NewPsSession(targetServer.Text, volumeUsage);

            txtVolume.Text = outputVol; //volume usage
        }
    }

    
}
