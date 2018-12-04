using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public struct PaletteDescription
    {
        public string description;
        public byte[] linkedColor;
        public byte value; 
        //Value (i don't think there is more than 5 ramp colors) so 0 to 5
        public PaletteDescription(string description,byte[] linkedColor = null,byte value = 0)
        {
            this.description = description;
            this.linkedColor = linkedColor;
            this.value = value;
        }
        

    }
}
