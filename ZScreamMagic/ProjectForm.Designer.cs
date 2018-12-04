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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Overworld");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Dungeon");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Light World");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Dark World");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("World Maps", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Monologues");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Swords");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Shields");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Mails");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Dungeons");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Main Overworld");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Aux Overworld");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Animated Overworld");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Hud");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Main Global Sprites");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Auxiliary Sprites 1");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Auxiliary Sprites 2");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Auxiliary Sprites 3");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("World Map");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Dungeon Map");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Triforce");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Crystal");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("HardCoded");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Palettes", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Dungeons Maps");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Intro Editor");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Menu Screens");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("ASM");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("GFX Editor");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Tiles Viewer");
            this.projectTreeview = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // projectTreeview
            // 
            this.projectTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTreeview.Location = new System.Drawing.Point(0, 0);
            this.projectTreeview.Name = "projectTreeview";
            treeNode1.Name = "Node_Overworld";
            treeNode1.Text = "Overworld";
            treeNode2.Name = "Node_Dungeon";
            treeNode2.Text = "Dungeon";
            treeNode3.Name = "Node_LWMap";
            treeNode3.Text = "Light World";
            treeNode4.Name = "Node_DWMap";
            treeNode4.Text = "Dark World";
            treeNode5.Name = "Node_WorldMaps";
            treeNode5.Text = "World Maps";
            treeNode6.Name = "Node_Monologues";
            treeNode6.Text = "Monologues";
            treeNode7.Name = "sword_pal";
            treeNode7.Text = "Swords";
            treeNode8.Name = "shield_pal";
            treeNode8.Text = "Shields";
            treeNode9.Name = "mail_pal";
            treeNode9.Text = "Mails";
            treeNode10.Name = "dungeons_pal";
            treeNode10.Text = "Dungeons";
            treeNode11.Name = "mainoverworld_pal";
            treeNode11.Text = "Main Overworld";
            treeNode12.Name = "auxoverworld_pal";
            treeNode12.Text = "Aux Overworld";
            treeNode13.Name = "animatedoverworld_pal";
            treeNode13.Text = "Animated Overworld";
            treeNode14.Name = "hud_pal";
            treeNode14.Text = "Hud";
            treeNode15.Name = "mainglobalsprite_pal";
            treeNode15.Text = "Main Global Sprites";
            treeNode16.Name = "spriteaux1_pal";
            treeNode16.Text = "Auxiliary Sprites 1";
            treeNode17.Name = "spriteaux2_pal";
            treeNode17.Text = "Auxiliary Sprites 2";
            treeNode18.Name = "spriteaux3_pal";
            treeNode18.Text = "Auxiliary Sprites 3";
            treeNode19.Name = "worldmap_pal";
            treeNode19.Text = "World Map";
            treeNode20.Name = "dungeonmap_pal";
            treeNode20.Text = "Dungeon Map";
            treeNode21.Name = "triforce_pal";
            treeNode21.Text = "Triforce";
            treeNode22.Name = "crystal_pal";
            treeNode22.Text = "Crystal";
            treeNode23.Name = "hardcoded_pal";
            treeNode23.Text = "HardCoded";
            treeNode24.Name = "Node_Palettes";
            treeNode24.Text = "Palettes";
            treeNode25.Name = "Node_DungeonsMaps";
            treeNode25.Text = "Dungeons Maps";
            treeNode26.Name = "Node_IntroEditor";
            treeNode26.Text = "Intro Editor";
            treeNode27.Name = "Node_MenuScreens";
            treeNode27.Text = "Menu Screens";
            treeNode28.Name = "Node_ASM";
            treeNode28.Text = "ASM";
            treeNode29.Name = "Node_GFX";
            treeNode29.Text = "GFX Editor";
            treeNode30.Name = "tileViewer";
            treeNode30.Text = "Tiles Viewer";
            this.projectTreeview.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode5,
            treeNode6,
            treeNode24,
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30});
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