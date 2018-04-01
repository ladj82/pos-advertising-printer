using PrintPDV.Controllers.Base;
using PrintPDV.Models;

namespace PrintPDV.Controllers
{
    public interface IAppLicenseController : IBaseController<AppLicense>
    {
        AppLicense ActiveLicense { get; }

        AppLicense GetActiveLicense();

        AppLicense ValidateLicense(string license);
    }
}
