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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = Form1.mgrName + " V" + Form1.mgrVer;
            BuildDisabledModsList();
        }

        private void BuildDisabledModsList()
        {
            checkedListBox1.Items.Clear();

            List<string> disabledMods = Directory.GetFiles(Form1.gameDir + @"Falcon\Content\Paks\~mods\DISABLED").ToList();

            foreach(var mod in disabledMods)
            {
                var modName = mod.Split('\\').Last();
                checkedListBox1.Items.Add(modName);
            }

            if(checkedListBox1.Items.Count == 0)
            {
                checkedListBox1.Items.Add("You Have No Disabled Mods");
                checkedListBox1.Enabled = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                if(checkedListBox1.CheckedItems.Count == 0)
                {
                    return;
                }

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    var file = Path.GetFullPath(Form1.gameDir + @"Falcon\Content\Paks\~mods\DISABLED\" + item.ToString());
                    File.Move(file, Form1.gameDir + @"Falcon\Content\Paks\~mods\" + item.ToString());
                    i++;
                }

                MessageBox.Show("Re-Enabled " + i.ToString() + " Mods!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex + "\n" + ex.StackTrace, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }
    }
}
