namespace PrintPDV.UI
{
    partial class PrintPDVVoucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPDVVoucher));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.txtVoucherCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCampaigns = new System.Windows.Forms.DataGridView();
            this.dgvStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTemplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCampaignItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvVoucherItems = new System.Windows.Forms.DataGridView();
            this.dgvCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvUsedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblDetailPrintedNum = new System.Windows.Forms.Label();
            this.lblDetailTrackedNum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.picTemplatePreview = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCampaigns)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherItems)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTemplatePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnValidate);
            this.groupBox1.Controls.Add(this.txtVoucherCode);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnValidate
            // 
            resources.ApplyResources(this.btnValidate, "btnValidate");
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtVoucherCode
            // 
            resources.ApplyResources(this.txtVoucherCode, "txtVoucherCode");
            this.txtVoucherCode.Name = "txtVoucherCode";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.dgvCampaigns);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dgvCampaigns
            // 
            this.dgvCampaigns.AllowUserToAddRows = false;
            this.dgvCampaigns.AllowUserToDeleteRows = false;
            this.dgvCampaigns.AllowUserToResizeColumns = false;
            this.dgvCampaigns.AllowUserToResizeRows = false;
            this.dgvCampaigns.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvCampaigns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCampaigns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStatus,
            this.dgvTemplate,
            this.dgvCampaignItem});
            resources.ApplyResources(this.dgvCampaigns, "dgvCampaigns");
            this.dgvCampaigns.MultiSelect = false;
            this.dgvCampaigns.Name = "dgvCampaigns";
            this.dgvCampaigns.RowHeadersVisible = false;
            this.dgvCampaigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCampaigns.ShowCellErrors = false;
            this.dgvCampaigns.ShowCellToolTips = false;
            this.dgvCampaigns.ShowEditingIcon = false;
            this.dgvCampaigns.ShowRowErrors = false;
            this.dgvCampaigns.SelectionChanged += new System.EventHandler(this.dgvCampaign_SelectionChanged);
            // 
            // dgvStatus
            // 
            this.dgvStatus.FillWeight = 63.9594F;
            resources.ApplyResources(this.dgvStatus, "dgvStatus");
            this.dgvStatus.Name = "dgvStatus";
            // 
            // dgvTemplate
            // 
            this.dgvTemplate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvTemplate.FillWeight = 118.0203F;
            resources.ApplyResources(this.dgvTemplate, "dgvTemplate");
            this.dgvTemplate.Name = "dgvTemplate";
            this.dgvTemplate.ReadOnly = true;
            // 
            // dgvCampaignItem
            // 
            resources.ApplyResources(this.dgvCampaignItem, "dgvCampaignItem");
            this.dgvCampaignItem.Name = "dgvCampaignItem";
            this.dgvCampaignItem.ReadOnly = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.dgvVoucherItems);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // dgvVoucherItems
            // 
            this.dgvVoucherItems.AllowUserToAddRows = false;
            this.dgvVoucherItems.AllowUserToDeleteRows = false;
            this.dgvVoucherItems.AllowUserToResizeColumns = false;
            this.dgvVoucherItems.AllowUserToResizeRows = false;
            this.dgvVoucherItems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvVoucherItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVoucherItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCode,
            this.dgvCreatedDate,
            this.dgvUsedDate});
            resources.ApplyResources(this.dgvVoucherItems, "dgvVoucherItems");
            this.dgvVoucherItems.MultiSelect = false;
            this.dgvVoucherItems.Name = "dgvVoucherItems";
            this.dgvVoucherItems.RowHeadersVisible = false;
            this.dgvVoucherItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucherItems.ShowCellErrors = false;
            this.dgvVoucherItems.ShowCellToolTips = false;
            this.dgvVoucherItems.ShowEditingIcon = false;
            this.dgvVoucherItems.ShowRowErrors = false;
            // 
            // dgvCode
            // 
            resources.ApplyResources(this.dgvCode, "dgvCode");
            this.dgvCode.Name = "dgvCode";
            this.dgvCode.ReadOnly = true;
            // 
            // dgvCreatedDate
            // 
            this.dgvCreatedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.dgvCreatedDate, "dgvCreatedDate");
            this.dgvCreatedDate.Name = "dgvCreatedDate";
            this.dgvCreatedDate.ReadOnly = true;
            // 
            // dgvUsedDate
            // 
            this.dgvUsedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.dgvUsedDate, "dgvUsedDate");
            this.dgvUsedDate.Name = "dgvUsedDate";
            this.dgvUsedDate.ReadOnly = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.lblDetailPrintedNum);
            this.groupBox4.Controls.Add(this.lblDetailTrackedNum);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // lblDetailPrintedNum
            // 
            resources.ApplyResources(this.lblDetailPrintedNum, "lblDetailPrintedNum");
            this.lblDetailPrintedNum.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailPrintedNum.Name = "lblDetailPrintedNum";
            // 
            // lblDetailTrackedNum
            // 
            resources.ApplyResources(this.lblDetailTrackedNum, "lblDetailTrackedNum");
            this.lblDetailTrackedNum.ForeColor = System.Drawing.Color.Blue;
            this.lblDetailTrackedNum.Name = "lblDetailTrackedNum";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.picTemplatePreview);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // picTemplatePreview
            // 
            resources.ApplyResources(this.picTemplatePreview, "picTemplatePreview");
            this.picTemplatePreview.Name = "picTemplatePreview";
            this.picTemplatePreview.TabStop = false;
            // 
            // PrintPDVVoucher
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDVVoucher";
            this.Load += new System.EventHandler(this.PrintPdvVoucher_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCampaigns)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherItems)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTemplatePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtVoucherCode;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvCampaigns;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvVoucherItems;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDetailPrintedNum;
        private System.Windows.Forms.Label lblDetailTrackedNum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox picTemplatePreview;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTemplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCampaignItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCreatedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvUsedDate;
    }
}