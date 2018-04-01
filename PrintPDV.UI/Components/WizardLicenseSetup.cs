using System;
using System.Diagnostics;
using System.Windows.Forms;
using PrintPDV.Controllers;
using PrintPDV.Utility;

namespace PrintPDV.UI.Components
{
    public partial class WizardLicenseSetup : UserControl, IWizardStep
    {
        public UserControl Content
        {
            get
            {
                Dock = DockStyle.Fill;
                return this;
            }
        }
        
        public new void Load()
        {
            ValidateLicense();
        }

        public void Save()
        {
            
        }

        public void Cancel()
        {
            
        }

        public bool IsBusy { get; private set; }
        
        public bool PageValid { get; private set; }

        public string ValidationMessage
        {
            get { return "A configuração da licença deve ser realizada."; } //TODO: put on resource file
        }

        public WizardLicenseSetup()
        {
            InitializeComponent();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                IsBusy = true;

                var newLicense = txtLicense.Text;

                var license = AppLicenseController.Instance.ValidateLicense(newLicense);

                ShowLicenseDetails(license.Key, "Ok!", license.Activation.ToShortDateString()); //TODO: put on resource file

                PageValid = true;
                IsBusy = false;
            }
            catch (LicenseNotValidException ex)
            {
                IsBusy = false;
                ShowLicenseDetails(string.Empty, ex.Details, string.Empty);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            Process.Start(AppConfigUtility.WebsiteUrl);
        }

        private void ValidateLicense()
        {
            try
            {
                IsBusy = true;

                var license = AppLicenseController.Instance.GetActiveLicense();

                if (license != null)
                {
                    license = AppLicenseController.Instance.ValidateLicense(license.Key);

                    ShowLicenseDetails(license.Key, "Ok!", license.Activation.ToShortDateString()); //TODO: put on resource file
                    
                    PageValid = true;
                    IsBusy = false;
                }
                else
                {
                    ShowLicenseDetails(string.Empty, "Não informada", string.Empty);
                }
            }
            catch (LicenseNotValidException ex)
            {
                IsBusy = false;
                ShowLicenseDetails(string.Empty, ex.Details, string.Empty);
            }
        }

        private void ShowLicenseDetails(string key, string status, string activationDate)
        {
            lblKey.Text = key;
            lblStatus.Text = status;
            lblActivationDate.Text = activationDate;
        }
    }
}
