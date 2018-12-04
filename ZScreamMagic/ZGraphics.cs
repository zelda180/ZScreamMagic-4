using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZCompressLibrary;

namespace ZScreamMagic
{
    public static class ZGraphics
    {
        public static IntPtr allgfx16Ptr = Marshal.AllocHGlobal((128 * 7136) / 2);



        public static bool[] isbpp3 = new bool[223];

        public static int GetPCGfxAddress(byte[] romData,byte id)
        {
            int gfxPointer1 = Utils.SnesToPc((romData[RomConstants.gfx_1_pointer + 1] << 8) + (romData[RomConstants.gfx_1_pointer])),
                gfxPointer2 = Utils.SnesToPc((romData[RomConstants.gfx_2_pointer + 1] << 8) + (romData[RomConstants.gfx_2_pointer])),
                gfxPointer3 = Utils.SnesToPc((romData[RomConstants.gfx_3_pointer + 1] << 8) + (romData[RomConstants.gfx_3_pointer]));

            byte gfxGamePointer1 = romData[gfxPointer1 + id];
            byte gfxGamePointer2 = romData[gfxPointer2 + id];
            byte gfxGamePointer3 = romData[gfxPointer3 + id];

            return Utils.SnesToPc(Utils.AddressFromBytes(gfxGamePointer1, gfxGamePointer2, gfxGamePointer3));
        }

        /// <summary>
        /// Create Snes Data Gfx (3 and 2 bpp) snes format
        /// </summary>
        /// <param name="CreateAllGfxDataRaw"></param>
        /// <returns></returns>
        public static byte[] CreateAllGfxDataRaw(byte[] romData)
        {

            //0-112 -> compressed 3bpp bgr -> (decompressed each) 0x600 bytes
            //113-114 -> compressed 2bpp -> (decompressed each) 0x800 bytes
            //115-126 -> uncompressed 3bpp sprites -> (each) 0x600 bytes
            //127-217 -> compressed 3bpp sprites -> (decompressed each) 0x600 bytes
            //218-222 -> compressed 2bpp -> (decompressed each) 0x800 bytes
            byte[] buffer = new byte[0x54A00];
            int bufferPos = 0;
            byte[] data = new byte[0x600];
            int compressedSize = 0;
            LogData.AddLine("====GFX Creation - Start====");
            for (int i = 0; i < 223; i++)
            {
                bool c = true;
                if (i >= 0 && i <= 112) //compressed 3bpp bgr
                {
                    isbpp3[i] = true;
                }
                else if (i >= 113 && i <= 114) //compressed 2bpp
                {
                    isbpp3[i] = false;
                }
                else if (i >= 115 && i <= 126) //uncompressed 3bpp sprites
                {
                    isbpp3[i] = true;
                    c = false;
                }
                else if (i >= 127 && i <= 217) //compressed 3bpp sprites
                {
                    isbpp3[i] = true;
                }
                else if (i >= 218 && i <= 222) //compressed 2bpp
                {
                    isbpp3[i] = false;
                }

                if (c)//if data is compressed decompress it
                {
                    data = Decompress.ALTTPDecompressGraphics(romData, GetPCGfxAddress(romData, (byte)i), 0x800, ref compressedSize);
                    LogData.AddLine("Gfx " + i.ToString() + " Size Decompressed:0x" + data.Length.ToString("X4") + " Compressed:0x" + compressedSize.ToString("X4"));
                }
                else
                {
                    data = new byte[0x600];
                    int startAddress = GetPCGfxAddress(romData, (byte)i);
                    for (int j = 0; j < 0x600; j++)
                    {
                        data[j] = romData[j + startAddress];
                    }
                    LogData.AddLine("2bpp Gfx " + i.ToString() + " Size Decompressed:0x" + data.Length.ToString("X4") + " Compressed:NULL");
                }
                
                for (int j = 0; j < data.Length; j++)
                {
                    buffer[j + bufferPos] = data[j];
                }
                bufferPos += data.Length;
            }



            return buffer;
        }

