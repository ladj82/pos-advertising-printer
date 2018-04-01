using System.Drawing;
using PrintPDV.Models;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Interfaces
{
    public interface IPrinterHandler
    {
        IPrinter PrinterConfig { get; }

        void InitCommunication();

        void SetHeader(bool printDateTime);

        void SetImage(string imagePath);

        void SetText(string text, int FontSize = 10, FontStyle fontStyle = FontStyle.Regular, int LineHeight = 60, Enumerations.TextAlignmentType textAlignmentType = Enumerations.TextAlignmentType.Left);

        void SetVoucher(Voucher voucher = null);

        void SetFooter();

        void BreakLine(int numBreaks);

        void SetCutPaper(Enumerations.CutType printCutType);

        void CloseCommunication();

        void ExecuteCommands();
    }
}
