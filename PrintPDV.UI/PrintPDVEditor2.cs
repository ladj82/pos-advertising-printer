using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PrintPDV.Controllers;
using PrintPDV.LanguagePack;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.UI
{
    public partial class PrintPDVEditor2 : Form
    {
        private Campaign _openedCampaign;
        private ImageList _galleryCliparts;
        private ImageList _galleryTemplates;

        public PrintPDVEditor2()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void PrintPDVEditor2_Load(object sender, EventArgs e)
        {
            LoadGalleryCliparts();
            LoadGalleryTemplates();
        }

        private void editor_OnNewCommand(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(AppStrings.Generic_MessageBox_ConfirmYesOrNo, AppStrings.Generic_MessageBox_ConfirmYesOrNoTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                _openedCampaign = null;

                editor.CampaignName = string.Empty;
                editor.CampaignType = Enumerations.CampaignType.Desconto;
                editor.CampaignShortcut = string.Empty;
                editor.CampaignSource = string.Empty;
                editor.CampaignBorderStyle = "Black";
                editor.CampaignBorderWidth = 20;
                editor.CampaignVoucherBarcodeType = Enumerations.BarcodeType.Nenhum;
                editor.CampaignPrintDateTime = true;
                editor.CampaignPaperSize = Enumerations.PaperSize.Tamanho_4;
                editor.CampaignCutType = Enumerations.CutType.Nenhum;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnNewCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnOpenCommand(object sender, EventArgs e)
        {
            try
            {
                var mdi = FormUtility.GetForm<PrintPDVContainer>();

                if (mdi != null)
                    mdi.OpenForm(new PrintPDVCampaign());
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnOpenCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnSaveCommand(object sender, EventArgs e)
        {
            try
            {
                var campaign = new Campaign();

                if (_openedCampaign != null)
                {
                    campaign.Id = _openedCampaign.Id;
                    campaign.Created = _openedCampaign.Created;
                    campaign.Modified = DateTime.Now;
                    campaign.Active = _openedCampaign.Active;
                }
                else
                {
                    campaign.Created = DateTime.Now;
                    campaign.Active = true;
                }

                campaign.Name = editor.CampaignName;
                campaign.CampaignType = editor.CampaignType;
                campaign.Shortcut = editor.CampaignShortcut;
                campaign.Source = editor.CampaignSource;
                campaign.BorderStyle = editor.CampaignBorderStyle;
                campaign.BorderWidth = editor.CampaignBorderWidth;
                campaign.PrintVoucher = editor.CampaignVoucherBarcodeType != Enumerations.BarcodeType.Nenhum;
                campaign.VoucherBarcodeType = editor.CampaignVoucherBarcodeType;
                campaign.PrintDateTime = editor.CampaignPrintDateTime;
                campaign.PaperSize = editor.CampaignPaperSize;
                campaign.CutType = editor.CampaignCutType;

                CampaignController.Instance.Save(campaign);

                _openedCampaign = campaign;

                var imgEditor = editor.GetEditorImage;
                var imagePath = CampaignController.Instance.GetCampaignImagePath(campaign);

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

                imgEditor.Save(imagePath, ImageFormat.Bmp);

                CampaignController.Instance.SetupShortcuts();

                MessageBox.Show(AppStrings.Generic_SuccessMessage_Save, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnSaveCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnPrintCommand(object sender, EventArgs e)
        {
            try
            {
                var testCampaign = new Campaign
                {
                    Name = Guid.NewGuid().ToString(),
                    PrintDateTime = editor.CampaignPrintDateTime,
                    CutType = editor.CampaignCutType,
                    CampaignType = Enumerations.CampaignType.Outros
                };

                var tempImagePath = CampaignController.Instance.GetCampaignImagePath(testCampaign);

                editor.GetEditorImage.Save(tempImagePath, ImageFormat.Bmp);

                CampaignController.Instance.PrintCampaign(testCampaign, Enumerations.TriggerType.Teste);

                File.Delete(tempImagePath);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnPrintCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnCutCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.Cut();
        }

        private void editor_OnCopyCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.Copy();
        }

        private void editor_OnPasteCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.Paste();
        }

        private void editor_OnUndoCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.Undo();
        }

        private void editor_OnRedoCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.Redo();
        }

        private void editor_OnBoldCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionFont == null) return;

            var currentFont = editor.rtbEditor.SelectionFont;
            var newFontStyle = editor.rtbEditor.SelectionFont.Style ^ FontStyle.Bold;

            editor.rtbEditor.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void editor_OnItalicCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionFont == null) return;

            var currentFont = editor.rtbEditor.SelectionFont;
            var newFontStyle = editor.rtbEditor.SelectionFont.Style ^ FontStyle.Italic;

            editor.rtbEditor.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void editor_OnUnderlineCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionFont == null) return;

            var currentFont = editor.rtbEditor.SelectionFont;
            var newFontStyle = editor.rtbEditor.SelectionFont.Style ^ FontStyle.Underline;

            editor.rtbEditor.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void editor_OnStrikeCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionFont == null) return;

            var currentFont = editor.rtbEditor.SelectionFont;
            var newFontStyle = editor.rtbEditor.SelectionFont.Style ^ FontStyle.Strikeout;

            editor.rtbEditor.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }

        private void editor_OnFontCommand(object sender, EventArgs e)
        {
            var fontDlg = new FontDialog { Font = editor.rtbEditor.SelectionFont, ShowColor = true };

            if (fontDlg.ShowDialog() != DialogResult.OK || editor.rtbEditor.SelectionFont == null) return;

            editor.rtbEditor.SelectionFont = fontDlg.Font;
            editor.rtbEditor.SelectionColor = fontDlg.Color;
        }

        private void editor_OnFontIncreaseCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionFont == null) return;

            var fontSize = (int)editor.rtbEditor.SelectionFont.Size;
            fontSize++;

            editor.rtbEditor.SelectionFont = new Font(editor.rtbEditor.SelectionFont.FontFamily, fontSize, editor.rtbEditor.SelectionFont.Style);
        }

        private void editor_OnFontDecreaseCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionFont == null) return;

            var fontSize = (int)editor.rtbEditor.SelectionFont.Size;
            fontSize--;

            editor.rtbEditor.SelectionFont = new Font(editor.rtbEditor.SelectionFont.FontFamily, fontSize, editor.rtbEditor.SelectionFont.Style);
        }

        private void editor_OnLeftAlignCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void editor_OnCenterAlignCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void editor_OnRightAlignCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void editor_OnListCommand(object sender, EventArgs e)
        {
            editor.rtbEditor.SelectionBullet = !editor.rtbEditor.SelectionBullet;
        }

        private void editor_OnIndentCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionAlignment == HorizontalAlignment.Left)
                editor.rtbEditor.SelectionIndent += 36;
        }

        private void editor_OnOutdentCommand(object sender, EventArgs e)
        {
            if (editor.rtbEditor.SelectionAlignment == HorizontalAlignment.Left && editor.rtbEditor.SelectionIndent >= 36)
                editor.rtbEditor.SelectionIndent -= 36;
        }

        private void editor_OnImageCommand(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = string.Format("{0} |*.jpg; *.jpeg; *.gif; *.png; *.bmp", AppStrings.Generic_Label_Images)
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    try
                    {
                        var image = Image.FromStream(new MemoryStream(File.ReadAllBytes(ofd.FileName)));

                        image = ImageUtility.ConvertToBlackWhite(image);

                        var width = editor.rtbEditor.Width - 4;
                        if (image.Width > width)
                            image = ImageUtility.ResizeImageFixedWidth(image, width);

                        image = ImageUtility.ByteToImage(ImageUtility.ImageToByte(image, ImageFormat.Bmp));

                        var currentClipboard = Clipboard.GetDataObject();

                        Clipboard.SetImage(image);

                        editor.rtbEditor.Paste();

                        if (currentClipboard != null) Clipboard.SetDataObject(currentClipboard);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Log(LogUtility.LogType.Error, "editor_OnImageCommand", ex.Message);
                        MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void editor_OnChangeCategoryTemplateCommand(object sender, EventArgs e)
        {
            LoadGalleryTemplates();
        }

        private void editor_OnApplyTemplateCommand(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = editor.lsvTemplates.SelectedItems;

                if (selectedItems.Count > 0)
                {
                    var templateId = Convert.ToInt32(selectedItems[0].Name);

                    var template = GalleryTemplateController.Instance.Get(templateId);

                    if (template != null && template.Source != null)
                    {
                        //editor.CampaignName = template.Name;
                        editor.CampaignType = template.CampaignType;
                        editor.CampaignShortcut = template.Shortcut;
                        editor.CampaignSource = template.Source;
                        editor.CampaignBorderStyle = template.BorderStyle;
                        editor.CampaignBorderWidth = template.BorderWidth;
                        editor.CampaignVoucherBarcodeType = template.VoucherBarcodeType;
                        editor.CampaignPrintDateTime = template.PrintDateTime;
                        editor.CampaignPaperSize = template.PaperSize;
                        editor.CampaignCutType = template.CutType;
                    }
                }
                else
                {
                    MessageBox.Show(AppStrings.ErrorMessage_GalleryTemplate_Required_NotSelected, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnApplyTemplateCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnAddTemplateCommand(object sender, EventArgs e)
        {
            try
            {
                var galleryTemplate = new GalleryTemplate
                {
                    Name = editor.CampaignName,
                    CampaignType = editor.CampaignType,
                    GalleryType = editor.GalleryTemplateType,
                    Shortcut = editor.CampaignShortcut,
                    Source = editor.CampaignSource,
                    Thumbnail = ImageUtility.ImageToByte(ImageUtility.ResizeImageFixedWidth(editor.GetEditorImage, 84), ImageFormat.Bmp),
                    BorderStyle = editor.CampaignBorderStyle,
                    BorderWidth = editor.CampaignBorderWidth,
                    PrintVoucher = editor.CampaignVoucherBarcodeType != Enumerations.BarcodeType.Nenhum,
                    VoucherBarcodeType = editor.CampaignVoucherBarcodeType,
                    PrintDateTime = editor.CampaignPrintDateTime,
                    PaperSize = editor.CampaignPaperSize,
                    CutType = editor.CampaignCutType,
                    Created = DateTime.Now,
                    Active = true
                };

                GalleryTemplateController.Instance.Save(galleryTemplate);

                LoadGalleryTemplates();

                MessageBox.Show(AppStrings.Generic_SuccessMessage_Save, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnAddTemplateCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnDeleteTemplateCommand(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = editor.lsvTemplates.SelectedItems;

                if (selectedItems.Count > 0)
                {
                    var selectedImageId = Convert.ToInt32(selectedItems[0].Name);

                    var template = GalleryTemplateController.Instance.Get(selectedImageId);

                    if (template != null)
                    {
                        GalleryTemplateController.Instance.Delete(template);
                        LoadGalleryTemplates();
                        MessageBox.Show(AppStrings.Generic_SuccessMessage_Delete, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnDeleteTemplateCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnChangeCategoryClipartCommand(object sender, EventArgs e)
        {
            LoadGalleryCliparts();
        }

        private void editor_OnApplyClipartCommand(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = editor.lsvCliparts.SelectedItems;

                if (selectedItems.Count > 0)
                {
                    var selectedImageId = Convert.ToInt32(selectedItems[0].Name);

                    var clipart = GalleryClipartController.Instance.Get(selectedImageId);

                    if (clipart != null && clipart.Image != null)
                    {
                        var image = ImageUtility.ByteToImage(clipart.Image);

                        if (image != null)
                        {
                            var currentClipboard = Clipboard.GetDataObject();

                            Clipboard.SetImage(image);

                            editor.rtbEditor.Paste();

                            if (currentClipboard != null) Clipboard.SetDataObject(currentClipboard);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(AppStrings.ErrorMessage_GalleryClipart_Required_NotSelected, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnApplyClipartCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editor_OnAddClipartCommand(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = string.Format("{0} |*.jpg; *.jpeg; *.gif; *.png; *.bmp", AppStrings.Generic_Label_Images)
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    try
                    {
                        var image = Image.FromStream(new MemoryStream(File.ReadAllBytes(ofd.FileName)));

                        image = ImageUtility.ConvertToBlackWhite(image);

                        var width = editor.rtbEditor.Width - 4;

                        if (image.Width > width)
                            image = ImageUtility.ResizeImageFixedWidth(image, width);

                        GalleryClipartController.Instance.Save(new GalleryClipart
                        {
                            Name = ofd.SafeFileName,
                            GalleryType = editor.GalleryClipartType,
                            Image = ImageUtility.ImageToByte(image, ImageFormat.Bmp)
                        });

                        LoadGalleryCliparts();

                        MessageBox.Show(AppStrings.Generic_SuccessMessage_Save, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Log(LogUtility.LogType.Error, "editor_OnAddClipartCommand", ex.Message);
                        MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void editor_OnDeleteClipartCommand(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = editor.lsvCliparts.SelectedItems;

                if (selectedItems.Count > 0)
                {
                    var selectedImageId = Convert.ToInt32(selectedItems[0].Name);

                    var clipart = GalleryClipartController.Instance.Get(selectedImageId);

                    if (clipart != null)
                    {
                        GalleryClipartController.Instance.Delete(clipart);
                        LoadGalleryCliparts();
                        MessageBox.Show(AppStrings.Generic_SuccessMessage_Delete, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.Error, "editor_OnDeleteClipartCommand", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helpers

        public void SetCampaign(Campaign campaign)
        {
            _openedCampaign = campaign;

            editor.CampaignId = _openedCampaign.Id;
            editor.CampaignName = _openedCampaign.Name;
            editor.CampaignType = _openedCampaign.CampaignType;
            editor.CampaignShortcut = _openedCampaign.Shortcut;
            editor.CampaignSource = _openedCampaign.Source;
            editor.CampaignBorderStyle = _openedCampaign.BorderStyle;
            editor.CampaignBorderWidth = _openedCampaign.BorderWidth;
            editor.CampaignVoucherBarcodeType = _openedCampaign.VoucherBarcodeType;
            editor.CampaignPrintDateTime = _openedCampaign.PrintDateTime;
            editor.CampaignPaperSize = _openedCampaign.PaperSize;
            editor.CampaignCutType = _openedCampaign.CutType;
        }

        private void LoadGalleryCliparts()
        {
            try
            {
                editor.lsvCliparts.Items.Clear();
                editor.lsvCliparts.View = View.LargeIcon;

                _galleryCliparts = new ImageList { ImageSize = new Size(60, 60) };

                editor.lsvCliparts.LargeImageList = _galleryCliparts;

                var count = 0;
                GalleryClipartController.Instance.GetByGalleryType(editor.GalleryClipartType).ToList().ForEach(item =>
                {
                    var image = ImageUtility.ByteToImage(item.Image);

                    _galleryCliparts.Images.Add(item.Name, image);

                    editor.lsvCliparts.Items.Add(item.Id.ToString(), item.Name, count);

                    count++;
                });
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Read, AppStrings.Generic_ClassName_GalleryClipart);
                LogUtility.Log(LogUtility.LogType.Error, errorMessage, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGalleryTemplates()
        {
            try
            {
                editor.lsvTemplates.Items.Clear();
                editor.lsvTemplates.View = View.LargeIcon;

                _galleryTemplates = new ImageList { ImageSize = new Size(84, 160) };

                editor.lsvTemplates.LargeImageList = _galleryTemplates;

                var count = 0;
                GalleryTemplateController.Instance.GetByGalleryType(editor.GalleryTemplateType).ToList().ForEach(item =>
                {
                    var image = ImageUtility.ByteToImage(item.Thumbnail);

                    _galleryTemplates.Images.Add(item.Name, image);

                    editor.lsvTemplates.Items.Add(item.Id.ToString(), item.Name, count);

                    count++;
                });
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Read, AppStrings.Generic_ClassName_GalleryClipart);
                LogUtility.Log(LogUtility.LogType.Error, errorMessage, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
