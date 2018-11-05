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
    public partial class ProjectForm : Form
    {
        //ROM DATA IS Tied to that form
        Rom romManager = new Rom();
        public ProjectForm(FileStream fs)
        {
            InitializeComponent();
            romManager.LoadRom(fs);
            
        }
        
        private void ProjectForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 132; i++)
            {
                projectTreeview.Nodes["Node_Dungeon"].Nodes.Add("Entrance " + i.ToString("X2"));
            }
        }

        public void Save(FileStream fs)
        {
            romManager.SaveRom(fs);
        }

        private void projectTreeview_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "tileViewer")
            {
                var tilesViewer = new TilesViewer(romManager);
                tilesViewer.MdiParent = this.MdiParent;
                tilesViewer.Show();


            }
        }
    }
}
