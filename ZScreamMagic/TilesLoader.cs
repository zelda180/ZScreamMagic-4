using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public class TilesLoader
    {


        public static Tile16[] LoadTile16(Rom rom)
        {
            int tpos = RomConstants.map16Tiles;
            List<Tile16> tiles16 = new List<Tile16>();
            for (int i = 0; i < 4096; i += 1)
            {
                var t0 = GetTile8(rom.readShort(tpos));
                var t1 = GetTile8(rom.readShort(tpos + 2));
                var t2 = GetTile8(rom.readShort(tpos + 4));
                var t3 = GetTile8(rom.readShort(tpos + 6));
                tpos += 8;
                tiles16.Add(new Tile16(new Tile8Data[4]{ t0, t1, t2, t3 }));
            }
            return tiles16.ToArray();
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
