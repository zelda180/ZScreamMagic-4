using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public static class TilesLoader
    {
        static int[] map32address;
        public static List<Tile32> tiles32 = new List<Tile32>();
        public static List<Tile16> tiles16 = new List<Tile16>();
        public static void LoadTile16(Rom rom)
        {
            int tpos = RomConstants.map16Tiles;
            //TODO: Change that magic number
            for (int i = 0; i < 4096; i += 1)
            {
                var t0 = GetTile8(rom.readShort(tpos));
                var t1 = GetTile8(rom.readShort(tpos + 2));
                var t2 = GetTile8(rom.readShort(tpos + 4));
                var t3 = GetTile8(rom.readShort(tpos + 6));
                tpos += 8;
                tiles16.Add(new Tile16(new Tile8Data[4]{ t0, t1, t2, t3 }));
            }
        }

        private enum Dimension
        {
            map32TilesTL = 0,
            map32TilesTR = 1,
            map32TilesBL = 2,
            map32TilesBR = 3
        }

        public static void LoadTile32(Rom rom)
        {
            map32address = new int[]
            {
            RomConstants.map32TilesTL,
            RomConstants.map32TilesTR,
            RomConstants.map32TilesBL,
            RomConstants.map32TilesBR
            };

            const int dim = 4;

            for (int i = 0; i < 0x33F0; i += 6)
            {
                ushort[,] b = new ushort[dim, dim];
                ushort tl, tr, bl, br;
                for (int k = 0; k < 4; k++)
                {
                    tl = generate(i, k, (int)Dimension.map32TilesTL, rom);
                    tr = generate(i, k, (int)Dimension.map32TilesTR, rom);
                    bl = generate(i, k, (int)Dimension.map32TilesBL, rom);
                    br = generate(i, k, (int)Dimension.map32TilesBR, rom);
                    tiles32.Add(new Tile32(new ushort[] { tl, tr, bl, br }));
                }
            }
        }

        static ushort generate(int i, int k, int dimension,Rom rom)
        {
            return (ushort)(rom.romData[map32address[dimension] + k + (i)]
                + (((rom.romData[map32address[dimension] + (i) + (k <= 1 ? 4 : 5)] >> (k % 2 == 0 ? 4 : 0)) & 0x0F) * 256));
        }




        private static Tile8Data GetTile8(short tile)
        {
            //vhopppcc cccccccc
            bool o = false;
            bool v = false;
            bool h = false;
            ushort tid = (ushort)(tile & 0x3FF);
            byte p = (byte)((tile >> 10) & 0x07);
            if ((tile & 0x2000) == 0x2000)
            {
                o = true;
            }
            if ((tile & 0x4000) == 0x4000)
            {
                h = true;
            }
            if ((tile & 0x8000) == 0x8000)
            {
                v = true;
            }
            return new Tile8Data(tid, p, v, h, o);

        }
    }
}
