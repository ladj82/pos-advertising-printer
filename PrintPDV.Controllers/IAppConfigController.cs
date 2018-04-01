using PrintPDV.Controllers.Base;
using PrintPDV.Models;

namespace PrintPDV.Controllers
{
    public interface IAppConfigController : IBaseController<AppConfig>
    {
        void SetAppConfig();
    }
}
