namespace PrintPDV.UI.Components
{
    partial class WizardPrinterSetup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardPrinterSetup));
            this.gbxPrinterConfig = new System.Windows.Forms.GroupBox();
            this.gbxCompatiblePrinters = new System.Windows.Forms.GroupBox();
            this.lbxCompatiblePrinters = new System.Windows.Forms.ListBox();
            this.lblPrinterNotFound = new System.Windows.Forms.Label();
            this.lblFoundPrinters = new System.Windows.Forms.Label();
            this.cboPrinters = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.gbxPrinterConfig.SuspendLayout();
            this.gbxCompatiblePrinters.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPrinterConfig
            // 
            resources.ApplyResources(this.gbxPrinterConfig, "gbxPrinterConfig");
            this.gbxPrinterConfig.Controls.Add(this.gbxCompatiblePrinters);
            this.gbxPrinterConfig.Controls.Add(this.lblPrinterNotFound);
            this.gbxPrinterConfig.Controls.Add(this.lblFoundPrinters);
            this.gbxPrinterConfig.Controls.Add(this.cboPrinters);
            this.gbxPrinterConfig.Controls.Add(this.btnSave);
            this.gbxPrinterConfig.Controls.Add(this.btnReload);
            this.gbxPrinterConfig.Controls.Add(this.btnTest);
            this.gbxPrinterConfig.Name = "gbxPrinterConfig";
            this.gbxPrinterConfig.TabStop = false;
            // 
            // gbxCompatiblePrinters
            // 
            resources.ApplyResources(this.gbxCompatiblePrinters, "gbxCompatiblePrinters");
            this.gbxCompatiblePrinters.Controls.Add(this.lbxCompatiblePrinters);
            this.gbxCompatiblePrinters.Name = "gbxCompatiblePrinters";
            this.gbxCompatiblePrinters.TabStop = false;
            // 
            // lbxCompatiblePrinters
            // 
            resources.ApplyResources(this.lbxCompatiblePrinters, "lbxCompatiblePrinters");
            this.lbxCompatiblePrinters.FormattingEnabled = true;
            this.lbxCompatiblePrinters.Name = "lbxCompatiblePrinters";
            this.lbxCompatiblePrinters.SelectionMode = System.Windows.Forms.SelectionMode.None;
            // 
            // lblPrinterNotFound
            // 
            resources.ApplyResources(this.lblPrinterNotFound, "lblPrinterNotFound");
            this.lblPrinterNotFound.ForeColor = System.Drawing.Color.Red;
            this.lblPrinterNotFound.Name = "lblPrinterNotFound";
            // 
            // lblFoundPrinters
            // 
            resources.ApplyResources(this.lblFoundPrinters, "lblFoundPrinters");
            this.lblFoundPrinters.Name = "lblFoundPrinters";
            // 
            // cboPrinters
            // 
            resources.ApplyResources(this.cboPrinters, "cboPrinters");
            this.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Name = "cboPrinters";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReload
            // 
            resources.ApplyResources(this.btnReload, "btnReload");
            this.btnReload.Name = "btnReload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // WizardPrinterSetup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxPrinterConfig);
            this.Name = "WizardPrinterSetup";
            this.gbxPrinterConfig.ResumeLayout(false);
            this.gbxPrinterConfig.PerformLayout();
            this.gbxCompatiblePrinters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPrinterConfig;
        private System.Windows.Forms.GroupBox gbxCompatiblePrinters;
        private System.Windows.Forms.ListBox lbxCompatiblePrinters;
        private System.Windows.Forms.Label lblPrinterNotFound;
        private System.Windows.Forms.Label lblFoundPrinters;
        private System.Windows.Forms.ComboBox cboPrinters;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnTest;
    }
}
