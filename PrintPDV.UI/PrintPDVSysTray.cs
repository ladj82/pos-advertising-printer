using PrintPDV.Controllers;
using PrintPDV.UI.Properties;
using PrintPDV.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using PrintPDV.LanguagePack;
using PrintPDV.UI.Components;

namespace PrintPDV.UI
{
    public class PrintPDVSysTray : Form
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

                Application.Run(new PrintPDVSysTray());
            }
            catch (Exception ex)
            {
                var errorMessage = "Erro generalizado! A aplicação será encerrada. Consulte o arquivo de log para mais detalhes."; //TODO: put on resource file

                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private readonly Dictionary<int, IWizardStep> WizardSteps = new Dictionary<int, IWizardStep>();
        private NotifyIcon  _trayIcon;
        private ContextMenu _trayMenu;

        public PrintPDVSysTray()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // PrintPdvSysTray
            // 
            ClientSize = new Size(284, 262);
            Name = "PrintPDVSysTray";
            Load += PrintPDVTray_Load;
            ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing && _trayIcon != null)
                _trayIcon.Dispose();

            base.Dispose(isDisposing);
        }

        private void PrintPDVTray_Load(object sender, EventArgs e)
        {
            Init();
        }
        
        private void Init()
        {
            try
            {
                Check();

                CreateAppFolders();

                SetupShortcuts();

                SetupTrayIcon();

                OnOpen(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnExit(null, null);
            }
        }

        private void Check()
        {
            AddDatabaseStep();

            AddLicenseStep();

            AddPrinterStep();

            if (!WizardSteps.Any()) return;

            var wizard = new PrintPDVWizard();

            WizardSteps.ToList().ForEach(item => wizard.WizardPages.Add(item.Key, item.Value));

            wizard.WizardCompleted += WizardCompleted;
                
            wizard.LoadWizard();
                
            wizard.ShowDialog();
        }

        private void AddDatabaseStep()
        {
            if (!AppConfigController.IsDatabaseCreated)
                WizardSteps.Add(WizardSteps.Count + 1, new WizardDatabaseSetup());
            else
                AppConfigController.Instance.SetAppConfig();
        }

        private void AddLicenseStep()
        {
            try
            {
                if (!AppConfigController.IsDatabaseCreated)
                {
                    WizardSteps.Add(WizardSteps.Count + 1, new WizardLicenseSetup());
                    return;
                }

                var license = AppLicenseController.Instance.GetActiveLicense();

                if (license == null)
                {
                    WizardSteps.Add(WizardSteps.Count + 1, new WizardLicenseSetup());
                }
                else
                {
                    AppLicenseController.Instance.ValidateLicense(license.Key);
                }
            }
            catch (Exception)
            {
                WizardSteps.Add(WizardSteps.Count + 1, new WizardLicenseSetup());
            }
        }

        private void AddPrinterStep()
        {
            try
            {
                if (!AppConfigController.IsDatabaseCreated)
                {
                    WizardSteps.Add(WizardSteps.Count + 1, new WizardPrinterSetup());
                    return;
                }

                PrinterController.Instance.GetPrinter();
            }
            catch (Exception)
            {
                WizardSteps.Add(WizardSteps.Count + 1, new WizardPrinterSetup());
            }
        }

        private static void WizardCompleted()
        {
            MessageBox.Show(@"Configuração realizada com sucesso!", AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information); //TODO: put on resource file
        }

        private void CreateAppFolders()
        {
            if (!Directory.Exists(AppConfigUtility.CampaignPath))
                Directory.CreateDirectory(AppConfigUtility.CampaignPath);
        }

        private void SetupTrayIcon()
        {
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add(AppStrings.Generic_Menu_SysTray_Open, OnOpen);
            _trayMenu.MenuItems.Add(AppStrings.Generic_Menu_SysTray_Gadget, OnGadget);
            _trayMenu.MenuItems.Add(AppStrings.Generic_Menu_SysTray_Exit, OnExit);

            _trayIcon = new NotifyIcon
            {
                Text = @"PrintPDV",
                Icon = new Icon(Resources.PrintPDV, 40, 40),
                ContextMenu = _trayMenu,
                Visible = true
            };
        }

        private void SetupShortcuts()
        {
            CampaignController.Instance.SetupShortcuts();
        }

        private void OnOpen(object sender, EventArgs e)
        {
            var mdiForm = FormUtility.GetForm<PrintPDVContainer>();

            if (mdiForm != null)
            {
                mdiForm.BringToFront();
            }
            else
            {
                mdiForm = new PrintPDVContainer();
                mdiForm.Show();
            }
        }

        private void OnGadget(object sender, EventArgs e)
        {
            var gadgetForm = FormUtility.GetForm<PrintPDVGadget>();

            if (gadgetForm != null)
            {
                gadgetForm.BringToFront();
            }
            else
            {
                gadgetForm = new PrintPDVGadget {TopMost = true};
                gadgetForm.Show();
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            ShortcutHandler.Instance.UnsetHotKeys();
            Application.Exit();
        }
    }
}
