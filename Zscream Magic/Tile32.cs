using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zscream_Magic
{
    public class Tile32
    {
        //[0,1]
        //[2,3]
        public ushort[] tile16data;
        public Tile32(ushort[] tile16data)
        {
            this.tile16data = new ushort[4];
            this.tile16data[0] = tile16data[0];
            this.tile16data[1] = tile16data[1];
            this.tile16data[2] = tile16data[2];
            this.tile16data[3] = tile16data[3];
        }
    }
}
