using System.Collections.Generic;
using PrintPDV.Controllers.Base;
using PrintPDV.Controllers.Interfaces;
using PrintPDV.Models;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public interface ICampaignController : IBaseController<Campaign>
    {
        List<Campaign> GetByPrintVoucher();

        Campaign GetByShortcut(string shortcut);

        List<Campaign> GetByActive(bool active = true);
        
        void SetupShortcuts();

        void PrintCampaign(Campaign campaign, Enumerations.TriggerType printTriggerType);

        void PrintTestPage(IPrinterHandler printerHandler);

        string GetCampaignImagePath(Campaign campaign);
    }
}
