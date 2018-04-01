namespace PrintPDV.Controllers.Interfaces.Elgin
{
    public class ElginVoxInterface
    {
        public static readonly int[] InitPrinter = { 27, 64 };
        public static readonly int[] DefaultJustification = { 27, 97, 0 };
        public static readonly int[] LeftJustification = { 27, 97, 48 };
        public static readonly int[] RightJustification = { 27, 97, 2 };
        public static readonly int[] CenterJustification = { 27, 97, 1 };
        public static readonly int[] DefaultModeText = { 27, 33, 0 };
        public static readonly int[] AlternativeModeText1 = { 27, 33, 1 };
        public static readonly int[] EmphasizedModeText = { 27, 33, 8 };
        public static readonly int[] DoubleWidthModeText = { 27, 33, 32 };
        public static readonly int[] DoubleHeightModeText = { 27, 33, 16 };
        public static readonly int[] UnderlineModeText = { 27, 33, 128 };
        public static readonly int[] LineFeed = { 10 };
        public static readonly int[] CutPaper = { 29, 86, 0 };
        public static readonly int[] BarcodeWidth1 = { 29, 119, 2 };
        public static readonly int[] BarcodeWidth2 = { 29, 119, 3 };
        public static readonly int[] BarcodeWidth3 = { 29, 119, 4 };
        public static readonly int[] BarcodeWidth4 = { 29, 119, 5 };
        public static readonly int[] BarcodeWidth5 = { 29, 119, 6 };
        public static readonly int[] BarcodeHeight = { 29, 104, 162 }; // 162 = range 1 ≤ n ≤ 255
        public static readonly int[] BarcodeHRIChars1 = { 29, 102, 0 };
        public static readonly int[] BarcodeHRIChars2 = { 29, 102, 1 };
        public static readonly int[] BarcodeHRIPosition1 = { 29, 72, 0 };
        public static readonly int[] BarcodeHRIPosition2 = { 29, 72, 1 };
        public static readonly int[] BarcodeHRIPosition3 = { 29, 72, 2 };
        public static readonly int[] BarcodeHRIPosition4 = { 29, 72, 3 };
        public static readonly int[] BarcodeJAN = { 29, 107, 2 };
        public static readonly int[] BarcodeCODE128 = { 29, 107, 73 };
    }
}
