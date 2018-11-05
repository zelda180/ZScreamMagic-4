using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public Color[] palettes = new Color[256]; //
        public byte[] blocksets = new byte[8];
        //All +1 because first color is transparent or grass color
        int startOfMainPalette = 32 + 1;
        int startOfAux1Palette = 40 + 1;
        int startOfAux2Palette = 88 + 1;
        int startOfAnimatedPalette = 112 + 1;

        public OverworldMap(Rom rom, int mapId)
        {
            this.rom = rom;
            this.mapId = mapId;
            LoadMapData();
        }

        public void LoadMapData()
        {
            LoadPalette(); 
            LoadBlockset();
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

            updateMainPalette(0);
            updateAux1Palette(Aux1PaletteIndex == 255 ? 0 : Aux1PaletteIndex);
            updateAux2Palette(Aux2PaletteIndex == 255 ? 0 : Aux2PaletteIndex);
            updateAnimatedPalette(AnimatedPaletteIndex == 255 ? 0 : AnimatedPaletteIndex);

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
