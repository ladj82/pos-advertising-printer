using System;
using System.ComponentModel.DataAnnotations;
using PrintPDV.LanguagePack;
using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public class Statistic : GenericEntity, IGenericEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Statistic_Required_License")]
        [StringLength(6, MinimumLength = 6, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Statistic_StringLength_License")]
        public string License { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Statistic_Required_TimeStamp")]
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Statistic_Required_Action")]
        public string Action { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(AppStrings), ErrorMessageResourceName = "ErrorMessage_Statistic_Required_Value")]
        public string Value { get; set; } // JSON format

        [DataType(DataType.DateTime)]
        public DateTime? Synced { get; set; }
    }
}
