using ImageButton = PrintPDV.UI.Components.ImageButton;

namespace PrintPDV.UI
{
    partial class PrintPDVGadget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPDVGadget));
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ibtPrint = new PrintPDV.UI.Components.ImageButton(this.components);
            this.btnDrag = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.DarkGray;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = global::PrintPDV.UI.Properties.Resources.g_exit;
            this.btnExit.Location = new System.Drawing.Point(101, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnExit.Size = new System.Drawing.Size(30, 30);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ibtPrint);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 1;
            // 
            // ibtPrint
            // 
            this.ibtPrint.BackColor = System.Drawing.Color.Transparent;
            this.ibtPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ibtPrint.ForeColorDown = System.Drawing.Color.Empty;
            this.ibtPrint.ForeColorOver = System.Drawing.Color.Empty;
            this.ibtPrint.ImageDisabled = null;
            this.ibtPrint.ImageDown = global::PrintPDV.UI.Properties.Resources.g_print_down;
            this.ibtPrint.ImageOver = global::PrintPDV.UI.Properties.Resources.g_print_up;
            this.ibtPrint.ImageSelected = global::PrintPDV.UI.Properties.Resources.g_print_up;
            this.ibtPrint.ImageUp = global::PrintPDV.UI.Properties.Resources.g_print_up;
            this.ibtPrint.IsDefault = false;
            this.ibtPrint.Location = new System.Drawing.Point(10, 10);
            this.ibtPrint.Name = "ibtPrint";
            this.ibtPrint.Selected = false;
            this.ibtPrint.Size = new System.Drawing.Size(80, 80);
            this.ibtPrint.TabIndex = 0;
            this.ibtPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ibtPrint.TextLocation = new System.Drawing.Point(0, 0);
            this.ibtPrint.TextLocationOverride = PrintPDV.UI.Components.ImageButton.TextDrawOptions.ByAlignment;
            this.ibtPrint.TextMarginHeight = 0;
            this.ibtPrint.TextMarginWidth = 0;
            this.ibtPrint.Click += new System.EventHandler(this.ibtPrint_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.AutoSize = true;
            this.btnDrag.BackColor = System.Drawing.Color.DarkGray;
            this.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrag.Image = global::PrintPDV.UI.Properties.Resources.g_move;
            this.btnDrag.Location = new System.Drawing.Point(101, 31);
            this.btnDrag.Margin = new System.Windows.Forms.Padding(0);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.btnDrag.Size = new System.Drawing.Size(30, 31);
            this.btnDrag.TabIndex = 0;
            this.btnDrag.UseVisualStyleBackColor = false;
            this.btnDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDrag_MouseDown);
            // 
            // PrintPDVGadget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(132, 100);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDVGadget";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDrag;
        private ImageButton ibtPrint;
    }
}