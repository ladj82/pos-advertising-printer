using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PrintPDV.Controllers;
using PrintPDV.Controllers.Interfaces;
using PrintPDV.LanguagePack;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.UI.Components
{
    public partial class WizardPrinterSetup : UserControl, IWizardStep
    {
        private IPrinterHandler PrinterHandler;
        private List<PrinterModel> CompatiblePrintersList;

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
            IsBusy = true;

            try
            {
                PrinterHandler = PrinterController.Instance.GetPrinter();
            }
            catch (Exception)
            {
                // ignored
            }

            CompatiblePrintersList = PrinterModelController.Instance.GetList().ToList();

            LoadPrinters();

            LoadCompatiblePrinters();

            gbxPrinterConfig.Visible = true;

            IsBusy = false;
        }

        public void Save()
        {
            
        }

        public void Cancel()
        {
            
        }

        public bool IsBusy { get; private set; }
        
        public bool PageValid 
        {
            get { return true; }
        }

        public string ValidationMessage
        {
            get { return "A impressora deve ser configurada."; } //TODO: put on resource file
        }

        public WizardPrinterSetup()
        {
            InitializeComponent();
        }

        private void LoadPrinters()
        {
            cboPrinters.DisplayMember = "Key";
            cboPrinters.ValueMember = "Value";
            cboPrinters.Items.Clear();

            LoadSpoolConnPrinters();
            LoadSerialConnPrinters();

            if (cboPrinters.Items.Count.Equals(0))
            {
                lblPrinterNotFound.Text = AppStrings.InfoMessage_Printer_Rule_PrinterNotFound;
                lblPrinterNotFound.Visible = true;
                gbxCompatiblePrinters.Visible = true;
            }
        }

        private void LoadCompatiblePrinters()
        {
            lbxCompatiblePrinters.DisplayMember = "Key";
            lbxCompatiblePrinters.ValueMember = "Value";
            lbxCompatiblePrinters.Items.Clear();

            CompatiblePrintersList.ForEach(item =>
            {
                var itemName = string.Format("{0} - {1}", item.Manufacturer, item.Name);
                lbxCompatiblePrinters.Items.Add(new KeyValuePair<string, string>(itemName, itemName));
            });
        }

        private void LoadSpoolConnPrinters()
        {
            var devices = PrintUtility.GetSpoolDevicesInfo();

            devices.ForEach(item =>
            {
                var printerModel = SearchWithInString(item.Name);

                if (printerModel != null)
                {
                    var printer = new Printer
                    {
                        Manufacturer = printerModel.Manufacturer,
                        Name = printerModel.Name,
                        Model = printerModel.Model,
                        IpAddress = null,
                        ConnectionType = Enumerations.ConnectionType.Spool
                    };

                    var displayText = string.Format("{0} ({1})", printer.Name, printer.ConnectionType);

                    cboPrinters.Items.Add(new KeyValuePair<string, Printer>(displayText, printer));

                    if (PrinterHandler != null)
                    {
                        var printerConfigText = string.Format("{0} ({1})", PrinterHandler.PrinterConfig.Name, PrinterHandler.PrinterConfig.ConnectionType);

                        if (displayText.Equals(printerConfigText))
                        {
                            cboPrinters.Text = displayText;
                        }
                    }
                }
            });
        }

        private void LoadSerialConnPrinters()
        {
            var devices = PrintUtility.GetComDevicesInfo();

            devices.ForEach(item =>
            {
                var printerModel = SearchWithInString(item.Name);

                if (printerModel != null)
                {
                    var printer = new Printer
                    {
                        Manufacturer = printerModel.Manufacturer,
                        Name = printerModel.Name,
                        Model = printerModel.Model,
                        IpAddress = null,
                        ConnectionType = item.ComPort.ToEnum<Enumerations.ConnectionType>()
                    };

                    var displayText = string.Format("{0} ({1})", printer.Name, printer.ConnectionType);

                    cboPrinters.Items.Add(new KeyValuePair<string, Printer>(displayText, printer));

                    if (PrinterHandler != null)
                    {
                        var printerConfigText = string.Format("{0} ({1})", PrinterHandler.PrinterConfig.Name, PrinterHandler.PrinterConfig.ConnectionType);

                        if (displayText.Equals(printerConfigText))
                        {
                            cboPrinters.Text = displayText;
                        }
                    }
                }
            });
        }

        private PrinterModel SearchWithInString(string printerName)
        {
            PrinterModel printerModel = null;
            CompatiblePrintersList.ForEach(item =>
            {
                if (printerName.Contains(item.Name))
                {
                    printerModel = item;
                    return;
                }
            });

            return printerModel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IsBusy = true;

                var printer = PrinterHandler != null ? (Printer)PrinterHandler.PrinterConfig : new Printer();
                var selectedPrinter = cboPrinters.SelectedItem;

                if (selectedPrinter != null)
                {
                    var item = ((KeyValuePair<string, Printer>)selectedPrinter).Value;

                    printer.Manufacturer = item.Manufacturer;
                    printer.Name = item.Name;
                    printer.Model = item.Model;
                    printer.ConnectionType = item.ConnectionType;
                }

                PrinterController.Instance.Save(printer);

                MessageBox.Show(AppStrings.Generic_SuccessMessage_Save, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                var errorMessage = string.Format(AppStrings.Generic_ErrorMessage_Save, AppStrings.Generic_ClassName_Printer);
                LogUtility.Log(LogUtility.LogType.Error, errorMessage, ex.Message);
                MessageBox.Show(errorMessage, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                IsBusy = true;

                var selectedPrinter = cboPrinters.SelectedItem;

                if (selectedPrinter != null)
                {
                    var printerConfig = new Printer();
                    var item = ((KeyValuePair<string, Printer>)selectedPrinter).Value;

                    printerConfig.Manufacturer = item.Manufacturer;
                    printerConfig.Name = item.Name;
                    printerConfig.Model = item.Model;
                    printerConfig.ConnectionType = item.ConnectionType;

                    CampaignController.Instance.PrintTestPage(PrinterController.Instance.GetTestPrinter(printerConfig));
                }
                else
                {
                    MessageBox.Show(AppStrings.ErrorMessage_Printer_Required_PrinterNotSelected, AppStrings.Generic_MessageBox_InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                LogUtility.Log(LogUtility.LogType.Error, "Printer Test", ex.Message);
                MessageBox.Show(ex.Message, AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Load();
        }
    }
}
