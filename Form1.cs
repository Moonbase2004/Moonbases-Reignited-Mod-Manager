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

namespace Moonbase_s_Reignited_Mod_Manager
{
    public partial class Form1 : Form
    {

        public static string mgrName = "Moonbase's Reignited Mod Manager";
        public static string mgrVer = "1.0 BETA 1";
        public static string gameDir = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = mgrName + " V" + mgrVer;
        }

        private void ImportGameFiles(object sender, EventArgs e)
        {
            if(gameDir == null)
            {
                FileDialog gameExe = new OpenFileDialog();
                gameExe.Title = "Select The Game Executable";
                gameExe.Filter = "Spyro Game Executable (*Spyro.exe)|*Spyro.exe";
                gameExe.ShowDialog();
                gameDir = gameExe.FileName.Remove(gameExe.FileName.Length - 9, 9);

                if(Directory.Exists(gameDir + "Falcon"))
                {
                    if (!Directory.Exists(gameDir + @"Falcon\Content\Paks\~mods"))
                    {
                        var diagResult = MessageBox.Show("I detected that there is no '~mods' folder in your game files. Do you want me to create that folder for you?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (diagResult == DialogResult.Yes)
                        {
                            Directory.CreateDirectory(gameDir + @"Falcon\Content\Paks\~mods");
                            MessageBox.Show("Created Directory Successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Operation Cancelled; You Need To Have A '~mods' Folder");
                            return;
                        }
                    }

                    if (!Directory.Exists(gameDir + @"Falcon\Content\Paks\~mods\DISABLED"))
                    {
                        MessageBox.Show("I detected that there is no 'DISABLED' Folder in your game files. Some features in this mgr require this. Do you want me to create it for you?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        Directory.CreateDirectory(gameDir + @"Falcon\Content\Paks\~mods\DISABLED");
                        MessageBox.Show("Created Directory Successfully!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    BuildModsList();
                }

            } else
            {
                MessageBox.Show("You Already Imported The Game Files, Silly", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void BuildModsList()
        {
            if(gameDir != null)
            {
                listBox1.Items.Clear();

                List<string> mods = Directory.GetFiles(gameDir + @"Falcon\Content\Paks\~mods").ToList();

                int i = 0;
                int i2 = 0;

                foreach(var mod in mods)
                {
                    var modSplitted = mod.Split('\\').Last();
                    listBox1.Items.Add(modSplitted);
                    i++;
                }

                if(Directory.Exists(gameDir + @"Falcon\Content\Paks\DISABLED"))
                {
                    List<string> modsDisabled = Directory.GetFiles(gameDir + @"Falcon\Content\Paks\~mods\DISABLED").ToList();

                    foreach (var disMod in modsDisabled)
                    {
                        var disModSplitted = disMod.Split('\\').Last();
                        listBox1.Items.Add(disModSplitted + " (DISABLED)");
                        i2++;
                    }
                }

                if(listBox1.Items.Count == 0)
                {
                    listBox1.Items.Add("No Mods Detected. Import some mods damn it.");
                }
            } else
            {
                MessageBox.Show("Error; gameDir is null", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(gameDir == null)
            {
                return;
            }

            Form2 form2 = new Form2();
            Hide();
            form2.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(gameDir == null)
            {
                return;
            }

            FileDialog modToImport = new OpenFileDialog();
            modToImport.Title = "Select The Mod You Want To Import";
            modToImport.Filter = "Spyro RT Mod (*.pak)|*.pak";
            
            if(modToImport.ShowDialog() == DialogResult.OK)
            {
                if(File.Exists(gameDir + @"Falcon\Content\Paks\~mods\" + modToImport.FileName.Split('\\').Last())) 
                {
                    MessageBox.Show("Operation Cancelled; This Mod Is Already Imported And Enabled.");
                } else
                {
                    if(File.Exists(gameDir + @"Falcon\Content\Paks\~mods\DISABLED\" + modToImport.FileName.Split('\\').Last()))
                    {
                        MessageBox.Show("Operation Cancelled; This Mod Is Already Imported But Disabled. Please Re-Enable It.");
                    } else
                    {
                        File.Copy(modToImport.FileName, gameDir + @"Falcon\Content\Paks\~mods\" + modToImport.FileName.Split('\\').Last());
                        MessageBox.Show("Mod Imported To Your '~mods' Folder! You May Now Launch Your Game.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuildModsList();
                        return;
                    }
                }
            } else
            {
                MessageBox.Show("Operation Cancelled");
                return;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if(gameDir == null)
            {
                return;
            }

            Form3 form3 = new Form3();
            form3.ShowDialog();
            BuildModsList();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(gameDir == null)
            {
                return;
            }

            Form4 form4 = new Form4();
            form4.ShowDialog();
            BuildModsList();
        }
    }
}