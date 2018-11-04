using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public class Tile16
    {
        //[0,1]
        //[2,3]
        public Tile8Data[] tile8data;
        public Tile16(Tile8Data[] tile8data)
        {
            this.tile8data = new Tile8Data[4];
            this.tile8data[0] = tile8data[0];
            this.tile8data[1] = tile8data[1];
            this.tile8data[2] = tile8data[2];
            this.tile8data[3] = tile8data[3];
        }
    }
}
