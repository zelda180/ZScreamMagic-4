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
        public Rom romManager = new Rom();
        public ProjectForm(FileStream fs)
        {
            InitializeComponent();
            romManager.LoadRom(fs);
            addPalettesNodes("sword_pal", 4, "Sword ",Palettes.swords_Palettes);
            addPalettesNodes("shield_pal", 3, "Shield ", Palettes.shields_Palettes);
            addPalettesNodes("mail_pal", 5, "Mail ", Palettes.armors_Palettes,new string[] {"Green Mail","Blue Mail","Red Mail","Bunny","Electrocuted" });
            addPalettesNodes("mainoverworld_pal", 6, "Main Overworld ", Palettes.overworld_MainPalettes);
            addPalettesNodes("auxoverworld_pal", 20, "Aux Overworld ", Palettes.overworld_AuxPalettes);
            addPalettesNodes("animatedoverworld_pal", 14, "Animated Overworld ", Palettes.overworld_AnimatedPalettes);
            addPalettesNodes("hud_pal", 1, "In Game", Palettes.HudPalettes);
            addPalettesNodes("hud_pal", 1, "Intro", Palettes.HudPalettes);
            addPalettesNodes("mainglobalsprite_pal", 1, "Light World", Palettes.globalSprite_Palettes);
            addPalettesNodes("mainglobalsprite_pal", 1, "Dark World", Palettes.globalSprite_Palettes);
            addPalettesNodes("dungeons_pal", 20, "Dungeon ", Palettes.dungeonsMain_Palettes);
            addPalettesNodes("spriteaux1_pal", 12, "Aux1 Sprite ", Palettes.spritesAux1_Palettes);
            addPalettesNodes("spriteaux2_pal", 11, "Aux2 Sprite ", Palettes.spritesAux2_Palettes, new string[11] {null,null,null,null,null,null,"Rocks,Sign,Bushes,Pegs LW",null,"Rocks,Sign,Bushes,Pegs DW",null,null });
            addPalettesNodes("spriteaux3_pal", 24, "Aux3 Sprite ", Palettes.spritesAux3_Palettes);


        }

        private void addPalettesNodes(string parent, int count, string name, Color[][] linkColor,string[] names=null)
        {
            for(int i = 0; i<count;i++)
            {
                TreeNode tn = new TreeNode();
                if (count != 1)
                {
                    tn.Text = name + i.ToString("X2");
                }
                else
                {
                    tn.Text = name;
                }
                tn.Tag = linkColor;
                if (names != null)
                {
                    if (names[i] != null)
                    {
                        tn.Text = names[i];
                    }
                }
                projectTreeview.Nodes["Node_Palettes"].Nodes[parent].Nodes.Add(tn);
            }
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
            if (e.Node.Name == "Node_Overworld")
            {
                var tilesViewer = new TilesViewer(romManager);
                tilesViewer.MdiParent = this.MdiParent;
                tilesViewer.Show();
            }
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Parent != null)
                {
                    if (e.Node.Parent.Parent.Name == "Node_Palettes")
                    {
                        var paletteEditor = new PaletteEditorSingle();
                        paletteEditor.tempColors = new Color[(e.Node.Tag as Color[][])[e.Node.Index].Length];
                        Array.Copy((e.Node.Tag as Color[][])[e.Node.Index], paletteEditor.tempColors, (e.Node.Tag as Color[][])[e.Node.Index].Length);
                        if (paletteEditor.ShowDialog() == DialogResult.OK)
                        {
                            (e.Node.Tag as Color[][])[e.Node.Index] = paletteEditor.tempColors;
                        }
                    }
                }
            }
        }
    }
}
