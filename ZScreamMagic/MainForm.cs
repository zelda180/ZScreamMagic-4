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

namespace ZScreamMagic
{
    public partial class MainForm : Form
    {

        ProjectForm projectForm;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        
        private void loadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = "Snes ROM |*.sfc|Snes ROM |*.smc";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(fd.FileName))
                    {
                        MessageBox.Show("Specified file does not exist", "Invalid Filename", MessageBoxButtons.OK);
                        return;
                    }

                    using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        projectForm = new ProjectForm(fs);
                        projectForm.ShowIcon = false;
                        projectForm.MdiParent = this;
                        projectForm.Show();
                    }
                    
                }
            }

                
        }

        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fd = new SaveFileDialog())
            {
                fd.Filter = "Snes ROM |*.sfc|Snes ROM |*.smc";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = new FileStream(fd.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        if (projectForm != null)
                        {
                            projectForm.Save(fs);
                        }
                    }
                }
            }
        }
    }
}
