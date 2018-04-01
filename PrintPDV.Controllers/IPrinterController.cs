using PrintPDV.Controllers.Base;
using PrintPDV.Controllers.Interfaces;
using PrintPDV.Models;

namespace PrintPDV.Controllers
{
    public interface IPrinterController : IBaseController<Printer>
    {
        IPrinterHandler GetPrinter();

        IPrinterHandler GetTestPrinter(IPrinter printerConfig);
    }
}
