using System.Collections.Generic;
using DapperExtensions;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using PrintPDV.Controllers.Interfaces;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public class CampaignController : GenericController<Campaign>, ICampaignController
    {
        private IPrinterHandler Printer;

        public static ICampaignController Instance
        {
            get { return SingletonUtility<CampaignController>.Instance; }
        }

        public override void Validate(Campaign entity)
        {
            base.Validate(entity);

            #region Unique Name validation

            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<Campaign>(f => f.Name, Operator.Eq, entity.Name));
            pg.Predicates.Add(Predicates.Field<Campaign>(f => f.Id, Operator.Eq, entity.Id, true));

            if (GetList(pg).Any())
                throw new CampaignUniqueNameException();

            #endregion
        }

        public List<Campaign> GetByPrintVoucher()
        {
            try
            {
                var predicate = Predicates.Field<Campaign>(f => f.PrintVoucher, Operator.Eq, true);

                return GetList(predicate).ToList();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public Campaign GetByShortcut(string shortcut)
        {
            try
            {
                if (string.IsNullOrEmpty(shortcut))
                    throw new ArgumentNullException();

                var predicate = Predicates.Field<Campaign>(f => f.Shortcut, Operator.Eq, shortcut);

                return GetList(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public List<Campaign> GetByActive(bool active = true)
        {
            try
            {
                return GetList().Where(q => q.Active.Equals(active)).ToList();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public void SetupShortcuts()
        {
            try
            {
                ShortcutHandler.Instance.UnsetHotKeys();

                GetList()
                    .Where(p => !string.IsNullOrEmpty(p.Shortcut))
                    .ToList()
                    .ForEach(item => ShortcutUtility.RegisterHotKey(item.Shortcut, ShortcutAction));
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public void PrintCampaign(Campaign campaign, Enumerations.TriggerType printTriggerType)
        {
            try
            {
                if (campaign == null)
                    throw new ArgumentNullException();

                Printer = PrinterController.Instance.GetPrinter();

                Voucher voucher;

                GeneralUtility.DateTime = DateTime.Now;

                HandleVoucher(campaign, out voucher);

                HandlePrinting(campaign, voucher);

                HandleStatistic("Printing", new Dictionary<string, string>
                {
                    {"CampaignType", campaign.CampaignType.ToString()},
                    {"Trigger", printTriggerType.ToString()}
                });
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public void PrintTestPage(IPrinterHandler printerHandler)
        {
            try
            {
                if (printerHandler == null)
                    throw new ArgumentNullException();

                printerHandler.InitCommunication();

                printerHandler.SetHeader(true);

                printerHandler.SetText(AppStrings.InfoMessage_Printer_Info_PrintTestPage, 12, FontStyle.Bold, 60, Enumerations.TextAlignmentType.Center);

                printerHandler.SetText(string.Format("{0} - {1} - {2}", printerHandler.PrinterConfig.Manufacturer, printerHandler.PrinterConfig.Name, printerHandler.PrinterConfig.ConnectionType), 10, FontStyle.Regular, 60, Enumerations.TextAlignmentType.Center);

                printerHandler.SetFooter();

                printerHandler.SetCutPaper(Enumerations.CutType.Completo);

                printerHandler.CloseCommunication();

                printerHandler.ExecuteCommands();

                HandleStatistic("TestPrinter", new Dictionary<string, string>
                {
                    {
                        "Printer",
                        string.Format("{0} - {1} - {2}", printerHandler.PrinterConfig.Manufacturer,
                            printerHandler.PrinterConfig.Name, printerHandler.PrinterConfig.ConnectionType)
                    }
                });
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public string GetCampaignImagePath(Campaign campaign)
        {
            try
            {
                if (campaign == null)
                    throw new ArgumentNullException();

                return string.Format(@"{0}\{1}.bmp", AppConfigUtility.CampaignPath, campaign.Name);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private void ShortcutAction(object sender, KeyPressedEventArgs e)
        {
            try
            {
                if (e == null)
                    throw new ArgumentNullException();

                var campaign = GetByShortcut(ShortcutUtility.GetShortcut(e));

                if (campaign == null) return;

                PrintCampaign(campaign, Enumerations.TriggerType.Atalho);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private void HandleVoucher(Campaign campaign, out Voucher voucher)
        {
            try
            {
                if (campaign == null)
                    throw new ArgumentNullException();

                voucher = null;

                if (campaign.PrintVoucher)
                {
                    voucher = new Voucher
                    {
                        Campaign_Id = campaign.Id,
                        BarcodeType = campaign.VoucherBarcodeType,
                        Code = VoucherController.Instance.GenerateCode(campaign.Id),
                        Created = GeneralUtility.DateTime ?? DateTime.Now,
                        Used = null
                    };

                    VoucherController.Instance.Insert(voucher);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private void HandlePrinting(Campaign campaign, Voucher voucher = null)
        {
            try
            {
                if (campaign == null)
                    throw new ArgumentNullException();

                Printer.InitCommunication();

                Printer.SetHeader(campaign.PrintDateTime);

                Printer.SetImage(GetCampaignImagePath(campaign));

                Printer.SetVoucher(voucher);

                Printer.SetFooter();

                Printer.SetCutPaper(campaign.CutType);

                Printer.CloseCommunication();

                Printer.ExecuteCommands();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private void HandleStatistic(string action, Dictionary<string, string> value)
        {
            try
            {
                if (action == null || value == null)
                    throw new ArgumentNullException();

                var license = AppLicenseController.Instance.ActiveLicense;

                var statistic = new Statistic
                {
                    License = license.Key,
                    TimeStamp = GeneralUtility.DateTime ?? DateTime.Now,
                    Action = action,
                    Value = value.ToJSON(),
                    Synced = null
                };

                StatisticController.Instance.Insert(statistic);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}