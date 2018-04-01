using System;
using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class Voucher : GenericEntity, IGenericEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Voucher_Required_Campaign_Id")]
        public int Campaign_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Voucher_Required_BarcodeType")]
        public Enumerations.BarcodeType BarcodeType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Voucher_Required_Code")]
        [StringLength(7, MinimumLength = 7, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Voucher_StringLength_Code")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Voucher_Required_Created")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Used { get; set; }
    }
}
