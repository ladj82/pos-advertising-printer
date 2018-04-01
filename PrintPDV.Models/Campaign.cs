using System;
using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class Campaign : GenericEntity, IGenericEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_Name")]
        [StringLength(50, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_StringLength_Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_Name")]
        public Enumerations.CampaignType CampaignType { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_StringLength_Shortcut")]
        public string Shortcut { get; set; }

        public string Source { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_StringLength_BorderStyle")]
        public string BorderStyle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_Name")]
        [Range(0, 100, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Range_BorderWidth")]
        public int BorderWidth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_PrintVoucher")]
        public bool PrintVoucher { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_VoucherBarcodeType")]
        public Enumerations.BarcodeType VoucherBarcodeType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_PrintDateTime")]
        public bool PrintDateTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_CutType")]
        public Enumerations.CutType CutType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_PaperSize")]
        public Enumerations.PaperSize PaperSize { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_Active")]
        public bool Active { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Campaign_Required_Created")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }
    }
}
