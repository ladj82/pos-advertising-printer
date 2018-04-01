using System.Runtime.InteropServices;

namespace PrintPDV.Controllers.Interfaces.Daruma
{
    public class DarumaInterface
    {
        [DllImport("DarumaFrameWork.dll")]
        public static extern int eDefinirProduto_Daruma(string sProduto);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int regPortaComunicacao_DUAL_DarumaFramework(string stParametro);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int regVelocidade_DUAL_DarumaFramework(string stParametro);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int regTermica_DUAL_DarumaFramework(string stParametro);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int regLinhasGuilhotina_DUAL_DarumaFramework(string stParametro);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int rStatusGuilhotina_DUAL_DarumaFramework();

        [DllImport("DarumaFrameWork.dll")]
        public static extern int iConfigurarGuilhotina_DUAL_DarumaFramework(string iHabilitar, string iQtdeLinha);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int rStatusImpressora_DUAL_DarumaFramework();

        [DllImport("DarumaFrameWork.dll")]
        public static extern int iImprimirBMP_DUAL_DarumaFramework(string stArqOrigem);

        [DllImport("DarumaFrameWork.dll")]
        public static extern int iImprimirTexto_DUAL_DarumaFramework(string stTexto, int iTam);
    }
}
