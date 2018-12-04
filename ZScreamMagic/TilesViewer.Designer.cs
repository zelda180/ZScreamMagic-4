namespace ZScreamMagic
{
    partial class TilesViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesViewer));
            this.mainTilesDisplay = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.usedPaletteButton = new System.Windows.Forms.ToolStripButton();
            this.animateButton = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mainTilesDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTilesDisplay
            // 
            this.mainTilesDisplay.Location = new System.Drawing.Point(0, 0);
            this.mainTilesDisplay.Name = "mainTilesDisplay";
            this.mainTilesDisplay.Size = new System.Drawing.Size(4096, 4096);
            this.mainTilesDisplay.TabIndex = 0;
            this.mainTilesDisplay.TabStop = false;
            this.mainTilesDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.mainTilesDisplay_Paint);
            this.mainTilesDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainTilesDisplay_MouseDown);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.mainTilesDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 398);
            this.panel1.TabIndex = 1;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usedPaletteButton,
            this.animateButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(516, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // usedPaletteButton
            // 
            this.usedPaletteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.usedPaletteButton.Image = ((System.Drawing.Image)(resources.GetObject("usedPaletteButton.Image")));
            this.usedPaletteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.usedPaletteButton.Name = "usedPaletteButton";
            this.usedPaletteButton.Size = new System.Drawing.Size(23, 22);
            this.usedPaletteButton.Text = "Used Palettes";
            this.usedPaletteButton.Click += new System.EventHandler(this.usedPaletteButton_Click);
            // 
            // animateButton
            // 
            this.animateButton.CheckOnClick = true;
            this.animateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.animateButton.Image = ((System.Drawing.Image)(resources.GetObject("animateButton.Image")));
            this.animateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.animateButton.Name = "animateButton";
            this.animateButton.Size = new System.Drawing.Size(61, 22);
            this.animateButton.Text = "Animate?";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 133;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TilesViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 423);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TilesViewer";
            this.Text = "TilesViewer";
            this.Load += new System.EventHandler(this.TilesViewer_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilesViewer_Scroll);
            ((System.ComponentModel.ISupportInitialize)(this.mainTilesDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainTilesDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton usedPaletteButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton animateButton;
    }
}