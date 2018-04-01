using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Interfaces.Spool
{
    public class SpoolController : BasePrinterController, IPrinterHandler
    {
        public override IPrinter PrinterConfig { get; protected set; }

        protected override List<Command> Commands { get; set; }

        private List<Image> ImagesToPrint { get; set; }

        private const int ImageWidth = 280;

        public SpoolController(IPrinter printer)
            : base(printer)
        {
            ImagesToPrint = new List<Image>();
        }

        private int AddImage(Image img)
        {
            try
            {
                ImagesToPrint.Add(img);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void InitCommunication()
        {
            //Handled by spool driver
        }

        public void SetHeader(bool printDateTime)
        {
            if (!printDateTime) return;

            var dateTime = GeneralUtility.DateTime ?? DateTime.Now;

            SetText(dateTime.ToString("dd/MM/yy H:mm:ss"), 10, FontStyle.Regular, 60, Enumerations.TextAlignmentType.Center);
        }

        public void SetImage(string imagePath)
        {
            Commands.Add(new Command("SetImage", () => AddImage(Image.FromStream(new MemoryStream(File.ReadAllBytes(imagePath))))));
        }

        public void SetText(string text, int FontSize = 10, FontStyle fontStyle = FontStyle.Regular, int LineHeight = 60, Enumerations.TextAlignmentType textAlignmentType = Enumerations.TextAlignmentType.Left)
        {
            var alignment = new StringFormat();

            switch (textAlignmentType)
            {
                case Enumerations.TextAlignmentType.Right:
                    alignment.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                    alignment.LineAlignment = StringAlignment.Center;
                    break;
                    
                case Enumerations.TextAlignmentType.Center:
                    alignment.Alignment = StringAlignment.Center;
                    alignment.LineAlignment = StringAlignment.Center;
                    break;

                case Enumerations.TextAlignmentType.Left:
                default:
                    alignment.LineAlignment = StringAlignment.Center;
                    break;
            }

            var img = ImageUtility.GetImageText(
                text,
                FontSize,
                fontStyle,
                ImageWidth,
                LineHeight,
                alignment
            );

            Commands.Add(new Command("SetText", () => AddImage(img)));
        }

        public void SetVoucher(Voucher voucher = null)
        {
            if (voucher == null) return;

            var barcodeImg = ImageUtility.GetBarcodeCode128(voucher.Code, 50, ImageWidth);

            BreakLine(1);

            Commands.Add(new Command("SetVoucher", () => AddImage(barcodeImg)));

            SetText(voucher.Code, 10, FontStyle.Bold, 60, Enumerations.TextAlignmentType.Center);
        }

        public void SetFooter()
        {
            SetText(AppConfigUtility.WebsiteUrl, 10, FontStyle.Bold, 60, Enumerations.TextAlignmentType.Center);
        }

        public void BreakLine(int numBreaks)
        {
            if (numBreaks <= 0) return;

            while (numBreaks > 0)
            {
                SetText(string.Empty);
                numBreaks--;
            }
        }

        public void SetCutPaper(Enumerations.CutType printCutType)
        {
            //Handled by spool driver
        }

        public void CloseCommunication()
        {
            //Handled by spool driver
        }

        public override void ExecuteCommands()
        {
            base.ExecuteCommands();

            Image img = null;
            var count = 0;

            ImagesToPrint.ForEach(item =>
            {
                img = count.Equals(0) ? item : ImageUtility.MergeImageAtBottom(img, item);
                count++;
            });

            if (img == null) return;

            ImagesToPrint.Clear();

            var printDocHandler = new PrintDocument
            {
                PrinterSettings = { PrinterName = PrinterConfig.Name },
                DefaultPageSettings = { PaperSize = new PaperSize(@"PRINTPDV", img.Width, img.Height) },
                DocumentName = AppConfigUtility.DefaultPrintDocumentName
            };

            printDocHandler.PrintPage += (sender, args) => args.Graphics.DrawImage(img, args.PageBounds);

            printDocHandler.Print();
        }
    }
}
