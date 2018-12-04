using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZScreamMagic
{
    public partial class PaletteViewerMap : Form
    {
        public PaletteViewerMap(TilesViewer formViewer)
        {
            InitializeComponent();
            this.formViewer = formViewer;
            undoButton.Enabled = false;
            redoButton.Enabled = false;
        }
        TilesViewer formViewer;
        public OverworldMap[] maps;
        public int mapId = 0;
        public byte mainPaletteIndex = 0;
        public byte aux1PaletteIndex = 0;
        public byte aux2PaletteIndex = 0;
        public byte animatedPaletteIndex = 0;
        public byte spritePaletteIndex;

        int startOfMainPalette = 32 + 1; //Position of the palette array
        int startOfAux1Palette = 40 + 1; //Position of the palette array
        int startOfAux2Palette = 88 + 1; //Position of the palette array
        int startOfAnimatedPalette = 112 + 1; //Position of the palette array
        int startOfHudPalette = 0; //Position of the palette array
        int undoRedoPosition = 0;
        int startOfSpritesPalette = 144 + 1;
        List<ColorChangedUndo> colorUndo = new List<ColorChangedUndo>();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PaletteViewerMap_Load(object sender, EventArgs e)
        {
            infoLabel.Text = "";
            infoLabel.Text += "Selected Map" + "\r\n";
            infoLabel.Text += "Main Palette" + "\r\n";
            infoLabel.Text += "Aux1 Palette" + "\r\n";
            infoLabel.Text += "Aux2 Palette" + "\r\n";
            infoLabel.Text += "Anim Palette" + "\r\n";

            valueLabel.Text = "";
            valueLabel.Text += mapId.ToString("D3") + "\r\n";
            valueLabel.Text += mainPaletteIndex.ToString("D2") + "\r\n";
            valueLabel.Text += aux1PaletteIndex.ToString("D2") + "\r\n";
            valueLabel.Text += aux2PaletteIndex.ToString("D2") + "\r\n";
            valueLabel.Text += animatedPaletteIndex.ToString("D2") + "\r\n";

            updateGrassPalette();
            updateMainPalette(mainPaletteIndex);
            updateAux1Palette(aux1PaletteIndex == 255 ? 0 : aux1PaletteIndex);
            updateAux2Palette(aux2PaletteIndex == 255 ? 0 : aux2PaletteIndex);
            updateAnimatedPalette(animatedPaletteIndex == 255 ? 0 : animatedPaletteIndex);
            updateSpritesPalettes(spritePaletteIndex);
            updateHudPalette(0);
            pictureBox1.Refresh();
        }

        public void updateGrassPalette()
        {
            Color[] c = Palettes.overworld_GrassPalettes;
            for (int y = 0; y < 8; y++)
            {
                oldColors[startOfMainPalette - 1 + (y * 16)] = c[0];
                oldColors[startOfAux1Palette - 1 + (y * 16)] = c[0];

                newColors[startOfMainPalette - 1 + (y * 16)] = c[0];
                newColors[startOfAux1Palette - 1 + (y * 16)] = c[0];
            }
        }

        public void updateHudPalette(int index)
        {
            for (int x = 0; x < 32; x++)
            {
                oldColors[startOfHudPalette + x] = Palettes.HudPalettes[index][x];
                newColors[startOfHudPalette + x] = Palettes.HudPalettes[index][x];
            }
        }


        public void updateMainPalette(int index)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    oldColors[startOfMainPalette + x + (y * 16)] = Palettes.overworld_MainPalettes[index][x + (y * 7)];
                    newColors[startOfMainPalette + x + (y * 16)] = Palettes.overworld_MainPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAux1Palette(int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    oldColors[startOfAux1Palette + x + (y * 16)] = Palettes.overworld_AuxPalettes[index][x + (y * 7)];
                    newColors[startOfAux1Palette + x + (y * 16)] = Palettes.overworld_AuxPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAux2Palette(int index)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    oldColors[startOfAux2Palette + x + (y * 16)] = Palettes.overworld_AuxPalettes[index][x + (y * 7)];
                    newColors[startOfAux2Palette + x + (y * 16)] = Palettes.overworld_AuxPalettes[index][x + (y * 7)];
                }
            }
        }

        public void updateAnimatedPalette(int index)
        {
            for (int x = 0; x < 7; x++)
            {
                oldColors[startOfAnimatedPalette + x] = Palettes.overworld_AnimatedPalettes[index][x];
                newColors[startOfAnimatedPalette + x] = Palettes.overworld_AnimatedPalettes[index][x];
            }
        }

        public void updateSpritesPalettes(int index)
        {
            int v = 0;
            int a = 0;
            for (int x = 0; x < 15 * 4; x++)
            {
                oldColors[startOfSpritesPalette + x + a] = Palettes.globalSprite_Palettes[0][x];
                newColors[startOfSpritesPalette + x + a] = Palettes.globalSprite_Palettes[0][x];
                v++;
                if (v >= 15)
                {
                    v = 0;
                    a++;
                }
            }
        }


        Color[] oldColors = new Color[256];
        Color[] newColors = new Color[256];

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < 256; i++)
            {

                e.Graphics.FillRectangle(new SolidBrush(newColors[i]), new Rectangle(x * 16, y * 16, 16, 16));

                x++;
                if (x >= 16)
                {
                    y++;
                    x = 0;
                }
            }
            int sy = (colorOver / 16);
            int sx = (colorOver) - (sy*16);
            e.Graphics.DrawRectangle(new Pen(Brushes.Yellow,2), new Rectangle(sx*16, sy*16, 16, 16));

        }



        public void updateColorInfo(int x,int y)
        {
            int group = 0;
            if (x == 0 || x == 8) //grass
            {
                group = 5;
            }
            else if (x > 0 && x < 8 && y > 1 && y < 7) //Main Group
            {
                group = 0;
            }
            else if (x > 8 && x < 16 && y > 1 && y < 5) //Aux1 Group
            {
                group = 1;
            }
            else if (x > 8 && x < 16 && y > 4 && y < 8) //Aux2 Group
            {
                group = 2;
            }
            else if (x > 0 && x < 8 && y >= 7 && y < 8) //Animated Group
            {
                group = 3;
            }

            int address = 0;
            if (group == 5) //grass hard coded for now
            {
                address = 0x5FEA9;
            }
            else if (group == 0)
            {
                address = (RomConstants.overworldPaletteMain + (mainPaletteIndex * (35 * 2)) + (((x-1)*2) + ((y-2) * 14)) );
            }
            else if (group == 1)
            {
                address = (RomConstants.overworldPaletteAuxialiary + (aux1PaletteIndex * (21 * 2)) + (((x - 8) * 2) + ((y-2) * 14)));
            }
            else if (group == 2)
            {
                address = (RomConstants.overworldPaletteAuxialiary + (aux2PaletteIndex * (21 * 2)) + (((x - 8) * 2) + ((y-5) * 14)));
            }
            else if (group == 3)
            {
                address = (RomConstants.overworldPaletteAnimated + (animatedPaletteIndex * (7 * 2)) + (((x - 1) * 2) + ((y-7) * 14)));
            }

            if (colorOver < 128)
            {
                colorInfoLabel.Text = "Color Information : " + "\r\n";
            byte r, g, b;
            r = newColors[colorOver].R;
            g = newColors[colorOver].G;
            b = newColors[colorOver].B;
            colorInfoLabel.Text += "RGB Color Value:\r\n";
            colorInfoLabel.Text += "R " + r.ToString("D3") + " G " + g.ToString("D3") + " B " + b.ToString("D3") + "\r\n";
            colorInfoLabel.Text += "Location In ROM (PC Address):\r\n";
            colorInfoLabel.Text += "0x" + address.ToString("X6") + "\r\n";
            colorInfoLabel.Text += "Color Value in Hex (24bit):\r\n";
            colorInfoLabel.Text += "#" + ((r << 16) | (g << 8) | (b)).ToString("X6") + "\r\n";
            colorInfoLabel.Text += "Color Value in Hex (5:5:5):\r\n";
            short colorShortValue = (short)(((b / 8) << 10) | ((g / 8) << 5) | (r / 8));
            colorInfoLabel.Text += "$" + colorShortValue.ToString("X4") + "\r\n";
            colorInfoLabel.Text += "Description:\r\n";

                colorInfoLabel.Text += colorDescription[colorOver].description + "\r\n";
            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

        }
        ColorDialog cd = new ColorDialog();
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int p = ((e.Y / 16) * 16) + (e.X / 16);
                cd.Color = newColors[p];
                if (cd.ShowDialog() == DialogResult.OK)
                {

                    if (similarColorButton.Checked)
                    {
                        Color similarColor = newColors[p];
                        for (int i = 0;i<256;i++)
                        {
                            if (newColors[i] == similarColor)
                            {
                                colorUndo.Add(new ColorChangedUndo((byte)p, Color.FromArgb((newColors[p].R / 8) * 8, (newColors[p].G / 8) * 8, (newColors[p].B / 8) * 8)));
                                undoRedoPosition++;
                                if (colorUndo.Count != undoRedoPosition)
                                {
                                    colorUndo.RemoveRange(undoRedoPosition, colorUndo.Count - undoRedoPosition);
                                    redoButton.Enabled = false;
                                }
                                newColors[i] = Color.FromArgb((cd.Color.R / 8) * 8, (cd.Color.G / 8) * 8, (cd.Color.B / 8) * 8);
                            }
                        }
                    }
                    else if (rampcolorButton.Checked)
                    {
                        Color rampColor = cd.Color; //selected color is always the brigther
                        Color c = cd.Color;
                        if (colorDescription[p].linkedColor != null)
                        {
                            for (int i = 0; i < colorDescription[p].linkedColor.Length; i++)
                            {
                                c = Palettes.getColorShade(rampColor, colorDescription[colorDescription[p].linkedColor[i]].value);

                                colorUndo.Add(new ColorChangedUndo((byte)p, Color.FromArgb((newColors[p].R / 8) * 8, (newColors[p].G / 8) * 8, (newColors[p].B / 8) * 8)));
                                undoRedoPosition++;
                                newColors[colorDescription[p].linkedColor[i]] = c;
                            }

                        }


                        if (colorUndo.Count != undoRedoPosition)
                        {
                            colorUndo.RemoveRange(undoRedoPosition, colorUndo.Count - undoRedoPosition);
                            redoButton.Enabled = false;
                        }
                        
                    }
                    else 
                    {
                        colorUndo.Add(new ColorChangedUndo((byte)p, Color.FromArgb((newColors[p].R / 8) * 8, (newColors[p].G / 8) * 8, (newColors[p].B / 8) * 8)));
                        undoRedoPosition++;
                        if (colorUndo.Count != undoRedoPosition)
                        {
                            colorUndo.RemoveRange(undoRedoPosition, colorUndo.Count - undoRedoPosition);
                            redoButton.Enabled = false;
                        }
                        newColors[p] = Color.FromArgb((cd.Color.R / 8) * 8, (cd.Color.G / 8) * 8, (cd.Color.B / 8) * 8);
                    }




                    undoButton.Enabled = true;
                    pictureBox1.Refresh();
                    setTempColors();
                }
            }
        }


        int tempColor = -1;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tempColor == -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    int p = ((e.Y / 16) * 16) + (e.X / 16);
                    newColors[p] = Color.Fuchsia;
                    tempColor = p;
                }

                pictureBox1.Refresh();
                setTempColors();

            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tempColor != -1)
            {
                newColors[tempColor] = oldColors[tempColor];
                tempColor = -1;

                pictureBox1.Refresh();
                setTempColors();

            }

        }

        public void setTempColors()
        {
            setGrassPalette();
            setMainPalette(mainPaletteIndex);
            setAux1Palette(aux1PaletteIndex);
            setAux2Palette(aux2PaletteIndex);
            setAnimatedPalette(animatedPaletteIndex);
            setSpritesPalette(spritePaletteIndex);
            setHudPalette(0);
            for (int i = 0; i < 64; i++)
            {
                maps[i].LoadPalette();
            }
            formViewer.redraw();
        }


        public void setGrassPalette()
        {
            Color[] c = Palettes.overworld_GrassPalettes;
            for (int y = 0; y < 8; y++)
            {
                Palettes.overworld_GrassPalettes[0] = newColors[startOfMainPalette - 1 + (y * 16)];
            }
        }


        public void setMainPalette(int index)
        {
            if (mainPaletteIndex != 255)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        Palettes.overworld_MainPalettes[index][x + (y * 7)] = newColors[startOfMainPalette + x + (y * 16)];
                    }
                }
            }
        }

        public void setAux1Palette(int index)
        {
            if (aux1PaletteIndex != 255)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        ;
                        Palettes.overworld_AuxPalettes[index][x + (y * 7)] = newColors[startOfAux1Palette + x + (y * 16)];
                    }
                }
            }
        }

        public void setAux2Palette(int index)
        {
            if (aux2PaletteIndex != 255)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        Palettes.overworld_AuxPalettes[index][x + (y * 7)] = newColors[startOfAux2Palette + x + (y * 16)];
                    }
                }
            }
        }

        public void setAnimatedPalette(int index)
        {
            if (animatedPaletteIndex != 255)
            {
                for (int x = 0; x < 7; x++)
                {
                    Palettes.overworld_AnimatedPalettes[index][x] = newColors[startOfAnimatedPalette + x];
                }
            }
        }

        public void setHudPalette(int index)
        {
            for (int x = 0; x < 32; x++)
            { 
                Palettes.HudPalettes[index][x] = newColors[startOfHudPalette + x];
            }
        }

        public void setSpritesPalette(int index)
        {
            int v = 0;
            int a = 0;
            for (int x = 0; x < 15 * 4; x++)
            {
                Palettes.globalSprite_Palettes[0][x] = newColors[startOfSpritesPalette + x + a];
                v++;
                if (v >= 15)
                {
                    v = 0;
                    a++;
                }
            }

        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (undoRedoPosition != 0)
            {
                undoRedoPosition--;
                redoButton.Enabled = true;
                Color c = colorUndo[undoRedoPosition].color;
                newColors[colorUndo[undoRedoPosition].pos] = Color.FromArgb((c.R / 8) * 8, (c.G / 8) * 8, (c.B / 8) * 8);
                pictureBox1.Refresh();
                setTempColors();
            }
        }
        int oldcolorOver = 0;
        int colorOver = 0;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            colorOver = ((e.Y / 16) * 16) + (e.X / 16);
            if (oldcolorOver != colorOver)
            {

                updateColorInfo(e.X/16,e.Y/16);
                oldcolorOver = colorOver;
                pictureBox1.Refresh();
            }

        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to restore default palettes?\r\nThis can't be undone", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                for (int i = 0; i < 256; i++)
                {
                    newColors[i] = oldColors[i];
                }
                undoRedoPosition = 0;
                colorUndo.Clear();
                pictureBox1.Refresh();
                setTempColors();
                undoButton.Enabled = false;
                redoButton.Enabled = false;

            }


        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (undoRedoPosition < colorUndo.Count)
            {
                undoRedoPosition++;
                Color c = colorUndo[undoRedoPosition].color;
                newColors[colorUndo[undoRedoPosition].pos] = Color.FromArgb((c.R / 8) * 8, (c.G / 8) * 8, (c.B / 8) * 8);
                pictureBox1.Refresh();
                setTempColors();
            }
            if (undoRedoPosition == colorUndo.Count)
            {
                redoButton.Enabled = false;
            }
        }

        private void rampcolorButton_Click(object sender, EventArgs e)
        {

        }


        //TODO : Finish palettes descriptions
        public PaletteDescription[] colorDescription = new PaletteDescription[]
        {
new PaletteDescription("Hud Palette [000]"),
new PaletteDescription("Hud Palette [001]"),
new PaletteDescription("Hud Palette [002]"),
new PaletteDescription("Hud Palette [003]"),
new PaletteDescription("Hud Palette [004]"),
new PaletteDescription("Hud Palette [005]"),
new PaletteDescription("Hud Palette [006]"),
new PaletteDescription("Hud Palette [007]"),
new PaletteDescription("Hud Palette [008]"),
new PaletteDescription("Hud Palette [009]"),
new PaletteDescription("Hud Palette [010]"),
new PaletteDescription("Hud Palette [011]"),
new PaletteDescription("Hud Palette [012]"),
new PaletteDescription("Hud Palette [013]"),
new PaletteDescription("Hud Palette [014]"),
new PaletteDescription("Hud Palette [015]"),
new PaletteDescription("Hud Palette [016]"),
new PaletteDescription("Hud Palette [017]"),
new PaletteDescription("Hud Palette [018]"),
new PaletteDescription("Hud Palette [019]"),
new PaletteDescription("Hud Palette [020]"),
new PaletteDescription("Hud Palette [021]"),
new PaletteDescription("Hud Palette [022]"),
new PaletteDescription("Hud Palette [023]"),
new PaletteDescription("Hud Palette [024]"),
new PaletteDescription("Hud Palette [025]"),
new PaletteDescription("Hud Palette [026]"),
new PaletteDescription("Hud Palette [027]"),
new PaletteDescription("Hud Palette [028]"),
new PaletteDescription("Hud Palette [029]"),
new PaletteDescription("Hud Palette [030]"),
new PaletteDescription("Hud Palette [031]"),
new PaletteDescription("Grass Color"), //32
new PaletteDescription("Mountain Wall Color (Black)\r\n",new byte[]{33,34,35,36,39},4), //33
new PaletteDescription("Mountain Wall Color (Darker)\r\n",new byte[]{33,34,35,36,39},3), //34
new PaletteDescription("Mountain Wall Color (Dark)\r\nDirt Darker",new byte[]{33,34,35,36,39},2), //35
new PaletteDescription("Mountain Wall Color (Main)\r\nDirt Dark",new byte[]{33,34,35,36,39},1), //36
new PaletteDescription("Main Palette [037]"), //37
new PaletteDescription("Main Palette [038]"), //38
new PaletteDescription("Mountain Wall Color (Light)\r\nDirt Main",new byte[]{33,34,35,36,39},0), //39
new PaletteDescription("Grass Color"),
new PaletteDescription("Aux1 Palette [041]"),
new PaletteDescription("Aux1 Palette [042]"),
new PaletteDescription("Aux1 Palette [043]"),
new PaletteDescription("Aux1 Palette [044]"),
new PaletteDescription("Aux1 Palette [045]"),
new PaletteDescription("Aux1 Palette [046]"),
new PaletteDescription("Aux1 Palette [047]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Main Palette [049]"),
new PaletteDescription("Main Palette [050]"),
new PaletteDescription("Main Palette [051]"),
new PaletteDescription("Main Palette [052]"),
new PaletteDescription("Main Palette [053]"),
new PaletteDescription("Main Palette [054]"),
new PaletteDescription("Main Palette [055]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Aux1 Palette [057]"),
new PaletteDescription("Aux1 Palette [058]"),
new PaletteDescription("Aux1 Palette [059]"),
new PaletteDescription("Aux1 Palette [060]"),
new PaletteDescription("Aux1 Palette [061]"),
new PaletteDescription("Aux1 Palette [062]"),
new PaletteDescription("Aux1 Palette [063]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Main Palette [065]"),
new PaletteDescription("Main Palette [066]"),
new PaletteDescription("Main Palette [067]"),
new PaletteDescription("Main Palette [068]"),
new PaletteDescription("Main Palette [069]"),
new PaletteDescription("Main Palette [070]"),
new PaletteDescription("Main Palette [071]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Aux1 Palette [073]"),
new PaletteDescription("Aux1 Palette [074]"),
new PaletteDescription("Aux1 Palette [075]"),
new PaletteDescription("Aux1 Palette [076]"),
new PaletteDescription("Aux1 Palette [077]"),
new PaletteDescription("Aux1 Palette [078]"),
new PaletteDescription("Aux1 Palette [079]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Main Palette [081]"),
new PaletteDescription("Main Palette [082]"),
new PaletteDescription("Main Palette [083]"),
new PaletteDescription("Main Palette [084]"),
new PaletteDescription("Main Palette [085]"),
new PaletteDescription("Main Palette [086]"),
new PaletteDescription("Main Palette [087]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Aux2 Palette [089]"),
new PaletteDescription("Aux2 Palette [090]"),
new PaletteDescription("Aux2 Palette [091]"),
new PaletteDescription("Aux2 Palette [092]"),
new PaletteDescription("Aux2 Palette [093]"),
new PaletteDescription("Aux2 Palette [094]"),
new PaletteDescription("Aux2 Palette [095]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Main Palette [097]"),
new PaletteDescription("Main Palette [098]"),
new PaletteDescription("Main Palette [099]"),
new PaletteDescription("Main Palette [100]"),
new PaletteDescription("Main Palette [101]"),
new PaletteDescription("Main Palette [102]"),
new PaletteDescription("Main Palette [103]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Aux2 Palette [105]"),
new PaletteDescription("Aux2 Palette [106]"),
new PaletteDescription("Aux2 Palette [107]"),
new PaletteDescription("Aux2 Palette [108]"),
new PaletteDescription("Aux2 Palette [109]"),
new PaletteDescription("Aux2 Palette [110]"),
new PaletteDescription("Aux2 Palette [111]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Anim Palette [113]"),
new PaletteDescription("Anim Palette [114]"),
new PaletteDescription("Anim Palette [115]"),
new PaletteDescription("Anim Palette [116]"),
new PaletteDescription("Anim Palette [117]"),
new PaletteDescription("Anim Palette [118]"),
new PaletteDescription("Anim Palette [119]"),
new PaletteDescription("Grass Color"),
new PaletteDescription("Aux2 Palette [121]"),
new PaletteDescription("Aux2 Palette [122]"),
new PaletteDescription("Aux2 Palette [123]"),
new PaletteDescription("Aux2 Palette [124]"),
new PaletteDescription("Aux2 Palette [125]"),
new PaletteDescription("Aux2 Palette [126]"),
new PaletteDescription("Aux2 Palette [127]"),
        };
    }
}
