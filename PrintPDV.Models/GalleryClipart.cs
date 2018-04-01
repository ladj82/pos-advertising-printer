using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class GalleryClipart : GenericEntity, IGenericEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_GalleryClipart_Required_Name")]
        [StringLength(50, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_GalleryClipart_StringLength_Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_GalleryClipart_Required_GalleryType")]
        public Enumerations.GalleryType GalleryType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_GalleryClipart_Required_Image")]
        public byte[] Image { get; set; }
    }
}
