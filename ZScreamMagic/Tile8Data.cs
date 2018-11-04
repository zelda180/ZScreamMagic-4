using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public struct Tile8Data
    {
        public bool over, mirror_v, mirror_h;
        public byte palette;
        public ushort id;

        public Tile8Data(ushort id, byte palette, bool v, bool h, bool o)
        {
            this.id = id;
            this.palette = palette;
            this.mirror_v = v;
            this.mirror_h = h;
            this.over = o;
        }

    }
}
