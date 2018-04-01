namespace PrintPDV.UI
{
    partial class PrintPDVCampaign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPDVCampaign));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCampaign = new System.Windows.Forms.DataGridView();
            this.dgvStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvShortcut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picTemplatePreview = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDetailModified = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDetailCreated = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDetailShortcut = new System.Windows.Forms.Label();
            this.lblDetailStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDetailName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCampaign)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTemplatePreview)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.dgvCampaign);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // dgvCampaign
            // 
            this.dgvCampaign.AllowUserToAddRows = false;
            this.dgvCampaign.AllowUserToDeleteRows = false;
            this.dgvCampaign.AllowUserToResizeColumns = false;
            this.dgvCampaign.AllowUserToResizeRows = false;
            this.dgvCampaign.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvCampaign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCampaign.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStatus,
            this.dgvTemplate,
            this.dgvShortcut,
            this.dgvEdit,
            this.dgvDelete,
            this.dgvItem});
            resources.ApplyResources(this.dgvCampaign, "dgvCampaign");
            this.dgvCampaign.MultiSelect = false;
            this.dgvCampaign.Name = "dgvCampaign";
            this.dgvCampaign.RowHeadersVisible = false;
            this.dgvCampaign.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCampaign.ShowCellErrors = false;
            this.dgvCampaign.ShowCellToolTips = false;
            this.dgvCampaign.ShowEditingIcon = false;
            this.dgvCampaign.ShowRowErrors = false;
            this.dgvCampaign.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCampaign_CellContentClick);
            this.dgvCampaign.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCampaign_CellValueChanged);
            this.dgvCampaign.SelectionChanged += new System.EventHandler(this.dgvCampaign_SelectionChanged);
            // 
            // dgvStatus
            // 
            this.dgvStatus.FillWeight = 63.9594F;
            resources.ApplyResources(this.dgvStatus, "dgvStatus");
            this.dgvStatus.Name = "dgvStatus";
            // 
            // dgvTemplate
            // 
            this.dgvTemplate.FillWeight = 118.0203F;
            resources.ApplyResources(this.dgvTemplate, "dgvTemplate");
            this.dgvTemplate.Name = "dgvTemplate";
            this.dgvTemplate.ReadOnly = true;
            // 
            // dgvShortcut
            // 
            this.dgvShortcut.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvShortcut.FillWeight = 118.0203F;
            resources.ApplyResources(this.dgvShortcut, "dgvShortcut");
            this.dgvShortcut.Name = "dgvShortcut";
            this.dgvShortcut.ReadOnly = true;
            // 
            // dgvEdit
            // 
            resources.ApplyResources(this.dgvEdit, "dgvEdit");
            this.dgvEdit.Name = "dgvEdit";
            // 
            // dgvDelete
            // 
            resources.ApplyResources(this.dgvDelete, "dgvDelete");
            this.dgvDelete.Name = "dgvDelete";
            // 
            // dgvItem
            // 
            resources.ApplyResources(this.dgvItem, "dgvItem");
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.picTemplatePreview);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // picTemplatePreview
            // 
            resources.ApplyResources(this.picTemplatePreview, "picTemplatePreview");
            this.picTemplatePreview.Name = "picTemplatePreview";
            this.picTemplatePreview.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.lblDetailModified);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblDetailCreated);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblDetailShortcut);
            this.groupBox3.Controls.Add(this.lblDetailStatus);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.lblDetailName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lblDetailModified
            // 
            resources.ApplyResources(this.lblDetailModified, "lblDetailModified");
            this.lblDetailModified.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailModified.Name = "lblDetailModified";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lblDetailCreated
            // 
            resources.ApplyResources(this.lblDetailCreated, "lblDetailCreated");
            this.lblDetailCreated.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailCreated.Name = "lblDetailCreated";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblDetailShortcut
            // 
            resources.ApplyResources(this.lblDetailShortcut, "lblDetailShortcut");
            this.lblDetailShortcut.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailShortcut.Name = "lblDetailShortcut";
            // 
            // lblDetailStatus
            // 
            resources.ApplyResources(this.lblDetailStatus, "lblDetailStatus");
            this.lblDetailStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailStatus.Name = "lblDetailStatus";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblDetailName
            // 
            resources.ApplyResources(this.lblDetailName, "lblDetailName");
            this.lblDetailName.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailName.Name = "lblDetailName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // PrintPDVCampaign
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDVCampaign";
            this.Load += new System.EventHandler(this.PrintPDVConfiguration_Load);
            this.Enter += new System.EventHandler(this.PrintPDVCampaign_Enter);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCampaign)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTemplatePreview)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picTemplatePreview;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDetailCreated;
        private System.Windows.Forms.Label lblDetailShortcut;
        private System.Windows.Forms.Label lblDetailStatus;
        private System.Windows.Forms.Label lblDetailName;
        private System.Windows.Forms.Label lblDetailModified;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvCampaign;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTemplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvShortcut;
        private System.Windows.Forms.DataGridViewButtonColumn dgvEdit;
        private System.Windows.Forms.DataGridViewButtonColumn dgvDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvItem;
    }
}