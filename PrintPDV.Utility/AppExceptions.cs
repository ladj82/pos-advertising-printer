using System;
using System.Collections.Generic;
using PrintPDV.LanguagePack;

namespace PrintPDV.Utility
{
    #region AppConfig Exceptions

    public class DatabaseNotFoundException : Exception
    {
        public override string Message
        {
            get { return AppStrings.Application_ErrorMessage_DatabaseNotFound; }
        }
    }

    #endregion

    #region Annotation Exceptions

    public class AnnotationException : Exception
    {
        private readonly List<string> _errorList;

        public AnnotationException(List<string> errorList)
        {
            _errorList = errorList;
        }

        public override string Message
        {
            get { return string.Join(Environment.NewLine, _errorList.ToArray()); }
        }
    }

    #endregion

    #region AppLicense Exceptions

    public class LicenseNotFoundException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_License_Rule_NotFound; }
        }
    }

    public class LicenseNotValidException : Exception
    {
        private readonly string _message;
        private readonly string _details;

        public LicenseNotValidException()
        {
            _message = AppStrings.ErrorMessage_License_Rule_NotValid;
            _details = null;
        }

        public LicenseNotValidException(string details)
        {
            _message = AppStrings.ErrorMessage_License_Rule_NotValid;
            _details = details;
        }

        public override string Message
        {
            get { return _message; }
        }

        public string Details
        {
            get { return _details; }
        }
    }

    #endregion

    #region Printer Exceptions

    public class PrinterNotDefinedException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_Printer_Rule_PrinterNotDefined; }
        }
    }

    public class PrinterHandlerNotResolvedException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_Printer_Rule_PrinterHandlerNotResolved; }
        }
    }

    #endregion

    #region Campaign Exception

    public class CampaignUniqueNameException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_Campaign_Rule_UniqueName; }
        }
    }

    #endregion

    #region Voucher Exceptions

    public class VoucherNotFoundException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_Voucher_Rule_NotFound; }
        }
    }

    public class VoucherAlreadyUsedException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_Voucher_Rule_AlreadyUsed; }
        }
    }

    #endregion

    #region Statistic Exceptions

    public class StatisticSyncException : Exception
    {
        public override string Message
        {
            get { return AppStrings.ErrorMessage_Statistic_Rule_SyncError; }
        }
    }

    #endregion
}
