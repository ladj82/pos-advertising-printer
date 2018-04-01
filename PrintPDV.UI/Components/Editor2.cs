using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PrintPDV.Utility;
using Enumerations = PrintPDV.Utility.Models.Enumerations;

namespace PrintPDV.UI.Components
{
    public partial class Editor2 : UserControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #region Attributes

        public int CampaignId { get; set; }

        public string CampaignName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public Enumerations.CampaignType CampaignType
        {
            get
            {
                return (Enumerations.CampaignType)
                    Enum.Parse(typeof (Enumerations.CampaignType),
                        ((KeyValuePair<string, string>) cboCategory.SelectedItem).Value);
            }
            set { cboCategory.Text = value.ToString(); }
        }

        public string CampaignShortcut
        {
            get { return txtShortcut.Text; }
            set { txtShortcut.Text = value; }
        }

        public string CampaignSource
        {
            get { return rtbEditor.Rtf; }
            set { rtbEditor.Rtf = value; }
        }

        public string CampaignBorderStyle
        {
            get { return cboBorderStyle.SelectedItem.ToString(); }
            set { cboBorderStyle.Text = value; }
        }

        public int CampaignBorderWidth
        {
            get { return Convert.ToInt32(nudBorderWidth.Value); }
            set { nudBorderWidth.Value = value; }
        }

        public Enumerations.BarcodeType CampaignVoucherBarcodeType
        {
            get
            {
                return (Enumerations.BarcodeType)
                    Enum.Parse(typeof (Enumerations.BarcodeType),
                        ((KeyValuePair<string, string>) cboVoucherType.SelectedItem).Value);
            }
            set { cboVoucherType.Text = value.ToString(); }
        }

        public bool CampaignPrintDateTime
        {
            get { return chkPrintDateTime.Checked; }
            set { chkPrintDateTime.Checked = value; }
        }

        public Enumerations.PaperSize CampaignPaperSize
        {
            get
            {
                return (Enumerations.PaperSize)
                    Enum.Parse(typeof (Enumerations.PaperSize),
                        ((KeyValuePair<string, string>) cboPaperSize.SelectedItem).Value);
            }
            set { cboPaperSize.Text = value.ToString(); }
        }

        public Enumerations.CutType CampaignCutType
        {
            get
            {
                return (Enumerations.CutType)
                    Enum.Parse(typeof (Enumerations.CutType),
                        ((KeyValuePair<string, string>) cboCutType.SelectedItem).Value);
            }
            set { cboCutType.Text = value.ToString(); }
        }

        public Enumerations.GalleryType GalleryTemplateType
        {
            get
            {
                return (Enumerations.GalleryType)
                    Enum.Parse(typeof (Enumerations.GalleryType),
                        ((KeyValuePair<string, string>) cboGalleryTemplateType.SelectedItem).Value);
            }
        }

        public Enumerations.GalleryType GalleryClipartType
        {
            get
            {
                return (Enumerations.GalleryType)
                    Enum.Parse(typeof (Enumerations.GalleryType),
                        ((KeyValuePair<string, string>) cboGalleryClipartType.SelectedItem).Value);
            }
        }

        #endregion

        private FormUtility.POINT _point;

        private Brush Border { get; set; }

        private int PaperSizeHeight { get; set; }

