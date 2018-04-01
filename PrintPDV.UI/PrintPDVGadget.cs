using PrintPDV.Controllers;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PrintPDV.LanguagePack;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.UI
{
    public partial class PrintPDVGadget : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern bool ReleaseCapture();

        public PrintPDVGadget()
        {
            InitializeComponent();
        }

        private void btnDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ibtPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var activeCampaigns = CampaignController.Instance.GetByActive();

                if (activeCampaigns != null && activeCampaigns.Any())
                    activeCampaigns.ForEach(item => CampaignController.Instance.PrintCampaign(item, Enumerations.TriggerType.Gadget));
                else
                    MessageBox.Show(AppStrings.InfoMessage_Campaign_Rule_NoActiveCampaign, AppStrings.Generic_MessageBox_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);            
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro ao imprimir a campanha pelo gadget."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
