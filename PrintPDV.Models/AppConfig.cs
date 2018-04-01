using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class AppConfig : GenericEntity, IGenericEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
