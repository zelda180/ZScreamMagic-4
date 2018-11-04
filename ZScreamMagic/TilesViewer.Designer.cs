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
            ((System.ComponentModel.ISupportInitialize)(this.mainTilesDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTilesDisplay
            // 
            this.mainTilesDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTilesDisplay.Location = new System.Drawing.Point(0, 0);
            this.mainTilesDisplay.Name = "mainTilesDisplay";
            this.mainTilesDisplay.Size = new System.Drawing.Size(516, 423);
            this.mainTilesDisplay.TabIndex = 0;
            this.mainTilesDisplay.TabStop = false;
            this.mainTilesDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.mainTilesDisplay_Paint);
            // 
            // TilesViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 423);
            this.Controls.Add(this.mainTilesDisplay);
            this.Name = "TilesViewer";
            this.Text = "TilesViewer";
            this.Load += new System.EventHandler(this.TilesViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainTilesDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainTilesDisplay;
    }
}