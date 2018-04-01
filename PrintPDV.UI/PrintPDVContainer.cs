using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PrintPDV.Controllers;
using PrintPDV.Controllers.Interfaces;
using PrintPDV.LanguagePack;
using PrintPDV.UI.Components;
using PrintPDV.Utility;

namespace PrintPDV.UI
{
    public partial class PrintPDVContainer : Form
    {
        public PrintPDVContainer()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void PrintPDVContainer_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void tsbEditor_Click(object sender, EventArgs e)
        {
            var form = FormUtility.GetForm<PrintPDVEditor2>();

            OpenForm(form ?? new PrintPDVEditor2());
        }

        private void tsbCampaing_Click(object sender, EventArgs e)
        {
            var form = FormUtility.GetForm<PrintPDVCampaign>();

            OpenForm(form ?? new PrintPDVCampaign());
        }

        private void tsbGadget_Click(object sender, EventArgs e)
        {
            var gadgetForm = FormUtility.GetForm<PrintPDVGadget>();

            if (gadgetForm != null)
            {
                gadgetForm.BringToFront();
            }
            else
            {
                gadgetForm = new PrintPDVGadget { TopMost = true };
                gadgetForm.Show();
            }
        }

        private void tsbVoucher_Click(object sender, EventArgs e)
        {
            var form = FormUtility.GetForm<PrintPDVVoucher>();

            OpenForm(form ?? new PrintPDVVoucher());
        }

        private void tsbPrinter_Click(object sender, EventArgs e)
        {
            var wizard = new PrintPDVWizard();

            wizard.WizardPages.Add(1, new WizardPrinterSetup());

            wizard.WizardCompleted += SetStatusBar;

            wizard.LoadWizard();

            wizard.ShowDialog();
        }

        private void tsbStatistic_Click(object sender, EventArgs e)
        {
            var form = FormUtility.GetForm<PrintPDVStatistic>();

            OpenForm(form ?? new PrintPDVStatistic());
        }

        private void tsbLicense_Click(object sender, EventArgs e)
        {
            var wizard = new PrintPDVWizard();

            wizard.WizardPages.Add(1, new WizardLicenseSetup());

            wizard.WizardCompleted += () => { };

            wizard.LoadWizard();

            wizard.ShowDialog();
        }

        #endregion

        #region Helpers

        private void Init()
        {
            SetStatusBar();

            var form = FormUtility.GetForm<PrintPDVEditor2>();

            OpenForm(form ?? new PrintPDVEditor2());

            StartSyncTimer();
        }

        public void OpenForm(Form objForm)
        {
            if (objForm == null) return;

            SetButtonBackground(objForm);

            pnlContent.Controls.Clear();

            objForm.TopLevel = false;
            objForm.ControlBox = false;
            objForm.FormBorderStyle = FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(objForm);

            objForm.Show();

            objForm.Focus();
        }

        private void SetButtonBackground(Control objForm)
        {
            foreach (var control in tsMenu.Items.OfType<ToolStripButton>())
                control.BackColor = Color.Silver;

            ToolStripButton obj = null;

            switch (objForm.Name)
            {
                case "PrintPDVEditor2":
                    obj = tsbEditor;
                    break;
                case "PrintPDVCampaign":
                    obj = tsbCampaing;
                    break;
                case "PrintPDVVoucher":
                    obj = tsbVoucher;
                    break;
                case "PrintPDVWizard":
                    obj = tsbPrinter;
                    break;
                case "PrintPDVStatistic":
                    obj = tsbStatistic;
                    break;
                case "PrintPDVLicense":
                    obj = tsbLicense;
                    break;
            }

            if (obj != null)
                obj.BackColor = Color.DimGray;
        }

        public void SetStatusBar()
        {
            IPrinterHandler printerHandler = null;

            try
            {
                printerHandler = PrinterController.Instance.GetPrinter();
            }
            catch (Exception)
            {
                // ignored
            }

            if (printerHandler != null)
            {
                statusBarLabel.Text = string.Format("{0} - {1} - {2}", 
                    printerHandler.PrinterConfig.Manufacturer,
                    printerHandler.PrinterConfig.Name,
                    printerHandler.PrinterConfig.ConnectionType);

                statusBarLabel.ForeColor = Color.Black;
            }
            else
            {
                statusBarLabel.Text = AppStrings.ErrorMessage_Printer_Rule_PrinterNotDefined;
                statusBarLabel.ForeColor = Color.Red;
            }
        }

        private void StartSyncTimer()
        {
            var timer = new Timer();

            timer.Interval = 30000; // 1min
            timer.Tick += SyncAction;
            timer.Start();
        }

        void SyncAction(object sender, EventArgs e)
        {
            try
            {
                StatisticController.Instance.Sync();
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(ex.Message, AppStrings.ErrorMessage_Statistic_Rule_SyncError);

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
