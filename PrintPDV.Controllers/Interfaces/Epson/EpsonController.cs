using System;
using System.Collections.Generic;
using System.Drawing;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Interfaces.Epson
{
    public class EpsonController : BasePrinterController, IPrinterHandler
    {
        public EpsonController(IPrinter printer) : base(printer)
        {
        }

        public sealed override IPrinter PrinterConfig { get; protected set; }
        
        protected override List<Command> Commands { get; set; }

        private const int TotalCharactersPerLine = 48;

        public void InitCommunication()
        {
            var printerCommunicationPort = GetPrinterComPort;

            Commands.Add(new Command("EpsonInterface.IniciaPorta", () => EpsonInterface.IniciaPorta(printerCommunicationPort)));
        }

        public void SetHeader(bool printDateTime)
        {
            if (printDateTime)
            {
                var dateTime = GeneralUtility.DateTime ?? DateTime.Now;
                var text = dateTime.ToString("dd/MM/yy H:mm:ss");

                SetText(text);

                BreakLine(1);
            }
        }

        public void SetImage(string imagePath)
        {
            Commands.Add(new Command("EpsonInterface.ImprimeBmpEspecial", () => EpsonInterface.ImprimeBmpEspecial(imagePath, -1, 0, 0)));
        }

        public void SetText(string text, int FontSize = 10, FontStyle fontStyle = FontStyle.Regular, int LineHeight = 60,
            Enumerations.TextAlignmentType textAlignmentType = Enumerations.TextAlignmentType.Left)
        {
            switch (textAlignmentType)
            {
                case Enumerations.TextAlignmentType.Center:
                    text = PrintUtility.GetCenterAlignmentText(text, TotalCharactersPerLine);
                    break;

                case Enumerations.TextAlignmentType.Right:
                    text = PrintUtility.GetRightAlignmentText(text, TotalCharactersPerLine);
                    break;

                case Enumerations.TextAlignmentType.Left:
                default:
                    break;
            }

            int italic = fontStyle.ToString().Contains(FontStyle.Italic.ToString()) ? 1 : 0;
            int underline = fontStyle.ToString().Contains(FontStyle.Underline.ToString()) ? 1 : 0;
            int bold = fontStyle.ToString().Contains(FontStyle.Bold.ToString()) ? 1 : 0;

            Commands.Add(new Command("EpsonInterface.FormataTX", () => EpsonInterface.FormataTX(text, 2, italic, underline, 0, bold)));

            BreakLine(1);
        }

        public void SetVoucher(Voucher voucher = null)
        {
            if (voucher == null) return;

            Commands.Add(new Command("EpsonInterface.ConfiguraCodigoBarras", () => EpsonInterface.ConfiguraCodigoBarras(50, 0, 2, 0, 90)));

            BreakLine(1);

            //TODO: Implement all Barcode Types
            switch (voucher.BarcodeType)
            {
                case Enumerations.BarcodeType.CODE128:
                    Commands.Add(new Command("EpsonInterface.ImprimeCodigoBarrasCODE12", () => EpsonInterface.ImprimeCodigoBarrasCODE128(voucher.Code)));
                    break;
            }
        }

        public void SetFooter()
        {
            BreakLine(1);

            SetText(AppConfigUtility.WebsiteUrl, -1, FontStyle.Bold, -1, Enumerations.TextAlignmentType.Center);

            BreakLine(1);
        }

        public void BreakLine(int numBreaks)
        {
            if (numBreaks <= 0) return;

            const string cmdBreakLine = "\r\n";

            while (numBreaks > 0)
            {
                Commands.Add(new Command("EpsonInterface.ComandoTX", () => EpsonInterface.ComandoTX(cmdBreakLine, cmdBreakLine.Length)));
                numBreaks--;
            }
        }

        public void SetCutPaper(Enumerations.CutType printCutType)
        {
            switch (printCutType)
            {
                case Enumerations.CutType.Nenhum:
                    // No cut.
                    break;

                case Enumerations.CutType.Parcial:
                    Commands.Add(new Command("EpsonInterface.AcionaGuilhotina", () => EpsonInterface.AcionaGuilhotina(0)));
                    break;

                case Enumerations.CutType.Completo:
                    Commands.Add(new Command("EpsonInterface.AcionaGuilhotina", () => EpsonInterface.AcionaGuilhotina(1)));
                    break;
            }
        }

        public void CloseCommunication()
        {
            Commands.Add(new Command("EpsonInterface.FechaPorta", () => EpsonInterface.FechaPorta()));
        }

        private string GetPrinterComPort
        {
            get
            {
                return !string.IsNullOrEmpty(PrinterConfig.IpAddress) ? PrinterConfig.IpAddress : PrinterConfig.ConnectionType.ToString();
            }
        }
    }
}