        /// <summary>
        /// Create gfx data for PC 4bpp indexed (Bitmap Raw Data)
        /// </summary>
        /// <param name="CreateAllGfxData"></param>
        /// <returns></returns>
        public static void CreateAllGfxData(byte[] romData)
        {
            //3BPP - 24 bytes
            //[r0, bp1], [r0, bp2], [r1, bp1], [r1, bp2], [r2, bp1], [r2, bp2], [r3, bp1], [r3, bp2]
            //[r4, bp1], [r4, bp2], [r5, bp1], [r5, bp2], [r6, bp1], [r6, bp2], [r7, bp1], [r7, bp2]
            //[r0, bp3], [r1, bp3], [r2, bp3], [r3, bp3], [r4, bp3], [r5, bp3], [r6, bp3], [r7, bp3]
            //2BPP - 16 bytes *2 ?
            //[r0, bp1], [r0, bp2], [r1, bp1], [r1, bp2], [r2, bp1], [r2, bp2], [r3, bp1], [r3, bp2]
            //[r4, bp1], [r4, bp2], [r5, bp1], [r5, bp2], [r6, bp1], [r6, bp2], [r7, bp1], [r7, bp2]

            //Indexed 4bpp (Bitmap Raw Data)
            //From top left each pixel is 4bit, 8 pixel per horizontal row (4bytes per line) (32bytes per 8*8)
            //[px00,px01], [px02,px03], [px04,px05], [px06,px07]
            //[px08,px09], [px10,px11], [px12,px13], [px14,px15]
            //[px16,px17], [px18,px19], [px20,px21], [px22,px23]
            //[px24,px25], [px26,px27], [px28,px29], [px30,px31]
            //[px32,px33], [px34,px35], [px36,px37], [px38,px39]
            //[px40,px41], [px42,px43], [px44,px45], [px46,px47]
            //[px48,px49], [px50,px51], [px52,px53], [px54,px55]
            //[px56,px57], [px58,px59], [px60,px61], [px62,px63]
            //Length of a sheet is 128x32 pixel but 128 /2 because 4bpp



            byte[] data = CreateAllGfxDataRaw(romData);
            byte[] newData = new byte[0x6F800]; //NEED TO GET THE APPROPRIATE SIZE FOR THAT
            byte[] mask = new byte[] {0x80,0x40,0x20,0x10,0x08,0x04,0x02,0x01 };
            int sheetPosition = 0;
            //8x8 tile
            for (int s = 0; s < 223; s++) //Per Sheet
            {

                for (int j = 0; j < 4; j++) //Per Tile Line Y
                {
                    for (int i = 0; i < 16; i++) //Per Tile Line X
                    {
                        for (int y = 0; y < 8; y++) //Per Pixel Line
                        {

                            if (isbpp3[s])
                            {
                                byte lineBits0 = data[(y * 2) + (i * 24) + (j * 384) + sheetPosition];
                                byte lineBits1 = data[(y * 2) + (i * 24) + (j * 384) + 1 + sheetPosition];
                                byte lineBits2 = data[(y) + (i * 24) + (j * 384) + 16 + sheetPosition];

                                for (int x = 0; x < 4; x++) //Per Pixel X
                                {
                                    byte pixdata = 0;
                                    byte pixdata2 = 0;

                                    if ((lineBits0 & mask[(x * 2)]) == mask[(x * 2)]) { pixdata += 1; }
                                    if ((lineBits1 & mask[(x * 2)]) == mask[(x * 2)]) { pixdata += 2; }
                                    if ((lineBits2 & mask[(x * 2)]) == mask[(x * 2)]) { pixdata += 4; }

                                    if ((lineBits0 & mask[(x * 2) + 1]) == mask[(x * 2) + 1]) { pixdata2 += 1; }
                                    if ((lineBits1 & mask[(x * 2) + 1]) == mask[(x * 2) + 1]) { pixdata2 += 2; }
                                    if ((lineBits2 & mask[(x * 2) + 1]) == mask[(x * 2) + 1]) { pixdata2 += 4; }

                                    newData[(y * 64) + (x) + (i * 4) + (j * 512) + (s * 2048)] = (byte)((pixdata << 4) | pixdata2);
                                }
                            }
                            else
                            {
                                byte lineBits0 = data[(y * 2) + (i * 16) + (j * 256) + sheetPosition];
                                byte lineBits1 = data[(y * 2) + (i * 16) + (j * 256) + 1 + sheetPosition];

                                for (int x = 0; x < 4; x++) //Per Pixel X
                                {
                                    byte pixdata = 0;
                                    byte pixdata2 = 0;

                                    if ((lineBits0 & mask[(x * 2)]) == mask[(x * 2)]) { pixdata += 1; }
                                    if ((lineBits1 & mask[(x * 2)]) == mask[(x * 2)]) { pixdata += 2; }

                                    if ((lineBits0 & mask[(x * 2) + 1]) == mask[(x * 2) + 1]) { pixdata2 += 1; }
                                    if ((lineBits1 & mask[(x * 2) + 1]) == mask[(x * 2) + 1]) { pixdata2 += 2; }

                                    newData[(y * 64) + (x) + (i * 4) + (j * 512) + (s * 2048)] = (byte)((pixdata << 4) | pixdata2);
                                }
                            }

                        }
                    }
                }
                if (isbpp3[s])
                {
                    sheetPosition += 0x600;
                }
                else
                {
                    sheetPosition += 0x800;
                }
            }

            unsafe
            {
               
                byte* allgfx16Data = (byte*)allgfx16Ptr.ToPointer();
                for (int i = 0; i < 0x6F800; i++)
                {
                    allgfx16Data[i] = newData[i];
                }
  
                //b.Save("Test.png",ImageFormat.Png);
            }


        }

   

    }
}
