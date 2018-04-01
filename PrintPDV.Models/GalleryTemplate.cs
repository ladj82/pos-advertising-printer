using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class GalleryTemplate : Campaign
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_GalleryClipart_Required_GalleryType")]
        public Enumerations.GalleryType GalleryType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_GalleryClipart_Required_Image")]
        public byte[] Thumbnail { get; set; }
    }
}
