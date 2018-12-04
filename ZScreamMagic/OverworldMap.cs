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
        byte spritesetId;
        public IntPtr mapGfxPtr = Marshal.AllocHGlobal(512 * 512); //8bpp - Different for every maps
        public Bitmap mapBitmap;
        public Color[] palettes = new Color[256]; //
        public byte[] blocksets = new byte[16];
        public byte mapParent = 0;
        //All +1 because first color is transparent or grass color
        int startOfHudPalette = 0; //Position of the palette array
        int startOfMainPalette = 32 + 1; //Position of the palette array
        int startOfAux1Palette = 40 + 1; //Position of the palette array
        int startOfAux2Palette = 88 + 1; //Position of the palette array
        int startOfAnimatedPalette = 112 + 1; //Position of the palette array
        int startOfSpritesPalette = 144 + 1;

        public List<Sprite> spritesList = new List<Sprite>();

        public byte mainPaletteIndex = 0;
        public byte Aux1PaletteIndex = 0;
        public byte Aux2PaletteIndex = 0;
        public byte AnimatedPaletteIndex = 0;
        public byte SpritePaletteIndex = 0;

        public ushort[] tiles = new ushort[32 * 32]; //tile16 

        //TODO : Replace "worldState" that in the Overworld Editor not here
        public WorldState worldState = WorldState.ZeldaRescued;

        public OverworldMap(Rom rom, int mapId)
        {
            this.rom = rom;
            this.mapId = mapId;
            mapParent = rom.romData[RomConstants.overworldmapParent + mapId];
            mapBitmap = new Bitmap(512, 512, 512, PixelFormat.Format8bppIndexed, mapGfxPtr);
            LoadMapData(rom);

            
        }

        public void LoadMapData(Rom rom)
        {
            LoadPalette();
            LoadBlockset();
            LoadMapTiles(rom);
            LoadSprites(worldState);
        }

        public void LoadSprites(WorldState worldState)
        {

            //spritesList.Add(new Sprite(4, 4, 0, 0, 0,(byte)mapId));
            int pointerPos = 0;
            if (worldState == WorldState.RainState)
            {
                pointerPos = RomConstants.overworldSpritesBegining;
            }
            else if (worldState == WorldState.ZeldaRescued)
            {
                pointerPos = RomConstants.overworldSpritesZelda;
            }
            else if (worldState == WorldState.AgahnimDefeated)
            {
                pointerPos = RomConstants.overworldSpritesAgahnim;
            }
            //Bank of overworld sprites is 09

            int snesPointer = (09<<16) +
                (rom.romData[pointerPos + (mapId * 2) + 1] << 8) +
                rom.romData[pointerPos + (mapId * 2)];

            int spritesAddress = Utils.SnesToPc(snesPointer);

            //Loading sprite routine !
            while (true)
            {

                byte b1 = rom.romData[spritesAddress];
                byte b2 = rom.romData[spritesAddress + 1];
                byte b3 = rom.romData[spritesAddress + 2];

                if (b1 == 0xFF) { break; }

                byte x = (byte)(b2 & 0x3F);
                byte y = (byte)(b1 & 0x3F);

                //Code to shift the sprite away on the next map if X or Y > 512
                byte mapOn = (byte)mapId;
                if (x > 32)
                {
                    mapOn += 1;
                    x -= 32;
                }
                if (y > 32)
                {
                    mapOn += 8;
                    y -= 32;
                }

                spritesList.Add(new Sprite(x,y,b3,0,0,(byte)mapOn));


                spritesAddress += 3;
            }

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
            byte v0AA2 = rom.romData[RomConstants.overworldBlockset + mapParent];
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

            if (mapParent >= 3 && mapParent <= 7)
            {
                blocksets[7] = 0x58;
            }
            else
            {
                blocksets[7] = 0x5A;
            }



            //TODO : Add a worldstate variable
            //Load Sprites Blocksets
            spritesetId = rom.romData[RomConstants.overworldSpriteset + mapParent + 0x40];

            blocksets[8] = 115 + 0;
            blocksets[9] = 115 + 1;
            blocksets[10] = 115 + 6;
            blocksets[11] = 115 + 7;

            blocksets[12] = (byte)(115 + rom.romData[RomConstants.spriteGroupset + (spritesetId * 4) + 0]);
            blocksets[13] = (byte)(115 + rom.romData[RomConstants.spriteGroupset + (spritesetId * 4) + 1]);
            blocksets[14] = (byte)(115 + rom.romData[RomConstants.spriteGroupset + (spritesetId * 4) + 2]);
            blocksets[15] = (byte)(115 + rom.romData[RomConstants.spriteGroupset + (spritesetId * 4) + 3]);



            //rom.romData[ + (blocksetId * 4)];
        }

        public void LoadPalette()
        {
            for (int i = 0; i < 256; i++)
            {
                palettes[i] = new Color();
            }

            //Load from PalettesHandler

            //NVM


            //there's 4 palette per group
            paletteId = (byte)(rom.romData[RomConstants.overworldMapPalette + mapParent]);

            mainPaletteIndex = 0;
            Aux1PaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4) + 0];
            Aux2PaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4) + 1];
            AnimatedPaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4) + 2];
            SpritePaletteIndex = rom.romData[RomConstants.overworldSpritePalette];
            updateGrassPalette();




            //Palettes Infos : 
            //0-31 -> Hud Palettes
            //byte mainPaletteIndex = rom.romData[RomConstants.overworldMapPaletteGroup + (paletteId * 4)];


            if (mapParent >= 3 && mapParent <= 7)
            {
                mainPaletteIndex = 2;
            }


            updateMainPalette(mainPaletteIndex);
            updateAux1Palette(Aux1PaletteIndex == 255 ? 0 : Aux1PaletteIndex);
            updateAux2Palette(Aux2PaletteIndex == 255 ? 0 : Aux2PaletteIndex);
            updateAnimatedPalette(AnimatedPaletteIndex == 255 ? 0 : AnimatedPaletteIndex);
            updateSpritesPalettes(SpritePaletteIndex == 255 ? 0 : SpritePaletteIndex);
            updateHudPalette(0);

        }

        public void updateSpritesPalettes(int index)
        {
            //Index will be used for the "map specific sprites palettes"
            int v = 0;
            int a = 0;
            for (int x = 0; x < 15*4; x++)
            {
                //This is global sprite palettes
                palettes[startOfSpritesPalette + x + a] = Palettes.globalSprite_Palettes[0][x];
                v++;
                if (v >= 15)
                {
                    v = 0;
                    a++;
                }
            }
        }

        public void updateGrassPalette()
        {
            Color[] c = Palettes.overworld_GrassPalettes;
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
                    palettes[startOfMainPalette + x + (y * 16)] = Palettes.overworld_MainPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAux1Palette(int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    palettes[startOfAux1Palette + x + (y * 16)] = Palettes.overworld_AuxPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAux2Palette(int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    palettes[startOfAux2Palette + x + (y * 16)] = Palettes.overworld_AuxPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAnimatedPalette(int index)
        {
            for (int x = 0; x < 7; x++)
            {
                palettes[startOfAnimatedPalette + x] = Palettes.overworld_AnimatedPalettes[index][x];
            }
        }

        public void updateHudPalette(int index)
        {
            for (int x = 0; x < 32; x++)
            {
                palettes[startOfHudPalette + x] = Palettes.HudPalettes[index][x];
            }
        }


    }
}
