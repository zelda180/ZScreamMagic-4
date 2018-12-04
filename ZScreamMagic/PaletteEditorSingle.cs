using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZScreamMagic
{
    public partial class PaletteEditorSingle : Form
    {
        public PaletteEditorSingle()
        {
            InitializeComponent();
        }
        public Color[] tempColors;
        public byte type = 0;
        ColorDialog cd = new ColorDialog();
        private void PaletteEditorSingle_Load(object sender, EventArgs e)
        {
            if (tempColors.Length == 15 || tempColors.Length == 90 || tempColors.Length == 60)
            {
                type = 1;//mails
            }
            else if (tempColors.Length == 32)
            {
                type = 2; //hude
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Except dungeons which are 90 colors long, draw colors X 7 max
            int x = 0;
            int y = 0;


            //Normal draw 7 color max per line
            if (type == 0)
            {
                for (int i = 0; i < tempColors.Length; i++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(tempColors[i]), new Rectangle(x * 16, y * 16, 16, 16));

                    x++;
                    if (x >= 7)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            else if (type == 1) //15 color per line (mails and dungeons)
            {
                for (int i = 0; i < tempColors.Length; i++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(tempColors[i]), new Rectangle(x * 16, y * 16, 16, 16));

                    x++;
                    if (x >= 15)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            else if (type == 2) //4 color per line (hud)
            {
                for (int i = 0; i < tempColors.Length; i++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(tempColors[i]), new Rectangle(x * 16, y * 16, 16, 16));

                    x++;
                    if (x >= 4)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int x = (e.X / 16);
            int y = (e.Y / 16);
            int id = (y * 7) + x;

            if (type == 1) //15 color per line (mails and dungeons)
            {
                id = (y * 15) + x;
            }
            else if (type == 2) //4 color per line (hud)
            {
                id = (y * 4) + x;
            }
            cd.Color = tempColors[id];
            if (cd.ShowDialog() == DialogResult.OK)
            {
                tempColors[id] = cd.Color;
            }

            pictureBox1.Refresh();
        }
    }
}
