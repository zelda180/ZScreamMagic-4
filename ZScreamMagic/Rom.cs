using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public class Rom
    {
        public byte[] romData; //1024kb - 4096kb
        RomRegion romRegion;

        public void LoadRom(FileStream file)
        {

            byte[] tempromData = new byte[file.Length];
            file.Read(tempromData, 0, (int)file.Length);

            if (IsRomHeadered((int)file.Length))
            {
                Console.WriteLine("Header Detected");
                romData = new byte[file.Length - 0x200];
                Array.Copy(tempromData, 0x200, romData, 0, (file.Length - 0x200));
            }
            else
            {
                romData = new byte[file.Length];
                Array.Copy(tempromData, 0, romData, 0, (file.Length));
            }

            romRegion = DetectRomRegion(romData);
            //Create all gfx data in Graphics static class as 4bpp indexed format
            ZGraphics.CreateAllGfxData(romData);
            ZGraphics.CreateAllPalettes(romData);


            LogData.SaveToFile("Logs.txt");
            Console.WriteLine("Rom Region Detected : " + romRegion.ToString());
        }

        public void SaveRom(FileStream file)
        {
            file.Write(romData, 0, romData.Length);
        }

        private bool IsRomHeadered(int size)
        {
            return (size & 0x200) == 0x200;
        }

        private RomRegion DetectRomRegion(byte[] romData)
        {
            int detectLocation = 0x2C;
            byte[] usBytes = new byte[] { 0x20, 0xC0, 0x87 };
            byte[] jpBytes = new byte[] { 0x0D, 0x21, 0xAD };
            byte[] romBytes = new byte[3];
            Array.Copy(romData, detectLocation, romBytes, 0, 3);

            if (romBytes.SequenceEqual(usBytes))
            {
                return RomRegion.Us;
            }

            if (romBytes.SequenceEqual(jpBytes))
            {
                return RomRegion.Jp;
            }

            return RomRegion.Invalid;
        }

        public byte readByte(int pcAddress)
        {
            return romData[pcAddress];
        }

        public short readShort(int pcAddress)
        {
            return BitConverter.ToInt16(romData, pcAddress);
        }

        


        /*public int readLongAddress(int pcAddress)
        {

        }

        public int readShortAddress(int pcAddress)
        {

        }

        public int snesAddressToPC(int snesAddress)
        {

        }*/
        

    }
}
