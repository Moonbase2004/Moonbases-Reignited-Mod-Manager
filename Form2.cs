using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Moonbase_s_Reignited_Mod_Manager
{
    public partial class Form2 : Form
    {
        private Form1 mainMenu = new Form1();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = Form1.mgrName + " V" + Form1.mgrVer;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            Process gameProcess = Process.Start(Form1.gameDir + @"Spyro.exe");
            gameProcess.WaitForExit();
            MessageBox.Show("The Game Exited With Code " + gameProcess.ExitCode + ".");
            Close();
            mainMenu.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Hide();
            Directory.CreateDirectory(Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED");
            List<string> mods = Directory.GetFiles(Form1.gameDir + @"Falcon\Content\Paks\~mods").ToList();
            foreach(var mod in mods)
            {
                File.Move(mod, Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED\" + mod.Split('\\').Last());
            }
            Process gameProcess = Process.Start(Form1.gameDir + @"Spyro.exe");
            gameProcess.WaitForExit();
            MessageBox.Show("The Game Exited With Code " + gameProcess.ExitCode + ".");
            List<string> modsToMoveBack = Directory.GetFiles(Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED").ToList();
            foreach (var mod in modsToMoveBack)
            {
                File.Move(mod, Form1.gameDir + @"Falcon\Content\Paks\~mods\" + mod.Split('\\').Last());
            }
            Directory.Delete(Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED");
            Close();
            mainMenu.ShowDialog();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            Process gameProcess = Process.Start(Form1.gameDir + @"Spyro.exe");
            await Task.Delay(1000);
            ProcessStartInfo consoleProcessInfo = new ProcessStartInfo
            {
                WorkingDirectory = @".\console",
                FileName = @"IGCSInjector.exe"
            };
            Process consoleHaccProcess = Process.Start(consoleProcessInfo);
            gameProcess.WaitForExit();
            consoleHaccProcess.Kill();
            consoleHaccProcess.WaitForExit();
            MessageBox.Show("The Game Exited With Code " + gameProcess.ExitCode + ". The Console Hacc exited with Code " + consoleHaccProcess.ExitCode + ".");
            Close();
            mainMenu.ShowDialog();
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            Directory.CreateDirectory(Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED");
            List<string> mods = Directory.GetFiles(Form1.gameDir + @"Falcon\Content\Paks\~mods").ToList();
            foreach (var mod in mods)
            {
                File.Move(mod, Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED\" + mod.Split('\\').Last());
            }
            Process gameProcess = Process.Start(Form1.gameDir + @"Spyro.exe");
            await Task.Delay(1000);
            ProcessStartInfo consoleHaccProcessInfo = new ProcessStartInfo
            {
                WorkingDirectory = @".\console",
                FileName = @"IGCSInjector.exe"
            };
            Process consoleHaccProcess = Process.Start(consoleHaccProcessInfo);
            gameProcess.WaitForExit();
            consoleHaccProcess.Kill();
            consoleHaccProcess.WaitForExit();
            MessageBox.Show("The Game Exited With Code " + gameProcess.ExitCode + ". The Console Hacc exited with Code " + consoleHaccProcess.ExitCode + ".");
            List<string> modsToMoveBack = Directory.GetFiles(Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED").ToList();
            foreach (var mod in modsToMoveBack)
            {
                File.Move(mod, Form1.gameDir + @"Falcon\Content\Paks\~mods\" + mod.Split('\\').Last());
            }
            Directory.Delete(Form1.gameDir + @"Falcon\Content\Paks\~mods\TEMP_DISABLED");
            Close();
            mainMenu.ShowDialog();
        }
    }
}
