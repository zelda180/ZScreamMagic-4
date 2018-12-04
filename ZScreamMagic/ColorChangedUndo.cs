using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    class ColorChangedUndo
    {
        public byte pos;
        public Color color;
        public ColorChangedUndo(byte pos, Color color)
        {
            this.color = color;
            this.pos = pos;
        }
    }
}
