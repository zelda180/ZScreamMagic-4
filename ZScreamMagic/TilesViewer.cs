using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        OverworldMap[] map = new OverworldMap[64];

        //Those Pointers data get filled with data everytime a new map is drawn

        IntPtr tiles16GfxPtr = Marshal.AllocHGlobal((128 * 7520)); //8bpp - Different for every maps
        Bitmap tiles16Bitmap;
        IntPtr tiles8GfxPtr = Marshal.AllocHGlobal((128 * 512) / 2); //4bpp - Different for every maps
        Bitmap tiles8Bitmap;


        public TilesViewer(Rom rom)
        {
            InitializeComponent();
            this.rom = rom;
            TilesLoader.LoadTile16(rom);
            TilesLoader.LoadTile32(rom);
        }
        //What do we need for a tile viewer?
        //Load Tile8 Gfx
        //Load Tile16 Data
        //Load Tile32 Data?
       
        private unsafe void TilesViewer_Load(object sender, EventArgs e)
        {


            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Need to load map data 

            for (int j = 0; j < 64; j++)
            {
                tiles8Bitmap = new Bitmap(128, 7136, 64, PixelFormat.Format4bppIndexed, tiles8GfxPtr);
                tiles16Bitmap = new Bitmap(128, 7520, 128, PixelFormat.Format8bppIndexed, tiles16GfxPtr);
                map[j] = new OverworldMap(rom, j);
                //allgfx16Ptr
                unsafe
                {
                    byte* newPdata = (byte*)ZGraphics.allgfx16Ptr.ToPointer();
                    byte* sheetsData = (byte*)tiles8GfxPtr.ToPointer();
                    int sheetPos = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        int d = 0;
                        while (d < 2048)
                        {
                            byte mapByte = newPdata[d + (map[j].blocksets[i] * 2048)];
                            switch (i)
                            {
                                case 0:
                                case 3:
                                case 4:
                                case 5:
                                    mapByte += 0x88;
                                    break;
                            }

                            sheetsData[d + (sheetPos * 2048)] = mapByte;
                            d++;
                        }
                        sheetPos++;
                    }

                }

                ColorPalette cp = tiles16Bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = map[j].palettes[i];
                }
                tiles16Bitmap.Palette = cp;
                map[j].mapBitmap.Palette = cp;

                BuildTiles16Gfx(tiles8GfxPtr, tiles16GfxPtr);

                UpdateTilesMap(map[j].mapGfxPtr,j);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            //mainTilesDisplay.Image = mapBitmap;

            //mainTilesDisplay.Image = b;
            //mainTilesDisplay.Refresh();
            //Need to load Palette Map Data

            //Need to load Blockset Map Data

            //-> will be able to draw a tile at this point
        }



        private unsafe void BuildTiles16Gfx(IntPtr allgfx8Ptr, IntPtr allgfx16Ptr)
        {
            var gfx16Data = (byte*)allgfx16Ptr.ToPointer();
            var gfx8Data = (byte*)allgfx8Ptr.ToPointer();
            int[] offsets = { 0, 8, 1024, 1032 };
            var yy = 0;
            var xx = 0;

            //TODO : Get rid of that magic number
            for (var i = 0; i < 3748; i++) //number of tiles16
            {
                //8x8 tile draw
                //gfx8 = 4bpp so everyting is /2
                var tiles = TilesLoader.tiles16[i];

                for (var tile = 0; tile < 4; tile++)
                {
                    Tile8Data info = tiles.tile8data[tile];
                    int offset = offsets[tile];

                    for (var y = 0; y < 8; y++)
                    {
                        for (var x = 0; x < 4; x++)
                        {
                            CopyTile(x, y, xx, yy, offset, info, gfx16Data, gfx8Data);
                        }
                    }
                }

                xx += 16;
                if (xx >= 128)
                {
                    yy += 2048;
                    xx = 0;
                }
            }
        }

        private unsafe void CopyTile(int x, int y, int xx, int yy, int offset, Tile8Data tile, byte* gfx16Pointer, byte* gfx8Pointer)
        {
            int mx = x;
            int my = y;
            byte r = 0;

            if (tile.mirror_h)
            {
                mx = 3 - x;
                r = 1;
            }
            if (tile.mirror_v)
            {
                my = 7 - y;
            }

            int tx = ((tile.id / 16) * 512) + ((tile.id - ((tile.id / 16) * 16)) * 4);
            var index = xx + yy + offset + (mx * 2) + (my * 128);
            var pixel = gfx8Pointer[tx + (y * 64) + x];

            gfx16Pointer[index + r ^ 1] = (byte)((pixel & 0x0F) + tile.palette * 16);
            gfx16Pointer[index + r] = (byte)(((pixel >> 4) & 0x0F) + tile.palette * 16);
        }

        private unsafe void UpdateTilesMap(IntPtr mapPtr, int mid)
        {
            byte* mapData = (byte*)mapPtr.ToPointer();
            byte* tile16Data = (byte*)tiles16GfxPtr.ToPointer();


                int x = 0;
                int y = 0;
                for (int i = 0; i < (32 * 32); i++)
                {
                    int srcy = (map[mid].tiles[i] / 8);
                    int srcx = map[mid].tiles[i] - (srcy * 8);

                    //e.Graphics.DrawImage(tiles16Bitmap, x * 16, y * 16, new Rectangle(srcx * 16, srcy * 16, 16, 16), GraphicsUnit.Pixel);
                    for (int j = 0; j < 16; j++)//tilecopy
                    {
                        for (int k = 0; k < 16; k++)//tilecopy
                        {
                            mapData[((x * 16) + (y * 8192)) + (j * 512) + k] = tile16Data[((srcx * 16) + (srcy * 2048)) + (j * 128) + k];
                        }
                    }


                    x++;
                    if (x >= 32)
                    {
                        x = 0;
                        y++;
                    }
                }
        }



        private void mainTilesDisplay_Paint(object sender, PaintEventArgs e)
        {
            int mid = 0;
            for (int sy = 0; sy < 8; sy++)
            {
                for (int sx = 0; sx < 8; sx++)
                {
                    e.Graphics.DrawImage(map[mid].mapBitmap, sx * 512, sy * 512);
                    mid++;
                }
            }

            //overworldPaletteGroup1
            //drawMainPalette(e.Graphics, 0);
            //drawAux1Palette(e.Graphics, 4);
            //drawAux2Palette(e.Graphics, 2);
            //drawAnimatedPalette(e.Graphics, 7);

        }


        //============================================================================
        //DEBUGING CODE
        //============================================================================

 


    }
}
