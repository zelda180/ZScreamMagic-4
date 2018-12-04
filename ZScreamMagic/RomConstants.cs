using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public static class RomConstants
    {
        #region GFX Related Variables
        //===========================================================================================
        //GFX Related Variables
        //===========================================================================================
        //===========================================================================================
        //
        //===========================================================================================
        public static int tile_address = 0x1B52; // JP = Same //i don't think that need a pointer
        public static int tile_address_floor = 0x1B5A; // JP = Same //i don't think that need a pointer
        public static int subtype1_tiles = 0x8000; // JP = Same //i don't think that need a pointer
        public static int subtype2_tiles = 0x83F0; // JP = Same //i don't think that need a pointer
        public static int subtype3_tiles = 0x84F0; // JP = Same //i don't think that need a pointer
        public static int gfx_animated_pointer = 0x10275; //JP 0x10624 //long pointer

        public static int gfx_1_pointer = 0x6790; //2byte pointer bank 00 pc -> 0x4320
        public static int gfx_2_pointer = 0x6795;
        public static int gfx_3_pointer = 0x679A;
        #endregion

        #region Overworld Related Variables
        //===========================================================================================
        //Overworld Related Variables
        //===========================================================================================
        public static int compressedAllMap32PointersHigh = 0x1794D;
        public static int compressedAllMap32PointersLow = 0x17B2D;
        public static int overworldblocksetGroups = 0x05D97;
        public static int map16Tiles = 0x78000;
        public static int map32TilesTL = 0x18000;
        public static int map32TilesTR = 0x1B400;
        public static int map32TilesBL = 0x20000;
        public static int map32TilesBR = 0x23400;
        public static int overworldMapPalette = 0x7D1C;
        public static int overworldSpritePalette = 0x7B41;
        public static int overworldMapPaletteGroup = 0x75504;
        public static int overworldMapSize = 0x12844;
        public static int overworldBlockset = 0x7C9C; //$0AA2 is the secondary background graphics index
        public static int overworldBlockset2 = 0x58F4;//$0AA4
        public static int overworldSpriteset = 0x7A41;//(Rain State, Light World, Agah Defeated Lightworld, Dark World)
        public static int spriteGroupset = 0x5B57;
        public static int mainGfxGroupset = 0x6073;
        public static int overworldmapParent = 0x125EC;
        public static int overworldSpritesBegining = 0x4C881;//0x40 LW and DW
        public static int overworldSpritesZelda = 0x4C901;//0x90 LW,DW,Special Areas
        public static int overworldSpritesAgahnim = 0x4CA21;//0x90 LW,DW,Special Areas (agah defeated)
        #endregion

        #region Palettes Related Variables
        //===========================================================================================
        //Palettes Related Variables - This contain all the palettes of the game
        //===========================================================================================
        public static int overworldPaletteMain = 0xDE6C8;
        public static int overworldPaletteAuxialiary = 0xDE86C;
        public static int overworldPaletteAnimated = 0xDE604;
        public static int globalSpritePalettesLW = 0xDD218;
        public static int globalSpritePalettesDW = 0xDD290;
        public static int armorPalettes = 0xDD308;//Green, Blue, Red, Bunny, Electrocuted (15 colors each)
        public static int spritePalettesAux1 = 0xDD39E; //7 colors each
        public static int spritePalettesAux2 = 0xDD446; //7 colors each
        public static int spritePalettesAux3 = 0xDD4E0; //7 colors each
        public static int swordPalettes = 0xDD630;//3 colors each - 4 entries
        public static int shieldPalettes = 0xDD648;//4 colors each - 3 entries
        public static int hudPalettes = 0xDD660;
        public static int dungeonMapPalettes = 0xDD70A; //21 colors
        public static int dungeonMainPalettes = 0xDD734;//(15*6) colors each - 20 entries
        public static int dungeonMapBgPalettes = 0xDE544; //16*6
        public static int hardcodedGrassLW = 0x5FEA9;//Mirrored Value at 0x75645 : 0x75625
        public static int hardcodedGrassDW = 0x7564F;
        public static int hardcodedGrassSpecial = 0x75640;

        #endregion
    }
}
