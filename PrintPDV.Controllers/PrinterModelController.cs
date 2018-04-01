using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;

namespace PrintPDV.Controllers
{
    public class PrinterModelController : GenericController<PrinterModel>, IPrinterModelController
    {
        public static IPrinterModelController Instance
        {
            get { return SingletonUtility<PrinterModelController>.Instance; }
        }

        public override void Validate(PrinterModel entity)
        {
            base.Validate(entity);
        }
    }
}
