using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public class OverworldMap
    {
        Rom rom;
        int mapId;
        byte paletteId;
        byte blocksetId;
        public IntPtr mapGfxPtr = Marshal.AllocHGlobal(512 * 512); //8bpp - Different for every maps
        public Bitmap mapBitmap;
        public Color[] palettes = new Color[256]; //
        public byte[] blocksets = new byte[8];
        //All +1 because first color is transparent or grass color
        int startOfMainPalette = 32 + 1; //Position of the palette array
        int startOfAux1Palette = 40 + 1; //Position of the palette array
        int startOfAux2Palette = 88 + 1; //Position of the palette array
        int startOfAnimatedPalette = 112 + 1; //Position of the palette array

        public ushort[] tiles = new ushort[32 * 32]; //tile16 

        public OverworldMap(Rom rom, int mapId)
        {
            this.rom = rom;
            this.mapId = mapId;
            mapBitmap = new Bitmap(512, 512, 512, PixelFormat.Format8bppIndexed, mapGfxPtr);
            LoadMapData(rom);
        }

        public void LoadMapData(Rom rom)
        {
            LoadPalette(); 
            LoadBlockset();
            LoadMapTiles(rom);
        }

        public void LoadMapTiles(Rom rom)
        {
            //locat functions

            int p1 =
            (rom.romData[(RomConstants.compressedAllMap32PointersHigh) + 2 + (int)(3 * mapId)] << 16) +
            (rom.romData[(RomConstants.compressedAllMap32PointersHigh) + 1 + (int)(3 * mapId)] << 8) +
            (rom.romData[(RomConstants.compressedAllMap32PointersHigh + (int)(3 * mapId))]);
            p1 = Utils.SnesToPc(p1);

            int p2 =
            (rom.romData[(RomConstants.compressedAllMap32PointersLow) + 2 + (int)(3 * mapId)] << 16) +
            (rom.romData[(RomConstants.compressedAllMap32PointersLow) + 1 + (int)(3 * mapId)] << 8) +
            (rom.romData[(RomConstants.compressedAllMap32PointersLow + (int)(3 * mapId))]);
            p2 = Utils.SnesToPc(p2);


            int ttpos = 0, compressedSize1 = 0, compressedSize2 = 0;

            byte[] bytes = ZCompressLibrary.Decompress.ALTTPDecompressOverworld(rom.romData, p2, 1000, ref compressedSize1);
            byte[] bytes2 = ZCompressLibrary.Decompress.ALTTPDecompressOverworld(rom.romData, p1, 1000, ref compressedSize2);

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    ushort tidD = (ushort)((bytes2[ttpos] << 8) + bytes[ttpos]);

                    int tpos = tidD;
                    if (tpos < TilesLoader.tiles32.Count)
                    {
                        tiles[(x * 2) + ((y * 2) * 32)] = TilesLoader.tiles32[tpos].tile16data[0];
                        tiles[(x * 2)+1 + ((y * 2) * 32)] = TilesLoader.tiles32[tpos].tile16data[1];
                        tiles[(x * 2) + (((y * 2)+1) * 32)] = TilesLoader.tiles32[tpos].tile16data[2];
                        tiles[(x * 2)+1 + (((y * 2)+1) * 32)] = TilesLoader.tiles32[tpos].tile16data[3];
                    }
                    ttpos += 1;
                }
            }
        }


        public void LoadBlockset()
        {
            //===================================================================
            //TODO : Find all the hardcoded values in the game since they are not all using that array
            //Notes : 0-1(Main) are always the same (trees,mountain sheet) depending on world you are in
            //Notes : 2-6(Map) are the group values depending on the map
            //Notes : 7-8(Animated) are Hardcoded depending on the map you are in

            //Notes : There's a default blockset load for all map
            //Notes : Blockset values are added on top of it, if it 0 it use default value

            //Setting Routine : 0x12B08 in ROM -> bank02.asm:6532
            //; $0AA1 = 0x21 for dark world, 0x20 for light world. (overworldBlockset MAIN)
            //; $0AA2 is the secondary background graphics index (overworldBlockset)
            //; $0AA3 is the sprite graphics index (overworldSpriteset)
            //; $0AA4 = (overworldBlockset2)

            //Loading Tileset Routine : 0x619B in ROM -> bank00.asm:4860
            //(Simplified)
            //LDX ($0AA2 *4)
            //LDA 5D97, X : BEQ +
            //STA 7EC2F8 -> $7E6000 (2)
            //+
            //LDA 5D98, X : BEQ +
            //STA 7EC2F9 -> $7E6600 (3)
            //+
            //LDA 5D99, X : BEQ +
            //STA 7EC2FA -> $7E6C00 (4)
            //+
            //LDA 5D9A, X : BEQ +
            //STA 7EC2FB -> $7E7200 (5)
            //+

            //$0AA1 is used to load all gfx group all 8 of them
            //====================================================================
            //There's 4 blockset per group

            byte v0AA1 = 0x20;
            byte v0AA2 = rom.romData[RomConstants.overworldBlockset + mapId];
            //byte v0AA3 = 0x20; Sprites useless for now
            //byte v0AA4 = 0x20;//Animated GFX i think
            if (mapId < 0x40)
            {
                //Light World
                v0AA1 = 0x20;
            }
            else
            {
                //Dark World
                v0AA1 = 0x21;
            }

            for (int i = 0;i < 8; i++)
            {
                blocksets[i] = rom.romData[RomConstants.mainGfxGroupset + (v0AA1 * 8) + i];
            }

            for (int i = 3; i < 7; i++) //0AA2 can replace 4 blockset on the map
            {
                if (rom.romData[RomConstants.overworldblocksetGroups + (v0AA2 * 4) + (i-3)] != 0)
                {
                    blocksets[i] = rom.romData[RomConstants.overworldblocksetGroups + (v0AA2 * 4) + (i - 3)];
                }
            }

            //rom.romData[ + (blocksetId * 4)];
        }

        public void LoadPalette()
        {
            for (int i = 0; i < 256; i++)
            {
                palettes[i] = new Color();
            }
            //there's 4 palette per group
            paletteId = (byte)(rom.romData[RomConstants.overworldMapPalette + mapId]);

            //Palettes Infos : 
            //0-31 -> Hud Palettes
            //byte mainPaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4)];
            byte Aux1PaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4) + 0];
            byte Aux2PaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4) + 1];
            byte AnimatedPaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4) + 2];
            updateGrassPalette();
            updateMainPalette(0);
            updateAux1Palette(Aux1PaletteIndex == 255 ? 0 : Aux1PaletteIndex);
            updateAux2Palette(Aux2PaletteIndex == 255 ? 0 : Aux2PaletteIndex);
            updateAnimatedPalette(AnimatedPaletteIndex == 255 ? 0 : AnimatedPaletteIndex);

        }

        public void updateGrassPalette()
        {
            Color[] c = ZGraphics.ReadPalette(rom.romData, 0x5FEA9, 1);
            for (int y = 0; y < 8; y++)
            {
                palettes[startOfMainPalette - 1 + (y * 16)] = c[0];
                palettes[startOfAux1Palette - 1 + (y * 16)] = c[0];
            }
        }


        public void updateMainPalette(int index)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    palettes[startOfMainPalette + x + (y * 16)] = ZGraphics.overworld_MainPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAux1Palette(int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    palettes[startOfAux1Palette + x + (y * 16)] = ZGraphics.overworld_AuxPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAux2Palette(int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    palettes[startOfAux2Palette + x + (y * 16)] = ZGraphics.overworld_AuxPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAnimatedPalette(int index)
        {
            for (int x = 0; x < 7; x++)
            {
                palettes[startOfAnimatedPalette + x] = ZGraphics.overworld_AnimatedPalettes[index][x];
            }
        }



    }
}
