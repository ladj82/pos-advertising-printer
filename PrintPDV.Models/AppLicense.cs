using System;
using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class AppLicense : GenericEntity, IGenericEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_AppLicense_Required_Key")]
        [StringLength(6, MinimumLength = 6, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_AppLicense_StringLength_Key")]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_AppLicense_Required_Activation")]
        [DataType(DataType.DateTime)]
        public DateTime Activation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_AppLicense_Required_Active")]
        public bool Active { get; set; }
    }
}
