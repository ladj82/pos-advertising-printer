namespace PrintPDV.UI
{
    partial class PrintPDVStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPDVStatistic));
            this.gbxPrinterConfig = new System.Windows.Forms.GroupBox();
            this.dgvResultset = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.gbxPrinterConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultset)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxPrinterConfig
            // 
            resources.ApplyResources(this.gbxPrinterConfig, "gbxPrinterConfig");
            this.gbxPrinterConfig.Controls.Add(this.dgvResultset);
            this.gbxPrinterConfig.Controls.Add(this.btnSearch);
            this.gbxPrinterConfig.Controls.Add(this.dtpEndDate);
            this.gbxPrinterConfig.Controls.Add(this.label1);
            this.gbxPrinterConfig.Controls.Add(this.dtpStartDate);
            this.gbxPrinterConfig.Controls.Add(this.label3);
            this.gbxPrinterConfig.Controls.Add(this.label2);
            this.gbxPrinterConfig.Controls.Add(this.lblTotal);
            this.gbxPrinterConfig.Name = "gbxPrinterConfig";
            this.gbxPrinterConfig.TabStop = false;
            // 
            // dgvResultset
            // 
            resources.ApplyResources(this.dgvResultset, "dgvResultset");
            this.dgvResultset.AllowUserToAddRows = false;
            this.dgvResultset.AllowUserToDeleteRows = false;
            this.dgvResultset.AllowUserToOrderColumns = true;
            this.dgvResultset.AllowUserToResizeColumns = false;
            this.dgvResultset.AllowUserToResizeRows = false;
            this.dgvResultset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultset.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvResultset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultset.Name = "dgvResultset";
            this.dgvResultset.ReadOnly = true;
            this.dgvResultset.RowHeadersVisible = false;
            this.dgvResultset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpEndDate
            // 
            resources.ApplyResources(this.dtpEndDate, "dtpEndDate");
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Name = "dtpEndDate";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dtpStartDate
            // 
            resources.ApplyResources(this.dtpStartDate, "dtpStartDate");
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Name = "dtpStartDate";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Name = "lblTotal";
            // 
            // PrintPDVStatistic
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxPrinterConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDVStatistic";
            this.Load += new System.EventHandler(this.PrintPDVStatistic_Load);
            this.Enter += new System.EventHandler(this.PrintPDVWizard_Enter);
            this.gbxPrinterConfig.ResumeLayout(false);
            this.gbxPrinterConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPrinterConfig;
        private System.Windows.Forms.DataGridView dgvResultset;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;


    }
}