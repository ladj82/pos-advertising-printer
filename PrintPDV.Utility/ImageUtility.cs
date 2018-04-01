using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using Zen.Barcode;

namespace PrintPDV.Utility
{
    public class ImageUtility
    {
        public static string ImageToBase64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                var imageBytes = ms.ToArray();
                var base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            var imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);

            return image;
        }

        public static byte[] ImageToByte(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                var imageBytes = ms.ToArray();
                
                return imageBytes;
            }
        }

        public static Image ByteToImage(byte[] imageBytes)
        {
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            
            return image;
        }

        public static Bitmap ConvertToGrayScale(Bitmap image)
        {
            for (var y = 0; y < image.Height; y++)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    var c = image.GetPixel(x, y);
                    var rgb = (c.R + c.G + c.B)/3;
                    image.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }

            return image;
        }

        public static Image ConvertToBlackWhite(Image image)
        {
            var grayMatrix = new[]
            { 
                new[] { 0.299f, 0.299f, 0.299f, 0, 0 }, 
                new[] { 0.587f, 0.587f, 0.587f, 0, 0 }, 
                new[] { 0.114f, 0.114f, 0.114f, 0, 0 }, 
                new float[] { 0, 0, 0, 1, 0 }, 
                new float[] { 0, 0, 0, 0, 1 } 
            };

            var ia = new ImageAttributes();
            var rc = new Rectangle(0, 0, image.Width, image.Height);

            ia.SetColorMatrix(new ColorMatrix(grayMatrix));
            ia.SetThreshold(0.35f);

            var src = new Bitmap(image);
            var target = new Bitmap(src.Size.Width, src.Size.Height);

            using (var g = Graphics.FromImage(target))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(src, rc, 0, 0, target.Width, target.Height, GraphicsUnit.Pixel, ia);
            }

            return target;
        }

        public static Image ResizeImageFixedWidth(Image image, int width)
        {
            var sourceWidth = image.Width;
            var sourceHeight = image.Height;

            var nPercent = width / (float)sourceWidth;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var img = new Bitmap(destWidth, destHeight);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(image, 0, 0, destWidth, destHeight);
            }

            return img;
        }

        public static Image GetImageText(string text, int fontSize, FontStyle fontStyle, int? width, int? height, StringFormat stringFormat)
        {
            var imgWidth = 0;
            var imgHeight = 0;
            var font = new Font("Verdana", fontSize, fontStyle);

            if (!width.HasValue)
            {
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    imgWidth = Convert.ToInt32(g.MeasureString(text, font).Width);
                }
            }
            else
            {
                imgWidth = width.Value;
            }

            if (!height.HasValue)
            {
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    imgHeight = Convert.ToInt32(g.MeasureString(text, font).Height);
                }
            }
            else
            {
                imgHeight = height.Value;
            }

            var img = new Bitmap(imgWidth, imgHeight);

            var canvas = new RectangleF(0, 0, imgWidth, imgHeight);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.DrawString(text, font, Brushes.Black, canvas, stringFormat);
            }

            return img;
        }

        public static Image MergeImageAtBottom(Image image1, Image image2)
        {
            var img = new Bitmap(Math.Max(image1.Width, image1.Width), image1.Height + image2.Height);

            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.White);
                g.DrawImage(image1, 0, 0);
                g.DrawImage(image2, 0, image1.Height);
            }

            return img;
        }

        public static Image GetBarcodeCode128(string code, int barcodeHeight, int canvasWidth)
        {
            var barCode = BarcodeDrawFactory.Code128WithChecksum;
            var imgBarcode = barCode.Draw(code, barcodeHeight);
            var barcodeCenter = canvasWidth/2 - imgBarcode.Width/2;

            var img = new Bitmap(canvasWidth, imgBarcode.Height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(imgBarcode, barcodeCenter, 0);
            }

            return img;
        }
    }
}
