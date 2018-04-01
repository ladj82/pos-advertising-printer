using PrintPDV.UI.Components;
using PrintPDV.Utility.Models;

namespace PrintPDV.UI
{
    partial class PrintPDVEditor2
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.editor = new PrintPDV.UI.Components.Editor2();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // editor
            // 
            this.editor.CampaignBorderStyle = "Black";
            this.editor.CampaignBorderWidth = 20;
            this.editor.CampaignCutType = PrintPDV.Utility.Models.Enumerations.CutType.Nenhum;
            this.editor.CampaignId = 0;
            this.editor.CampaignName = "";
            this.editor.CampaignPaperSize = PrintPDV.Utility.Models.Enumerations.PaperSize.Tamanho_4;
            this.editor.CampaignPrintDateTime = true;
            this.editor.CampaignShortcut = "";
            this.editor.CampaignSource = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1046{\\fonttbl{\\f0\\fnil\\fcharset0 Arial Narro" +
    "w;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs29\\par\r\n}\r\n";
            this.editor.CampaignType = PrintPDV.Utility.Models.Enumerations.CampaignType.Desconto;
            this.editor.CampaignVoucherBarcodeType = PrintPDV.Utility.Models.Enumerations.BarcodeType.Nenhum;
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(784, 662);
            this.editor.TabIndex = 0;
            this.editor.OnNewCommand += new PrintPDV.UI.Components.Editor2.EditorOnNewCommand(this.editor_OnNewCommand);
            this.editor.OnOpenCommand += new PrintPDV.UI.Components.Editor2.EditorOnOpenCommand(this.editor_OnOpenCommand);
            this.editor.OnSaveCommand += new PrintPDV.UI.Components.Editor2.EditorOnSaveCommand(this.editor_OnSaveCommand);
            this.editor.OnPrintCommand += new PrintPDV.UI.Components.Editor2.EditorOnPrintCommand(this.editor_OnPrintCommand);
            this.editor.OnCutCommand += new PrintPDV.UI.Components.Editor2.EditorOnCutCommand(this.editor_OnCutCommand);
            this.editor.OnCopyCommand += new PrintPDV.UI.Components.Editor2.EditorOnCopyCommand(this.editor_OnCopyCommand);
            this.editor.OnPasteCommand += new PrintPDV.UI.Components.Editor2.EditorOnPasteCommand(this.editor_OnPasteCommand);
            this.editor.OnUndoCommand += new PrintPDV.UI.Components.Editor2.EditorOnUndoCommand(this.editor_OnUndoCommand);
            this.editor.OnRedoCommand += new PrintPDV.UI.Components.Editor2.EditorOnRedoCommand(this.editor_OnRedoCommand);
            this.editor.OnBoldCommand += new PrintPDV.UI.Components.Editor2.EditorOnBoldCommand(this.editor_OnBoldCommand);
            this.editor.OnItalicCommand += new PrintPDV.UI.Components.Editor2.EditorOnItalicCommand(this.editor_OnItalicCommand);
            this.editor.OnUnderlineCommand += new PrintPDV.UI.Components.Editor2.EditorOnUnderlineCommand(this.editor_OnUnderlineCommand);
            this.editor.OnStrikeCommand += new PrintPDV.UI.Components.Editor2.EditorOnStrikeCommand(this.editor_OnStrikeCommand);
            this.editor.OnFontCommand += new PrintPDV.UI.Components.Editor2.EditorOnFontCommand(this.editor_OnFontCommand);
            this.editor.OnFontIncreaseCommand += new PrintPDV.UI.Components.Editor2.EditorOnFontIncreaseCommand(this.editor_OnFontIncreaseCommand);
            this.editor.OnFontDecreaseCommand += new PrintPDV.UI.Components.Editor2.EditorOnFontDecreaseCommand(this.editor_OnFontDecreaseCommand);
            this.editor.OnLeftAlignCommand += new PrintPDV.UI.Components.Editor2.EditorOnLeftAlignCommand(this.editor_OnLeftAlignCommand);
            this.editor.OnCenterAlignCommand += new PrintPDV.UI.Components.Editor2.EditorOnCenterAlignCommand(this.editor_OnCenterAlignCommand);
            this.editor.OnRightAlignCommand += new PrintPDV.UI.Components.Editor2.EditorOnRightAlignCommand(this.editor_OnRightAlignCommand);
            this.editor.OnListCommand += new PrintPDV.UI.Components.Editor2.EditorOnListCommand(this.editor_OnListCommand);
            this.editor.OnOutdentCommand += new PrintPDV.UI.Components.Editor2.EditorOnOutdentCommand(this.editor_OnOutdentCommand);
            this.editor.OnIndentCommand += new PrintPDV.UI.Components.Editor2.EditorOnIndentCommand(this.editor_OnIndentCommand);
            this.editor.OnImageCommand += new PrintPDV.UI.Components.Editor2.EditorOnImageCommand(this.editor_OnImageCommand);
            this.editor.OnApplyTemplateCommand += new PrintPDV.UI.Components.Editor2.EditorOnApplyTemplateCommand(this.editor_OnApplyTemplateCommand);
            this.editor.OnAddTemplateCommand += new PrintPDV.UI.Components.Editor2.EditorOnAddTemplateCommand(this.editor_OnAddTemplateCommand);
            this.editor.OnDeleteTemplateCommand += new PrintPDV.UI.Components.Editor2.EditorOnDeleteTemplateCommand(this.editor_OnDeleteTemplateCommand);
            this.editor.OnChangeCategoryTemplateCommand += new PrintPDV.UI.Components.Editor2.EditorOnChangeCategoryTemplateCommand(this.editor_OnChangeCategoryTemplateCommand);
            this.editor.OnApplyClipartCommand += new PrintPDV.UI.Components.Editor2.EditorOnApplyClipartCommand(this.editor_OnApplyClipartCommand);
            this.editor.OnAddClipartCommand += new PrintPDV.UI.Components.Editor2.EditorOnAddClipartCommand(this.editor_OnAddClipartCommand);
            this.editor.OnDeleteClipartCommand += new PrintPDV.UI.Components.Editor2.EditorOnDeleteClipartCommand(this.editor_OnDeleteClipartCommand);
            this.editor.OnChangeCategoryClipartCommand += new PrintPDV.UI.Components.Editor2.EditorOnChangeCategoryClipartCommand(this.editor_OnChangeCategoryClipartCommand);
            // 
            // PrintPDVEditor2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.editor);
            this.Name = "PrintPDVEditor2";
            this.Text = "PrintPDVEditor2";
            this.Load += new System.EventHandler(this.PrintPDVEditor2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Editor2 editor;
        private System.Windows.Forms.ImageList imageList1;

    }
}