namespace ZScreamMagic
{
    partial class ProjectForm
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
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Overworld");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Dungeon");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Light World");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Dark World");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("World Maps", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Monologues");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Palettes");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Dungeons Maps");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Intro Editor");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Menu Screens");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("ASM");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("GFX Editor");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Tiles Viewer");
            this.projectTreeview = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // projectTreeview
            // 
            this.projectTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTreeview.Location = new System.Drawing.Point(0, 0);
            this.projectTreeview.Name = "projectTreeview";
            treeNode14.Name = "Node_Overworld";
            treeNode14.Text = "Overworld";
            treeNode15.Name = "Node_Dungeon";
            treeNode15.Text = "Dungeon";
            treeNode16.Name = "Node_LWMap";
            treeNode16.Text = "Light World";
            treeNode17.Name = "Node_DWMap";
            treeNode17.Text = "Dark World";
            treeNode18.Name = "Node_WorldMaps";
            treeNode18.Text = "World Maps";
            treeNode19.Name = "Node_Monologues";
            treeNode19.Text = "Monologues";
            treeNode20.Name = "Node_Palettes";
            treeNode20.Text = "Palettes";
            treeNode21.Name = "Node_DungeonsMaps";
            treeNode21.Text = "Dungeons Maps";
            treeNode22.Name = "Node_IntroEditor";
            treeNode22.Text = "Intro Editor";
            treeNode23.Name = "Node_MenuScreens";
            treeNode23.Text = "Menu Screens";
            treeNode24.Name = "Node_ASM";
            treeNode24.Text = "ASM";
            treeNode25.Name = "Node_GFX";
            treeNode25.Text = "GFX Editor";
            treeNode26.Name = "tileViewer";
            treeNode26.Text = "Tiles Viewer";
            this.projectTreeview.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            this.projectTreeview.Size = new System.Drawing.Size(800, 450);
            this.projectTreeview.TabIndex = 0;
            this.projectTreeview.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectTreeview_NodeMouseDoubleClick);
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.projectTreeview);
            this.Name = "ProjectForm";
            this.Text = "Project Form";
            this.Load += new System.EventHandler(this.ProjectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projectTreeview;
    }
}