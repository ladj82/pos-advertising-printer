namespace PrintPDV.Utility.Models
{
    public class Enumerations
    {
        public enum CampaignType
        {
            Desconto = 1,
            Enquete = 2,
            Promoção = 3,
            Propaganda = 4,
            Informativo = 5,
            Sorteio = 6,
            Outros = 7
        }

        public enum ConnectionType
        {
            Spool = 1,
            USB = 2,
            COM1 = 3,
            COM2 = 4,
            COM3 = 5,
            COM4 = 6,
            COM5 = 7,
            COM6 = 8,
            COM7 = 9,
            LPT1 = 10,
            LPT2 = 11,
            Remota = 12
        }

        public enum TextAlignmentType
        {
            Left = 1,
            Center = 2,
            Right = 3
        }

        public enum CutType
        {
            Nenhum = 1,
            Parcial = 2,
            Completo = 3
        }

        public enum TriggerType
        {
            Atalho = 1,
            Gadget = 2,
            Teste = 3
        }

        public enum PaperSize
        {
            Tamanho_4 = 1,
            Tamanho_3 = 2,
            Tamanho_2 = 3,
            Tamanho_1 = 4
        }

        public enum BarcodeType
        {
            Nenhum = 1,
            CODE128 = 2
        }

        public enum GalleryType
        {
            Genérico = 1,
            Alimentação = 2,
            Vestuário = 3,
            Combustível = 4,
            Farmácia = 5,
            Web = 6
        }
    }
}
