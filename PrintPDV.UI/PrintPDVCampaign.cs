using PrintPDV.Controllers;
using PrintPDV.Models;
using PrintPDV.Utility;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PrintPDV.LanguagePack;

namespace PrintPDV.UI
{
    public partial class PrintPDVCampaign : Form
    {
        public PrintPDVCampaign()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void PrintPDVConfiguration_Load(object sender, EventArgs e)
        {
            LoadCampaingList();
        }

        private void PrintPDVCampaign_Enter(object sender, EventArgs e)
        {
            LoadCampaingList();
        }

        private void dgvCampaign_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvCampaign.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvCampaign.SelectedRows[0];

                    if (selectedRow != null && selectedRow.Selected)
                    {
                        var campaign = (Campaign)selectedRow.Cells["dgvItem"].Value;
                        ShowDetails(campaign);
                        ShowImagePreview(campaign);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro ao selecionar a campanha."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCampaign_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    var column = dgvCampaign.Columns[e.ColumnIndex].Name;

                    if (column.Equals("dgvStatus"))
                    {
                        var selectedRow = dgvCampaign.SelectedRows[0];

                        if (selectedRow.Selected)
                        {
                            var campaign = (Campaign)selectedRow.Cells["dgvItem"].Value;
                            campaign.Active = (bool)selectedRow.Cells["dgvStatus"].Value;
                            campaign.Modified = DateTime.Now;

                            SaveCampaign(campaign);

                            ShowDetails(campaign);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro ao ativar a campanha."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCampaign_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    var selectedRow = dgvCampaign.SelectedRows[0];

                    if (selectedRow.Selected)
                    {
                        var campaign = (Campaign)selectedRow.Cells["dgvItem"].Value;

                        var column = dgvCampaign.Columns[e.ColumnIndex].Name;

                        if (column.Equals("dgvEdit"))
                        {
                            var editor = FormUtility.GetForm<PrintPDVEditor2>();

                            editor = editor ?? new PrintPDVEditor2();

                            editor.SetCampaign(campaign);

                            var container = FormUtility.GetForm<PrintPDVContainer>();

                            if (container != null)
                                container.OpenForm(editor);
                        }
                        else if (column.Equals("dgvDelete"))
                        {
                            DeleteCampaign(campaign);
                            ShowImagePreview(null);
                            ShowDetails(null);
                            LoadCampaingList();
                        }
                    }
                }

                dgvCampaign.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro ao editar ou excluir a campanha."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helpers

        private void LoadCampaingList()
        {
            try
            {
                dgvCampaign.Rows.Clear();

                CampaignController.Instance.GetList()
                    .ToList()
                    .ForEach(
                        item =>
                            dgvCampaign.Rows.Add(item.Active, item.Name, item.Shortcut, AppStrings.Generic_Button_Edit,
                                AppStrings.Generic_Button_Delete, item));
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Read, AppStrings.Generic_ClassName_Campaign);

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveCampaign(Campaign campaign)
        {
            try
            {
                CampaignController.Instance.Save(campaign);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Save, AppStrings.Generic_ClassName_Campaign);
                
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteCampaign(Campaign campaign)
        {
            try
            {
                var result = MessageBox.Show(AppStrings.Generic_MessageBox_ConfirmYesOrNo, AppStrings.Generic_MessageBox_ConfirmYesOrNoTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                CampaignController.Instance.Delete(campaign);

                var imagePath = CampaignController.Instance.GetCampaignImagePath(campaign);

                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Delete, AppStrings.Generic_ClassName_Campaign);

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetails(Campaign campaign)
        {
            try
            {
                if (campaign != null)
                {
                    lblDetailName.Text = campaign.Name;
                    lblDetailStatus.Text = campaign.Active ? AppStrings.Generic_Label_Enabled : AppStrings.Generic_Label_Disabled;
                    lblDetailShortcut.Text = campaign.Shortcut;
                    lblDetailCreated.Text = campaign.Created.ToLongDateString();
                    lblDetailModified.Text = campaign.Modified.HasValue ? campaign.Modified.Value.ToLongDateString() : string.Empty;
                }
                else
                {
                    lblDetailName.Text = string.Empty;
                    lblDetailStatus.Text = string.Empty;
                    lblDetailShortcut.Text = string.Empty;
                    lblDetailCreated.Text = string.Empty;
                    lblDetailModified.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro ao mostrar os detalhes da campanha."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowImagePreview(Campaign campaign)
        {
            try
            {
                if (campaign != null)
                {
                    var imagePath = CampaignController.Instance.GetCampaignImagePath(campaign);

                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        picTemplatePreview.Image = Image.FromStream(new MemoryStream(File.ReadAllBytes(imagePath)));
                    }
                }
                else
                {
                    picTemplatePreview.Image = null;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro ao renderizar a imagem."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}