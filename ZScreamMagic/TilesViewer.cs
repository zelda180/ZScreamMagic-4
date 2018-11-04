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
            Color[] palettes = Graphics.ReadPalette(rom.romData, RomConstants.overworldPaletteMain, 35);
            Color[] palettes = Graphics.ReadPalette(rom.romData, RomConstants.overworldPaletteAuxialiary, 21);
            Color[] palettes = Graphics.ReadPalette(rom.romData, RomConstants.overworldPaletteGroup1, 105);
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(palettes[x + (y * 7)]), new Rectangle(x * 16, y * 16, 16, 16));
                }
            }

        }
    }
}
