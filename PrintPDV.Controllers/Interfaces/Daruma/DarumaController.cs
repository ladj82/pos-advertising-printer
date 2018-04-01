using System;
using System.Collections.Generic;
using System.Drawing;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Interfaces.Daruma
{
    public class DarumaController : BasePrinterController, IPrinterHandler
    {
        public DarumaController(IPrinter printer) : base(printer)
        {
        }

        public sealed override IPrinter PrinterConfig { get; protected set; }
        
        protected override List<Command> Commands { get; set; }

        public void InitCommunication()
        {
            throw new NotImplementedException();
        }

        public void SetHeader(bool printDateTime)
        {
            throw new NotImplementedException();
        }

        public void SetImage(string imagePath)
        {
            throw new NotImplementedException();
        }

        public void SetText(string text, int FontSize = 10, FontStyle fontStyle = FontStyle.Regular, int LineHeight = 60,
            Enumerations.TextAlignmentType textAlignmentType = Enumerations.TextAlignmentType.Left)
        {
            throw new NotImplementedException();
        }

        public void SetVoucher(Voucher voucher = null)
        {
            throw new NotImplementedException();
        }

        public void SetFooter()
        {
            throw new NotImplementedException();
        }

        public void BreakLine(int numBreaks)
        {
            throw new NotImplementedException();
        }

        public void SetCutPaper(Enumerations.CutType printCutType)
        {
            throw new NotImplementedException();
        }

        public void CloseCommunication()
        {
            throw new NotImplementedException();
        }
    }
}
