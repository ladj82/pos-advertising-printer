namespace PrintPDV.UI
{
    partial class PrintPDVContainer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPDVContainer));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbEditor = new System.Windows.Forms.ToolStripButton();
            this.tsbCampaing = new System.Windows.Forms.ToolStripButton();
            this.tsbGadget = new System.Windows.Forms.ToolStripButton();
            this.tsbVoucher = new System.Windows.Forms.ToolStripButton();
            this.tsbPrinter = new System.Windows.Forms.ToolStripButton();
            this.tsbStatistic = new System.Windows.Forms.ToolStripButton();
            this.tsbLicense = new System.Windows.Forms.ToolStripButton();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusBar);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContent);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // statusBar
            // 
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel});
            this.statusBar.Name = "statusBar";
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Name = "statusBarLabel";
            resources.ApplyResources(this.statusBarLabel, "statusBarLabel");
            // 
            // pnlContent
            // 
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.Name = "pnlContent";
            // 
            // tsMenu
            // 
            this.tsMenu.AllowMerge = false;
            resources.ApplyResources(this.tsMenu, "tsMenu");
            this.tsMenu.BackColor = System.Drawing.Color.Silver;
            this.tsMenu.CanOverflow = false;
            this.tsMenu.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEditor,
            this.tsbCampaing,
            this.tsbGadget,
            this.tsbVoucher,
            this.tsbPrinter,
            this.tsbStatistic,
            this.tsbLicense});
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMenu.ShowItemToolTips = false;
            this.tsMenu.Stretch = true;
            // 
            // tsbEditor
            // 
            resources.ApplyResources(this.tsbEditor, "tsbEditor");
            this.tsbEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditor.Image = global::PrintPDV.UI.Properties.Resources.m_editor;
            this.tsbEditor.Margin = new System.Windows.Forms.Padding(0);
            this.tsbEditor.Name = "tsbEditor";
            this.tsbEditor.Click += new System.EventHandler(this.tsbEditor_Click);
            // 
            // tsbCampaing
            // 
            resources.ApplyResources(this.tsbCampaing, "tsbCampaing");
            this.tsbCampaing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCampaing.Image = global::PrintPDV.UI.Properties.Resources.m_campaign;
            this.tsbCampaing.Margin = new System.Windows.Forms.Padding(0);
            this.tsbCampaing.Name = "tsbCampaing";
            this.tsbCampaing.Click += new System.EventHandler(this.tsbCampaing_Click);
            // 
            // tsbGadget
            // 
            resources.ApplyResources(this.tsbGadget, "tsbGadget");
            this.tsbGadget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGadget.Image = global::PrintPDV.UI.Properties.Resources.m_gadget;
            this.tsbGadget.Name = "tsbGadget";
            this.tsbGadget.Click += new System.EventHandler(this.tsbGadget_Click);
            // 
            // tsbVoucher
            // 
            resources.ApplyResources(this.tsbVoucher, "tsbVoucher");
            this.tsbVoucher.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVoucher.Image = global::PrintPDV.UI.Properties.Resources.m_tracker;
            this.tsbVoucher.Name = "tsbVoucher";
            this.tsbVoucher.Click += new System.EventHandler(this.tsbVoucher_Click);
            // 
            // tsbPrinter
            // 
            resources.ApplyResources(this.tsbPrinter, "tsbPrinter");
            this.tsbPrinter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrinter.Image = global::PrintPDV.UI.Properties.Resources.m_printer;
            this.tsbPrinter.Margin = new System.Windows.Forms.Padding(0);
            this.tsbPrinter.Name = "tsbPrinter";
            this.tsbPrinter.Click += new System.EventHandler(this.tsbPrinter_Click);
            // 
            // tsbStatistic
            // 
            resources.ApplyResources(this.tsbStatistic, "tsbStatistic");
            this.tsbStatistic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStatistic.Image = global::PrintPDV.UI.Properties.Resources.m_statistic;
            this.tsbStatistic.Margin = new System.Windows.Forms.Padding(0);
            this.tsbStatistic.Name = "tsbStatistic";
            this.tsbStatistic.Click += new System.EventHandler(this.tsbStatistic_Click);
            // 
            // tsbLicense
            // 
            resources.ApplyResources(this.tsbLicense, "tsbLicense");
            this.tsbLicense.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLicense.Image = global::PrintPDV.UI.Properties.Resources.m_license;
            this.tsbLicense.Margin = new System.Windows.Forms.Padding(0);
            this.tsbLicense.Name = "tsbLicense";
            this.tsbLicense.Click += new System.EventHandler(this.tsbLicense_Click);
            // 
            // BottomToolStripPanel
            // 
            resources.ApplyResources(this.BottomToolStripPanel, "BottomToolStripPanel");
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // TopToolStripPanel
            // 
            resources.ApplyResources(this.TopToolStripPanel, "TopToolStripPanel");
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // RightToolStripPanel
            // 
            resources.ApplyResources(this.RightToolStripPanel, "RightToolStripPanel");
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // LeftToolStripPanel
            // 
            resources.ApplyResources(this.LeftToolStripPanel, "LeftToolStripPanel");
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // ContentPanel
            // 
            resources.ApplyResources(this.ContentPanel, "ContentPanel");
            // 
            // PrintPDVContainer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.tsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDVContainer";
            this.Load += new System.EventHandler(this.PrintPDVContainer_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbCampaing;
        private System.Windows.Forms.ToolStripButton tsbEditor;
        private System.Windows.Forms.ToolStripButton tsbPrinter;
        private System.Windows.Forms.ToolStripButton tsbLicense;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ToolStripButton tsbVoucher;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripButton tsbStatistic;
        private System.Windows.Forms.ToolStripButton tsbGadget;
    }
}