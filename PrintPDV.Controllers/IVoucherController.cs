using System.Collections.Generic;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;

namespace PrintPDV.Controllers
{
    public interface IVoucherController : IBaseController<Voucher>
    {
        string GenerateCode(int campaignId);

        List<Voucher> GetByCampaignId(int campaignId);

        Voucher GetByCode(string code);

        bool IsValid(Voucher voucher);

        void SetAsUsed(Voucher voucher);
    }
}
