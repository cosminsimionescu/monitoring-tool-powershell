﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace monitoring_tool
{
    public partial class MainForm : Form
    {
        string memoryUsage, outputMem;

        string cpuUsage, outputCpu;

        string volumeUsage, outputVol;

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
            //dataGridViewFreeSpace.Columns.Add("columnDrive","Drive");
            //dataGridViewFreeSpace.Columns.Add("columnFreespace", "Free space");
            
            RemoteSession newSession = new RemoteSession();
            Scripts volumeS = new Scripts();

            Dictionary<string, string> driveInformations = new Dictionary<string, string>();

            volumeUsage = volumeS.volumeScript();
            outputVol = newSession.NewPsSession(targetServer.Text, volumeUsage);

            var corectLines = outputVol.Split('\n')
                             .Where(l=>l!="\r").ToList();
            corectLines.Remove("");

            for(int i=0;i<corectLines.Count();i+=4)
            {
                string driveId = corectLines[i+3].Split(':')[1].Trim();
                string driveSize = corectLines[i].Split(':')[1].Trim();
                string driveSpace = corectLines[i + 1].Split(':')[1].Trim();
                string driveSpacePercentage = corectLines[i + 2].Split(':')[1].Trim().Split('\r')[0];
                
                dataGridViewFreeSpace.Rows.Add(driveId, driveSize, driveSpace, driveSpacePercentage);
                driveInformations.Add(driveId, driveSpacePercentage);
            }
            
        }
    }

    
}
