using PrintPDV.Controllers;
using PrintPDV.Models;
using PrintPDV.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PrintPDV.LanguagePack;

namespace PrintPDV.UI
{
    public partial class PrintPDVVoucher : Form
    {
        private List<Campaign> campaignListHandler;
        private List<Voucher> voucherListHandler;

        public PrintPDVVoucher()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void PrintPdvVoucher_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void dgvCampaign_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvCampaigns.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvCampaigns.SelectedRows[0];

                    if (selectedRow.Selected)
                    {
                        var campaign = (Campaign)selectedRow.Cells["dgvCampaignItem"].Value;

                        ShowDetails();

                        LoadVouchers(campaign.Id);

                        PopulateVoucherGrid();

                        ShowImagePreview(campaign);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Read, AppStrings.Generic_ClassName_Voucher);
                LogUtility.Log(LogUtility.LogType.Error, errorMessage, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                var voucherCode = txtVoucherCode.Text;
                var voucher = VoucherController.Instance.GetByCode(voucherCode);

                if (!VoucherController.Instance.IsValid(voucher)) return;

                VoucherController.Instance.SetAsUsed(voucher);

                var campaign = CampaignController.Instance.Get(voucher.Campaign_Id);

                campaignListHandler = new List<Campaign> { campaign };
                voucherListHandler = new List<Voucher> { voucher };

                PopulateCampaignGrid();

                PopulateVoucherGrid();

                MessageBox.Show(AppStrings.Generic_SuccessMessage_Save, AppStrings.Generic_MessageBox_InformationTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (VoucherNotFoundException ex)
            {
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (VoucherAlreadyUsedException ex)
            {
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Save, AppStrings.Generic_ClassName_Voucher);
                LogUtility.Log(LogUtility.LogType.Error, errorMessage, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Init();
        }

        #endregion

        #region Helpers

        private void Init()
        {
            txtVoucherCode.Text = string.Empty;
            txtVoucherCode.Focus();

            ShowDetails();

            LoadCampaigns();

            PopulateCampaignGrid();
        }

        private void LoadCampaigns()
        {
            campaignListHandler = CampaignController.Instance.GetByPrintVoucher();
        }

        private void LoadVouchers(int campaignId)
        {
            voucherListHandler = VoucherController.Instance.GetByCampaignId(campaignId);
        }

        private void PopulateCampaignGrid()
        {
            dgvCampaigns.Rows.Clear();

            campaignListHandler
                .ForEach(
                    item => dgvCampaigns.Rows.Add(item.Active, item.Name, item));
        }

        private void PopulateVoucherGrid()
        {
            dgvVoucherItems.Rows.Clear();

            voucherListHandler
                .ForEach(
                    item => dgvVoucherItems.Rows.Add(item.Code, item.Created, item.Used));
        }

        private void ShowDetails()
        {
            if (voucherListHandler != null && voucherListHandler.Any())
            {
                lblDetailPrintedNum.Text = voucherListHandler.Count.ToString(CultureInfo.InvariantCulture);
                lblDetailTrackedNum.Text = voucherListHandler.Count(x => x.Used != null).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                lblDetailPrintedNum.Text = string.Empty;
                lblDetailTrackedNum.Text = string.Empty;
            }
        }

        private void ShowImagePreview(Campaign campaign)
        {
            var imagePath = CampaignController.Instance.GetCampaignImagePath(campaign);

            if (campaign != null && File.Exists(imagePath))
                picTemplatePreview.Image = Image.FromStream(new MemoryStream(File.ReadAllBytes(imagePath)));
            else
                picTemplatePreview.Image = null;
        }

        #endregion
    }
}
