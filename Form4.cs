using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moonbase_s_Reignited_Mod_Manager
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = Form1.mgrName + " V" + Form1.mgrVer;
            BuildEnabledModsList();
        }

        private void BuildEnabledModsList()
        {
            checkedListBox1.Items.Clear();

            List<string> enabledMods = Directory.GetFiles(Form1.gameDir + @"Falcon\Content\Paks\~mods").ToList();

            foreach(var mod in enabledMods)
            {
                var modName = mod.Split('\\').Last();
                checkedListBox1.Items.Add(modName);
            }

            if (checkedListBox1.Items.Count == 0)
            {
                checkedListBox1.Items.Add("You Have No Enabled Mods");
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

                foreach(var item in checkedListBox1.CheckedItems)
                {
                    var mod = Path.GetFullPath(Form1.gameDir + @"Falcon\Content\Paks\~mods\" + item.ToString());
                    File.Move(mod, Form1.gameDir + @"Falcon\Content\Paks\~mods\DISABLED\" + item.ToString());
                    i++;
                }

                MessageBox.Show("Disabled " + i.ToString() + " Mods!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
