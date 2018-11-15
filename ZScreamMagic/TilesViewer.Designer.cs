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
            this.mainTilesDisplay = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mainTilesDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTilesDisplay
            // 
            this.mainTilesDisplay.Location = new System.Drawing.Point(3, 3);
            this.mainTilesDisplay.Name = "mainTilesDisplay";
            this.mainTilesDisplay.Size = new System.Drawing.Size(4096, 4096);
            this.mainTilesDisplay.TabIndex = 0;
            this.mainTilesDisplay.TabStop = false;
            this.mainTilesDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.mainTilesDisplay_Paint);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.mainTilesDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 423);
            this.panel1.TabIndex = 1;
            // 
            // TilesViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 423);
            this.Controls.Add(this.panel1);
            this.Name = "TilesViewer";
            this.Text = "TilesViewer";
            this.Load += new System.EventHandler(this.TilesViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainTilesDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainTilesDisplay;
        private System.Windows.Forms.Panel panel1;
    }
}