        public Bitmap GetEditorImage
        {
            get
            {
                var width = pnlBorder.Width;
                var height = pnlBorder.Height;
                var size = pnlBorder.Size;
                var bmp = new Bitmap(width, height);

                FormUtility.GetCursorPos(out _point);

                HandleDirtyScreen(true);

                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CopyFromScreen(pnlBorder.PointToScreen(Point.Empty).X, pnlBorder.PointToScreen(Point.Empty).Y, 0,
                        0, size);
                }

                HandleDirtyScreen(false);

                return bmp;
            }
        }

        public Editor2()
        {
            InitializeComponent();

            Init();
        }

        private void Editor2_Load(object sender, EventArgs e)
        {
            SetEditorDefaultFont();
        }

        #region Form Ribbon Commands Handlers

        public delegate void EditorOnNewCommand(object sender, EventArgs e);

        public delegate void EditorOnOpenCommand(object sender, EventArgs e);

        public delegate void EditorOnSaveCommand(object sender, EventArgs e);

        public delegate void EditorOnPrintCommand(object sender, EventArgs e);

        public delegate void EditorOnCutCommand(object sender, EventArgs e);

        public delegate void EditorOnCopyCommand(object sender, EventArgs e);

        public delegate void EditorOnPasteCommand(object sender, EventArgs e);

        public delegate void EditorOnUndoCommand(object sender, EventArgs e);

        public delegate void EditorOnRedoCommand(object sender, EventArgs e);

        public delegate void EditorOnBoldCommand(object sender, EventArgs e);

        public delegate void EditorOnItalicCommand(object sender, EventArgs e);

        public delegate void EditorOnUnderlineCommand(object sender, EventArgs e);

        public delegate void EditorOnStrikeCommand(object sender, EventArgs e);

        public delegate void EditorOnFontCommand(object sender, EventArgs e);

        public delegate void EditorOnFontIncreaseCommand(object sender, EventArgs e);

        public delegate void EditorOnFontDecreaseCommand(object sender, EventArgs e);

        public delegate void EditorOnLeftAlignCommand(object sender, EventArgs e);

        public delegate void EditorOnCenterAlignCommand(object sender, EventArgs e);

        public delegate void EditorOnRightAlignCommand(object sender, EventArgs e);

        public delegate void EditorOnListCommand(object sender, EventArgs e);

        public delegate void EditorOnOutdentCommand(object sender, EventArgs e);

        public delegate void EditorOnIndentCommand(object sender, EventArgs e);

        public delegate void EditorOnImageCommand(object sender, EventArgs e);

        public delegate void EditorOnApplyTemplateCommand(object sender, EventArgs e);

        public delegate void EditorOnAddTemplateCommand(object sender, EventArgs e);

        public delegate void EditorOnDeleteTemplateCommand(object sender, EventArgs e);

        public delegate void EditorOnChangeCategoryTemplateCommand(object sender, EventArgs e);

        public delegate void EditorOnApplyClipartCommand(object sender, EventArgs e);

        public delegate void EditorOnAddClipartCommand(object sender, EventArgs e);

        public delegate void EditorOnDeleteClipartCommand(object sender, EventArgs e);

        public delegate void EditorOnChangeCategoryClipartCommand(object sender, EventArgs e);

        public event EditorOnNewCommand OnNewCommand;
        public event EditorOnOpenCommand OnOpenCommand;
        public event EditorOnSaveCommand OnSaveCommand;
        public event EditorOnPrintCommand OnPrintCommand;
        public event EditorOnCutCommand OnCutCommand;
        public event EditorOnCopyCommand OnCopyCommand;
        public event EditorOnPasteCommand OnPasteCommand;
        public event EditorOnUndoCommand OnUndoCommand;
        public event EditorOnRedoCommand OnRedoCommand;
        public event EditorOnBoldCommand OnBoldCommand;
        public event EditorOnItalicCommand OnItalicCommand;
        public event EditorOnUnderlineCommand OnUnderlineCommand;
        public event EditorOnStrikeCommand OnStrikeCommand;
        public event EditorOnFontCommand OnFontCommand;
        public event EditorOnFontIncreaseCommand OnFontIncreaseCommand;
        public event EditorOnFontDecreaseCommand OnFontDecreaseCommand;
        public event EditorOnLeftAlignCommand OnLeftAlignCommand;
        public event EditorOnCenterAlignCommand OnCenterAlignCommand;
        public event EditorOnRightAlignCommand OnRightAlignCommand;
        public event EditorOnListCommand OnListCommand;
        public event EditorOnOutdentCommand OnOutdentCommand;
        public event EditorOnIndentCommand OnIndentCommand;
        public event EditorOnImageCommand OnImageCommand;

        public event EditorOnApplyTemplateCommand OnApplyTemplateCommand;
        public event EditorOnAddTemplateCommand OnAddTemplateCommand;
        public event EditorOnDeleteTemplateCommand OnDeleteTemplateCommand;
        public event EditorOnChangeCategoryTemplateCommand OnChangeCategoryTemplateCommand;

        public event EditorOnApplyClipartCommand OnApplyClipartCommand;
        public event EditorOnAddClipartCommand OnAddClipartCommand;
        public event EditorOnDeleteClipartCommand OnDeleteClipartCommand;
        public event EditorOnChangeCategoryClipartCommand OnChangeCategoryClipartCommand;

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            if (OnNewCommand != null) OnNewCommand(this, e);
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (OnOpenCommand != null) OnOpenCommand(this, e);
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (OnSaveCommand != null) OnSaveCommand(this, e);
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            if (OnPrintCommand != null) OnPrintCommand(this, e);
        }

        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            if (OnCutCommand != null) OnCutCommand(this, e);
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (OnCopyCommand != null) OnCopyCommand(this, e);
        }

        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            if (OnPasteCommand != null) OnPasteCommand(this, e);
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            if (OnUndoCommand != null) OnUndoCommand(this, e);
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            if (OnRedoCommand != null) OnRedoCommand(this, e);
        }

        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            if (OnBoldCommand != null) OnBoldCommand(this, e);
        }

        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            if (OnItalicCommand != null) OnItalicCommand(this, e);
        }

        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {
            if (OnUnderlineCommand != null) OnUnderlineCommand(this, e);
        }

        private void toolStripButtonStrike_Click(object sender, EventArgs e)
        {
            if (OnStrikeCommand != null) OnStrikeCommand(this, e);
        }

        private void toolStripButtonFont_Click(object sender, EventArgs e)
        {
            if (OnFontCommand != null) OnFontCommand(this, e);
        }

        private void toolStripButtonFontIncrease_Click(object sender, EventArgs e)
        {
            if (OnFontIncreaseCommand != null) OnFontIncreaseCommand(this, e);
        }

        private void toolStripButtonFontDecrease_Click(object sender, EventArgs e)
        {
            if (OnFontDecreaseCommand != null) OnFontDecreaseCommand(this, e);
        }

        private void toolStripButtonLeftText_Click(object sender, EventArgs e)
        {
            if (OnLeftAlignCommand != null) OnLeftAlignCommand(this, e);
        }

        private void toolStripButtonCenterText_Click(object sender, EventArgs e)
        {
            if (OnCenterAlignCommand != null) OnCenterAlignCommand(this, e);
        }

        private void toolStripButtonRightText_Click(object sender, EventArgs e)
        {
            if (OnRightAlignCommand != null) OnRightAlignCommand(this, e);
        }

        private void toolStripButtonList_Click(object sender, EventArgs e)
        {
            if (OnListCommand != null) OnListCommand(this, e);
        }

        private void toolStripButtonOutdent_Click(object sender, EventArgs e)
        {
            if (OnOutdentCommand != null) OnOutdentCommand(this, e);
        }

        private void toolStripButtonIndent_Click(object sender, EventArgs e)
        {
            if (OnIndentCommand != null) OnIndentCommand(this, e);
        }

        private void toolStripButtonImage_Click(object sender, EventArgs e)
        {
            if (OnImageCommand != null) OnImageCommand(this, e);
        }

        #endregion

        #region Form Commands Handlers

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (OnNewCommand != null) OnNewCommand(this, e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (OnOpenCommand != null) OnOpenCommand(this, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OnSaveCommand != null) OnSaveCommand(this, e);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (OnPrintCommand != null) OnPrintCommand(this, e);
        }

        private void btnApplyTemplate_Click(object sender, EventArgs e)
        {
            if (OnApplyTemplateCommand != null) OnApplyTemplateCommand(this, e);
        }

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            if (OnAddTemplateCommand != null) OnAddTemplateCommand(this, e);
        }

        private void lsvTemplates_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OnApplyTemplateCommand != null) OnApplyTemplateCommand(this, e);
        }

        private void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            if (OnDeleteTemplateCommand != null) OnDeleteTemplateCommand(this, e);
        }

        private void btnApplyClipart_Click(object sender, EventArgs e)
        {
            if (OnApplyClipartCommand != null) OnApplyClipartCommand(this, e);
        }

        private void btnAddClipart_Click(object sender, EventArgs e)
        {
            if (OnAddClipartCommand != null) OnAddClipartCommand(this, e);
        }

        private void lsvCliparts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OnApplyClipartCommand != null) OnApplyClipartCommand(this, e);
        }

        private void btnDeleteClipart_Click(object sender, EventArgs e)
        {
            if (OnDeleteClipartCommand != null) OnDeleteClipartCommand(this, e);
        }

        private void cboGalleryTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnChangeCategoryTemplateCommand != null) OnChangeCategoryTemplateCommand(this, e);
        }

        private void cboGalleryClipartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnChangeCategoryClipartCommand != null) OnChangeCategoryClipartCommand(this, e);
        }

        #endregion

        #region Form Fields Handlers

        private void nudBorderWidth_ValueChanged(object sender, EventArgs e)
        {
            var borderWidth = Convert.ToInt32(nudBorderWidth.Value);

            CampaignBorderWidth = borderWidth;
            pnlBorder.Padding = new Padding(CampaignBorderWidth);
        }

        private void cboBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBorderStyle();

            ApplyBorderSyle();
        }

        private void pnlBorder_PaddingChanged(object sender, EventArgs e)
        {
            ApplyBorderSyle();
        }

        private void pnlBorder_Paint(object sender, PaintEventArgs e)
        {
            ApplyBorderSyle();
        }

        private void cboPaperSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPaperSize();

            ApplyPaperSize();
        }

        private void txtShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete) || e.KeyCode.Equals(Keys.Back))
                return;

            var h = new HotKeyCls {Ctrl = e.Control, Alt = e.Alt, Shift = e.Shift, Key = e.KeyCode};
            txtShortcut.Text = h.ToString();
        }

        #endregion

        #region Helpers

        private void Init()
        {
            LoadPaperSize();

            LoadCutType();

            LoadBorderStyle();

            LoadVoucherType();

            LoadCategory();

            LoadGalleryType();
        }

        private void SetEditorDefaultFont()
        {
            rtbEditor.Font = new Font("Verdana", 12, FontStyle.Regular);
        }

        private void LoadPaperSize()
        {
            cboPaperSize.DisplayMember = "Key";
            cboPaperSize.ValueMember = "Value";

            Enum.GetNames(typeof(Enumerations.PaperSize)).ToList().ForEach(item => cboPaperSize.Items.Add(new KeyValuePair<string, string>(item, item)));

            cboPaperSize.SelectedIndex = 0;
        }

        private void LoadCutType()
        {
            cboCutType.DisplayMember = "Key";
            cboCutType.ValueMember = "Value";

            Enum.GetNames(typeof(Enumerations.CutType)).ToList().ForEach(item => cboCutType.Items.Add(new KeyValuePair<string, string>(item, item)));

            cboCutType.SelectedIndex = 0;
        }

        private void LoadBorderStyle()
        {
            cboBorderStyle.SelectedIndex = 0;
        }

        private void LoadVoucherType()
        {
            cboVoucherType.DisplayMember = "Key";
            cboVoucherType.ValueMember = "Value";

            Enum.GetNames(typeof(Enumerations.BarcodeType)).ToList().ForEach(item => cboVoucherType.Items.Add(new KeyValuePair<string, string>(item, item)));

            cboVoucherType.SelectedIndex = 0;
        }

        private void LoadCategory()
        {
            cboCategory.DisplayMember = "Key";
            cboCategory.ValueMember = "Value";

            Enum.GetNames(typeof(Enumerations.CampaignType)).ToList().ForEach(item => cboCategory.Items.Add(new KeyValuePair<string, string>(item, item)));

            cboCategory.SelectedIndex = 0;
        }

        private void LoadGalleryType()
        {
            cboGalleryClipartType.DisplayMember = "Key";
            cboGalleryClipartType.ValueMember = "Value";

            cboGalleryTemplateType.DisplayMember = "Key";
            cboGalleryTemplateType.ValueMember = "Value";

            Enum.GetNames(typeof(Enumerations.GalleryType)).ToList().ForEach(item =>
            {
                cboGalleryClipartType.Items.Add(new KeyValuePair<string, string>(item, item));
                cboGalleryTemplateType.Items.Add(new KeyValuePair<string, string>(item, item));
            });

            cboGalleryClipartType.SelectedIndex = 0;
            cboGalleryTemplateType.SelectedIndex = 0;
        }

        private void SetBorderStyle()
        {
            if (cboBorderStyle.SelectedItem != null)
            {
                CampaignBorderStyle = cboBorderStyle.SelectedItem.ToString();

                if (CampaignBorderStyle.Equals("White"))
                    Border = new SolidBrush(Color.White);
                else if (CampaignBorderStyle.Equals("Black"))
                    Border = new SolidBrush(Color.Black);
                else
                {
                    var hs = (HatchStyle) Enum.Parse(typeof (HatchStyle), cboBorderStyle.SelectedItem.ToString(), true);
                    Border = new HatchBrush(hs, Color.Black, Color.White);
                }
            }
            else
            {
                CampaignBorderStyle = "Black";
                Border = new SolidBrush(Color.White);
            }
        }

        private void SetPaperSize()
        {
            if (cboPaperSize.SelectedItem != null)
            {
                CampaignPaperSize = (Enumerations.PaperSize)
                    Enum.Parse(typeof(Enumerations.PaperSize),
                        ((KeyValuePair<string, string>)cboPaperSize.SelectedItem).Value);

                pnlBorder.Dock = DockStyle.Fill;

                switch (CampaignPaperSize)
                {
                    case Enumerations.PaperSize.Tamanho_1:
                        PaperSizeHeight = Convert.ToInt32(pnlBorder.Height * 0.25);
                        break;
                    case Enumerations.PaperSize.Tamanho_2:
                        PaperSizeHeight = Convert.ToInt32(pnlBorder.Height * 0.50);
                        break;
                    case Enumerations.PaperSize.Tamanho_3:
                        PaperSizeHeight = Convert.ToInt32(pnlBorder.Height * 0.75);
                        break;
                    case Enumerations.PaperSize.Tamanho_4:
                    default:
                        PaperSizeHeight = pnlBorder.Height;
                        break;
                }
            }
        }

        private void ApplyBorderSyle()
        {
            if (Border == null) return;

            var gr = pnlBorder.CreateGraphics();
            gr.FillRectangle(Border, pnlBorder.ClientRectangle);
        }

        private void ApplyPaperSize()
        {
            if (CampaignPaperSize != Enumerations.PaperSize.Tamanho_4)
            {
                pnlBorder.Dock = DockStyle.None;
                pnlBorder.Height = PaperSizeHeight;
            }
            else
            {
                pnlBorder.Dock = DockStyle.Fill;
            }
        }

        private void HandleDirtyScreen(bool before)
        {
            if (before)
                FormUtility.SetCursorPos(0, 0);
            else
                FormUtility.SetCursorPos(_point.X, _point.Y);
        }

        #endregion
    }
}
