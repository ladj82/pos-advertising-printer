using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class PrinterModel : GenericEntity, IGenericEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Printer_Required_Manufacturer")]
        [StringLength(50, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Printer_StringLength_Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Printer_Required_Name")]
        [StringLength(50, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Printer_StringLength_Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Printer_Required_Model")]
        [StringLength(50, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Printer_StringLength_Model")]
        public string Model { get; set; }
    }
}
