using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZScreamMagic
{
    public partial class TilesViewer : Form
    {
        Rom rom;
        

        public TilesViewer(Rom rom)
        {
            InitializeComponent();
            this.rom = rom;
        }
        //What do we need for a tile viewer?
        //Load Tile8 Gfx
        //Load Tile16 Data
        //Load Tile32 Data?
        
        private void TilesViewer_Load(object sender, EventArgs e)
        {
            Tile16[] tiles16 = TilesLoader.LoadTile16(rom);

           
        }

        private void mainTilesDisplay_Paint(object sender, PaintEventArgs e)
        {
            //overworldPaletteGroup1
            drawMainPalette(e.Graphics, 0);
            drawAux1Palette(e.Graphics, 4);
            drawAux2Palette(e.Graphics, 2);
            drawAnimatedPalette(e.Graphics, 7);

        }


        //============================================================================
        //DEBUGING CODE
        //============================================================================

        public void drawMainPalette(Graphics g, int index)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    g.FillRectangle(new SolidBrush(ZGraphics.overworld_MainPalettes[index][x + (y*7)]), new Rectangle((x * 16), (y * 16)+(32), 16, 16));
                }
            }
        }

        public void drawAux1Palette(Graphics g, int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    g.FillRectangle(new SolidBrush(ZGraphics.overworld_AuxPalettes[index][x + (y * 7)]), new Rectangle((x * 16)+(8*16), (y * 16) + (32), 16, 16));
                }
            }
        }

        public void drawAux2Palette(Graphics g, int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    g.FillRectangle(new SolidBrush(ZGraphics.overworld_AuxPalettes[index][x + (y * 7)]), new Rectangle((x * 16) + (8 * 16), (y * 16) +(5*16), 16, 16));
                }
            }
        }

        public void drawAnimatedPalette(Graphics g, int index)
        {
            for (int y = 0; y < 1; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    g.FillRectangle(new SolidBrush(ZGraphics.overworld_AnimatedPalettes[index][x + (y * 7)]), new Rectangle((x * 16), (y * 16) + (7*16), 16, 16));
                }
            }
        }


    }
}
