using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PrintPDV.Controllers.Base;
using PrintPDV.Controllers.Interfaces;
using PrintPDV.Controllers.Interfaces.Bematech;
using PrintPDV.Controllers.Interfaces.Spool;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public class PrinterController : GenericController<Printer>, IPrinterController
    {
        private static IPrinterHandler Printer;

        public static IPrinterController Instance
        {
            get { return SingletonUtility<PrinterController>.Instance; }
        }

        public override void Validate(Printer entity)
        {
            base.Validate(entity);
        }

        public override dynamic Save(Printer entity, int? commandTimeout = null)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();

                Printer = null;

                var license = AppLicenseController.Instance.ActiveLicense;
                var licenseKey = license != null ? license.Key : string.Empty;
                var timeStamp = DateTime.Now;
                var action = "SetupPrinter";
                var value = new Dictionary<string, string> { { "Printer", string.Format("{0} - {1} - {2}", entity.Manufacturer, entity.Name, entity.ConnectionType) } }.ToJSON();

                var statistic = new Statistic
                {
                    License = licenseKey,
                    TimeStamp = timeStamp,
                    Action = action,
                    Value = value
                };

                StatisticController.Instance.Insert(statistic);

                return base.Save(entity, commandTimeout);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public IPrinterHandler GetPrinter()
        {
            try
            {
                if (Printer != null)
                    return Printer;

                var printer = GetList().FirstOrDefault();

                Printer = GetPrinterHandlerByPrinter(printer);

                return Printer;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public IPrinterHandler GetTestPrinter(IPrinter printerConfig)
        {
            try
            {
                if (printerConfig == null)
                    throw new ArgumentNullException();

                return GetPrinterHandlerByPrinter(printerConfig);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private IPrinterHandler GetPrinterHandlerByPrinter(IPrinter printerConfig)
        {
            try
            {
                if (printerConfig == null) 
                    throw new PrinterNotDefinedException();

                if (printerConfig.ConnectionType == Enumerations.ConnectionType.Spool)
                {
                    return new SpoolController(printerConfig);
                }

                //TODO: Implement another calls
                switch (printerConfig.Manufacturer)
                {
                    case "Bematech":
                        return new BematechController(printerConfig);
                    default:
                        throw new PrinterHandlerNotResolvedException();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
