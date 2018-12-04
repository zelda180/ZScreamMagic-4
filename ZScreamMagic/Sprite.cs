using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public class Sprite
    {
        public byte x, y, layer, id, overlord,mapOn;
        public Sprite(byte x,byte y, byte id, byte layer, byte overlord,byte mapOn)
        {
            this.x = x;
            this.y = y;
            this.layer = layer;
            this.id = id;
            this.overlord = overlord;
            this.mapOn = mapOn;
        }

        public unsafe void DrawOnBitmapData(IntPtr mapPtr,IntPtr tiles8GfxPtr)
        {

            //Color 0 must be ignored

            switch(id)
            {
                case 0x00: //Raven
                    DrawTile16(966, x * 16, y * 16, mapPtr, tiles8GfxPtr,04);
                    break;
                case 0x01: //Vulture
                    DrawTile16(902, (x * 16)-8, y * 16, mapPtr, tiles8GfxPtr, 04);
                    DrawTile16(902, (x * 16) + 8, y * 16, mapPtr, tiles8GfxPtr, 04,true);
                    break;
                case 0x02://Flying Skull
                case 0x03://Empty
                case 0x04://Pull Switch
                case 0x05://Pull Switch
                case 0x06://Pull Switch
                case 0x07://Pull Switch
                    break;
                case 0x08://Octorok (One Way)
                    DrawTile16(896, (x * 16), y * 16, mapPtr, tiles8GfxPtr, 09);
                    DrawTile8(954, (x * 16)+4, (y * 16)+8, mapPtr, tiles8GfxPtr, 09);
                    break;
                case 0x09:// Moldorm(Boss)
                    break;
                case 0x0A:// Octorok(Four Way)
                    DrawTile16(896, (x * 16), y * 16, mapPtr, tiles8GfxPtr, 09);
                    DrawTile8(954, (x * 16) + 4, (y * 16) + 8, mapPtr, tiles8GfxPtr, 09);
                    break;
                case 0x0B:// Chicken
                    DrawTile16(1002, (x * 16), y * 16, mapPtr, tiles8GfxPtr, 09);
                    break;
                case 0x0C:// Octorok(?)
                    DrawTile16(896, (x * 16), y * 16, mapPtr, tiles8GfxPtr, 09);
                    DrawTile8(954, (x * 16) + 4, (y * 16) + 8, mapPtr, tiles8GfxPtr, 09);
                    break;
                case 0x0D:// Buzzblob
                    DrawTile16(993, (x * 16), y * 16, mapPtr, tiles8GfxPtr, 04); //TODO : Change the palette
                    DrawTile8(1008, (x * 16), (y * 16) - 8, mapPtr, tiles8GfxPtr, 04);
                    DrawTile8(1008, (x * 16)+8, (y * 16) - 8, mapPtr, tiles8GfxPtr, 04,true);
                    break;
                case 0x0E:// Snapdragon

                    break;
                case 0x0F:// Octoballoon
                    DrawTile16(908, (x * 16), (y * 16), mapPtr, tiles8GfxPtr, 09);
                    DrawTile8(940, (x * 16), (y * 16)+16, mapPtr, tiles8GfxPtr, 09);
                    DrawTile8(941, (x * 16)+8,(y * 16)+16, mapPtr, tiles8GfxPtr, 09);

                    DrawTile8(908, (x * 16)+16, (y * 16), mapPtr, tiles8GfxPtr, 09,true);
                    DrawTile8(924, (x * 16)+16, (y * 16) + 8, mapPtr, tiles8GfxPtr, 09, true);
                    DrawTile8(940, (x * 16) + 16, (y * 16) + 16, mapPtr, tiles8GfxPtr, 09, true);
                    break;
                case 0x10:// Octoballoon Hatchlings

                    break;
                case 0x11:// Hinox
                    break;
                case 0x12:// Moblin
                    break;
                case 0x13:// Mini Helmasaur
                    break;
                case 0x14:// Gargoyle's Domain Gate
                    break;
                case 0x15:// Antifairy
                    break;
                case 0x16:// Sahasrahla / Aginah
                    break;
                case 0x17:// Bush Hoarder
                    break;
                case 0x18:// Mini Moldorm
                    break;
                case 0x19:// Poe
                    break;
                case 0x1A:// Dwarves
                    break;
                case 0x1B:// Arrow in wall ?
                    break;
                case 0x1C:// Statue
                    break;
                case 0x1D:// Weathervane
                    break;
                case 0x1E:// Crystal Switch
                    break;
                case 0x1F:// Bug - Catching Kid
                    break;
                case 0x20:// Sluggula
                    break;
                case 0x21:// Push Switch
                    break;
                case 0x22:// Ropa
                    break;
                case 0x23:// Red Bari
                    break;
                case 0x24:// Blue Bari
                    break;
                case 0x25:// Talking Tree
                    break;
                case 0x26:// Hardhat Beetle
                    break;
                case 0x27:// Deadrock
                    break;
                case 0x28:// Storytellers
                    break;
                case 0x29:// Blind Hideout attendant
                    break;
                case 0x2A:// Sweeping Lady
                    break;
                case 0x2B:// Multipurpose Sprite

                case 0x2C:// Lumberjacks

                case 0x2D:// Telepathic stones? (No idea what this actually is, likely unused)

                case 0x2E:// Flute Boy's Notes

                case 0x2F:// Race HP NPCs

                case 0x30:// Person ?

                case 0x31:// Fortune Teller

                case 0x32:// Angry Brothers

                case 0x33:// Pull For Rupees Sprite

                case 0x34:// Scared Girl 2

                case 0x35:// Innkeeper

                case 0x36:// Witch

                case 0x37:// Waterfall

                case 0x38:// Arrow Target

                case 0x39:// Average Middle-Aged Man

                case 0x3A:// Half Magic Bat

                case 0x3B:// Dash Item

                case 0x3C:// Village Kid

                case 0x3D:// Signs? Chicken lady also showed up / Scared ladies outside houses.

                case 0x3E:// Rock Hoarder

                case 0x3F:// Tutorial Soldier

                case 0x40:// Lightning Lock

                case 0x41:// Blue Sword Soldier / Used by guards to detect player

                case 0x42:// Green Sword Soldier

                case 0x43:// Red Spear Soldier

                case 0x44:// Assault Sword Soldier

                case 0x45:// Green Spear Soldier

                case 0x46:// Blue Archer

                case 0x47:// Green Archer

                case 0x48:// Red Javelin Soldier

                case 0x49:// Red Javelin Soldier 2

                case 0x4A:// Red Bomb Soldiers

                case 0x4B:// Green Soldier Recruits

                case 0x4C:// Geldman

                case 0x4D:// Rabbit

                case 0x4E:// Popo

                case 0x4F:// Popo 2

                case 0x50:// Cannon Balls

                case 0x51:// Armos

                case 0x52:// Giant Zora

                case 0x53:// Armos Knights(Boss)

                case 0x54:// Lanmolas(Boss)

                case 0x55:// Fireball Zora

                case 0x56:// Walking Zora

                case 0x57:// Desert Palace Barriers

                case 0x58:// Crab

                case 0x59:// Bird

                case 0x5A:// Squirrel

                case 0x5B:// Spark(Left to Right)

                case 0x5C:// Spark(Right to Left)

                case 0x5D:// Roller(vertical moving)

                case 0x5E:// Roller(vertical moving)

                case 0x5F:// Roller

                case 0x60:// Roller(horizontal moving)

                case 0x61:// Beamos

                case 0x62:// Master Sword

                case 0x63:// Devalant(Non - shooter)

                case 0x64:// Devalant(Shooter)

                case 0x65:// Shooting Gallery Proprietor

                case 0x66:// Moving Cannon Ball Shooters(Right)

                case 0x67:// Moving Cannon Ball Shooters(Left)

                case 0x68:// Moving Cannon Ball Shooters(Down)

                case 0x69:// Moving Cannon Ball Shooters(Up)

                case 0x6A:// Ball N' Chain Trooper

                case 0x6B:// Cannon Soldier

                case 0x6C:// Mirror Portal

                case 0x6D:// Rat

                case 0x6E:// Rope

                case 0x6F:// Keese

                case 0x70:// Helmasaur King Fireball

                case 0x71:// Leever

                case 0x72:// Activator for the ponds (where you throw in items)

                case 0x73:// Uncle / Priest

                case 0x74:// Running Man

                case 0x75:// Bottle Salesman

                case 0x76:// Princess Zelda

                case 0x77:// Antifairy(Alternate)

                case 0x78:// Village Elder

                case 0x79:// Bee

                case 0x7A:// Agahnim

                case 0x7B:// Agahnim Energy Ball

                case 0x7C:// Hyu

                case 0x7D:// Big Spike Trap

                case 0x7E:// Guruguru Bar(Clockwise)

                case 0x7F:// Guruguru Bar(Counter Clockwise)

                case 0x80:// Winder

                case 0x81:// Water Tektite

                case 0x82:// Antifairy Circle

                case 0x83:// Green Eyegore

                case 0x84:// Red Eyegore

                case 0x85:// Yellow Stalfos

                case 0x86:// Kodongos

                case 0x87:// Flames

                case 0x88:// Mothula(Boss)

                case 0x89:// Mothula's Beam

                case 0x8A:// Spike Trap

                case 0x8B:// Gibdo

                case 0x8C:// Arrghus(Boss)

                case 0x8D:// Arrghus spawn

                case 0x8E:// Terrorpin

                case 0x8F:// Slime

                case 0x90:// Wallmaster

                case 0x91:// Stalfos Knight

                case 0x92:// Helmasaur King

                case 0x93:// Bumper

                case 0x94:// Swimmers

                case 0x95:// Eye Laser(Right)

                case 0x96:// Eye Laser(Left)

                case 0x97:// Eye Laser(Down)

                case 0x98:// Eye Laser(Up)

                case 0x99:// Pengator

                case 0x9A:// Kyameron

                case 0x9B:// Wizzrobe

                case 0x9C:// Tadpoles

                case 0x9D:// Tadpoles

                case 0x9E:// Ostrich(Haunted Grove)

                case 0x9F:// Flute

                case 0xA0:// Birds(Haunted Grove)

                case 0xA1:// Freezor

                case 0xA2:// Kholdstare(Boss)

                case 0xA3:// Kholdstare's Shell

                case 0xA4:// Falling Ice

                case 0xA5:// Blue Zazak

                case 0xA6:// Red Zazak

                case 0xA7:// Stalfos

                case 0xA8:// Bomber Flying Creatures from Darkworld

                case 0xA9:// Bomber Flying Creatures from Darkworld

                case 0xAA:// Pikit

                case 0xAB:// Maiden

                case 0xAC:// Apple

                case 0xAD:// Lost Old Man

                case 0xAE:// Down Pipe

                case 0xAF:// Up Pipe

                case 0xB0:// Right Pip

                case 0xB1:// Left Pipe

                case 0xB2:// Good bee again ?

                case 0xB3:// Hylian Inscription

                case 0xB4:// Thief's chest (not the one that follows you, the one that you grab from the DW smithy house)

                case 0xB5:// Bomb Salesman

                case 0xB6:// Kiki

                case 0xB7:// Maiden following you in Blind Dungeon

                case 0xB8:// Monologue Testing Sprite

                case 0xB9:// Feuding Friends on Death Mountain

                case 0xBA:// Whirlpool

                case 0xBB:// Salesman / chestgame guy / 300 rupee giver guy / Chest game thief

                case 0xBC:// Drunk in the inn

                case 0xBD:// Vitreous(Large Eyeball)

                case 0xBE:// Vitreous(Small Eyeball)

                case 0xBF:// Vitreous' Lightning

                case 0xC0:// Monster in Lake of Ill Omen / Quake Medallion

                case 0xC1:// Agahnim teleporting Zelda to dark world

                case 0xC2:// Boulders

                case 0xC3:// Gibo

                case 0xC4:// Thief

                case 0xC5:// Medusa

                case 0xC6:// Four Way Fireball Spitters(spit when you use your sword)

                case 0xC7:// Hokku - Bokku

                case 0xC8:// Big Fairy who heals you

                case 0xC9:// Tektite

                case 0xCA:// Chain Chomp

                case 0xCB:// Trinexx

                case 0xCC:// Another part of trinexx

                case 0xCD:// Yet another part of trinexx

                case 0xCE:// Blind The Thief(Boss)

                case 0xCF:// Swamola

                case 0xD0:// Lynel

                case 0xD1://	Bunny Beam

                case 0xD2://	Flopping fish

                case 0xD3://	Stal

                case 0xD4://	Landmine

                case 0xD5://	Digging Game Proprietor

                case 0xD6://	Ganon

                case 0xD7://	Copy of Ganon, except invincible?

                case 0xD8://	Heart

                case 0xD9://	Green Rupee

                case 0xDA://	Blue Rupee

                case 0xDB://	Red Rupee

                case 0xDC://	Bomb Refill (1)

                case 0xDD://	Bomb Refill (4)

                case 0xDE://	Bomb Refill (8)

                case 0xDF://	Small Magic Refill

                case 0xE0://	Full Magic Refill

                case 0xE1://	Arrow Refill (5)

                case 0xE2://	Arrow Refill (10)

                case 0xE3://	Fairy

                case 0xE4://	Key

                case 0xE5://	Big Key

                case 0xE6://	Shield

                case 0xE7://	Mushroom

                case 0xE8://	Fake Master Sword

                case 0xE9://	Magic Shop dude / His items, including the magic powder

                case 0xEA://	Heart Container

                case 0xEB://	Heart Piece

                case 0xEC://	Bushes

                case 0xED://	Cane Of Somaria Platform

                case 0xEE://	Mantle

                case 0xEF://	Cane of Somaria Platform (Unused)

                case 0xF0://	Cane of Somaria Platform (Unused)

                case 0xF1://	Cane of Somaria Platform (Unused)

                case 0xF2://	Medallion Tablet
                    break;

            }

        }

        public unsafe void DrawTile16(int block, int x, int y, IntPtr mapPtr, IntPtr tiles8GfxPtr, byte palette, bool mirrorX = false, bool mirrorY = false)
        {
            byte* mapData = (byte*)mapPtr.ToPointer();
            byte* tile8Data = (byte*)tiles8GfxPtr.ToPointer();

            int srcy = (block / 16);
            int srcx = block - (srcy * 16);

            int mx = 0;
            if (mirrorX)
            {
                mx = 7;
            }

            for (int y2 = 0; y2 < 16; y2++)
            {
                for (int x2 = 0; x2 < 8; x2++) 
                {
                    //4bpp -> 8bpp
                    byte b1data = (byte)((tile8Data[(srcx * 4) + (srcy * 512) + (y2 * 64) + x2] >> 4) & 0x0F);
                    byte b2data = (byte)(tile8Data[(srcx * 4) + (srcy * 512) + (y2 * 64) + x2] & 0x0F);

                    byte p = (byte)(palette + 8);

                    if (palette >= 8)
                    {
                        b1data += 0x08; //set high bit to use right side palettes
                        b2data += 0x08;
                        p -= 8;
                    }

                    b1data |= (byte)(p << 4); //left side must have highbit setted too
                    b2data |= (byte)(p << 4);

                    if (mirrorX)
                    {
                        if ((b1data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2) + 1] = b1data;
                        }
                        if ((b2data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2)] = b2data;
                        }

                        mx--;
                        if (mx == -1)
                        {
                            mx = 7;
                        }
                    }
                    else
                    {
                        if ((b1data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2)] = b1data;
                        }
                        if ((b2data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2) + 1] = b2data;
                        }
                        mx++;
                        if (mx == 8)
                        {
                            mx = 0;
                        }
                    }
                }
            }
            
        }

        public unsafe void DrawTile8(int block, int x, int y, IntPtr mapPtr, IntPtr tiles8GfxPtr, byte palette, bool mirrorX = false, bool mirrorY = false)
        {
            byte* mapData = (byte*)mapPtr.ToPointer();
            byte* tile8Data = (byte*)tiles8GfxPtr.ToPointer();

            int srcy = (block / 16);
            int srcx = block - (srcy * 16);

            int mx = 0;
            if (mirrorX)
            {
                mx = 3;
            }

            for (int y2 = 0; y2 < 8; y2++)
            {
                for (int x2 = 0; x2 < 4; x2++)
                {
                    //4bpp -> 8bpp
                    byte b1data = (byte)((tile8Data[(srcx * 4) + (srcy * 512) + (y2 * 64) + x2] >> 4) & 0x0F);
                    byte b2data = (byte)(tile8Data[(srcx * 4) + (srcy * 512) + (y2 * 64) + x2] & 0x0F);
                    //Palette
                    byte p = (byte)(palette + 8);

                    if (palette >= 8)
                    {
                        b1data += 0x08; //set high bit to use right side palettes
                        b2data += 0x08;
                        p -= 8;
                    }

                    b1data |= (byte)(p << 4); //left side must have highbit setted too
                    b2data |= (byte)(p << 4);

                    if (mirrorX)
                    {
                        if ((b1data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2) + 1] = b1data;
                        }
                        if ((b2data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2)] = b2data;
                        }

                        mx--;
                        if (mx == -1)
                        {
                            mx = 3;
                        }
                    }
                    else
                    {
                        if ((b1data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2)] = b1data;
                        }
                        if ((b2data & 0x07) != 0x00) //Non Transparent Color
                        {
                            mapData[x + (y * 512) + (y2 * 512) + (mx * 2) + 1] = b2data;
                        }
                        mx++;
                        if (mx == 4)
                        {
                            mx = 0;
                        }
                    }
                }
            }

        }

    }
}
