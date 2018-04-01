using System;
using System.Collections.Generic;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using System.Linq;
using System.Reflection;
using DapperExtensions;
using PrintPDV.Utility;

namespace PrintPDV.Controllers
{
    public class AppLicenseController : GenericController<AppLicense>, IAppLicenseController
    {
        public AppLicense ActiveLicense { get; private set; }

        public static IAppLicenseController Instance
        {
            get { return SingletonUtility<AppLicenseController>.Instance; }
        }

        public override void Validate(AppLicense entity)
        {
            base.Validate(entity);
        }

        public AppLicense ValidateLicense(string license)
        {
            try
            {
                if (string.IsNullOrEmpty(license))
                    throw new ArgumentNullException();

                if (IsValid(license))
                {
                    ActiveLicense = GetActiveLicense();

                    if (ActiveLicense != null)
                    {
                        if (ActiveLicense.Key.Equals(license))
                        {
                            return ActiveLicense;
                        }
                        else
                        {
                            DeactivatePreviousLicenses();

                            ActiveLicense = InsertNewLicense(license);

                            return ActiveLicense;
                        }
                    }
                    else
                    {
                        ActiveLicense = InsertNewLicense(license);

                        return ActiveLicense;
                    }
                }

                throw new LicenseNotValidException();
            }
            catch (LicenseNotValidException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Erro durante a validação da licença. Verifique sua conexão com a internet."); //TODO: put on resource file
            }
        }

        private bool IsValid(string license)
        {
            try
            {
                if (string.IsNullOrEmpty(license))
                    throw new ArgumentNullException();

                var request = new ValidateRequest
                {
                    License = license,
                    FingerPrint = SecurityUtility.GetMd5Hash(AppConfigUtility.FingerPrint)
                };

                var response = WebServiceUtility.Post(AppConfigUtility.ValidateLicenseUrl, request.ToJSON());

                if (!response.Success)
                    throw new LicenseNotValidException(response.Message);

                return true;
            }
            catch (LicenseNotValidException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                return false;
            }
        }

        private AppLicense InsertNewLicense(string license)
        {
            try
            {
                var newLicense = new AppLicense { Key = license, Activation = DateTime.Now, Active = true };

                Instance.Save(newLicense);
                return newLicense;
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private void DeactivatePreviousLicenses()
        {
            try
            {
                Instance.GetList().ToList().ForEach(item =>
                {
                    item.Active = false;
                    Instance.Save(item);
                });
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private class ValidateRequest
        {
            public string License { get; set; }

            public string FingerPrint { get; set; }
        }

        public AppLicense GetActiveLicense()
        {
            try
            {
                var predicate = Predicates.Field<AppLicense>(f => f.Active, Operator.Eq, true);

                return GetList(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
