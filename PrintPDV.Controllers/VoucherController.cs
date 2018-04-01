using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DapperExtensions;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;

namespace PrintPDV.Controllers
{
    public class VoucherController : GenericController<Voucher>, IVoucherController
    {
        public static IVoucherController Instance
        {
            get { return SingletonUtility<VoucherController>.Instance; }
        }

        public override void Validate(Voucher entity)
        {
            base.Validate(entity);
        }

        public override dynamic Save(Voucher entity, int? commandTimeout = null)
        {
            try
            {
                var license = AppLicenseController.Instance.ActiveLicense;
                var campaign = CampaignController.Instance.Get(entity.Campaign_Id);
                var statistic = new Statistic
                {
                    License = license != null ? license.Key : string.Empty,
                    TimeStamp = DateTime.Now,
                    Action = "Voucher",
                    Value = new Dictionary<string, string>
                    {
                        {"Campaign", campaign.Name},
                        {"Code", entity.Code},
                        {"Created", entity.Created.ToString(CultureInfo.InvariantCulture)},
                        {"Used", entity.Used.ToString()}
                    }.ToJSON(),
                    Synced = null
                };

                StatisticController.Instance.Insert(statistic);

                return base.Save(entity, commandTimeout);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public string GenerateCode(int campaignId)
        {
            try
            {
                return string.Format("{0}-{1}", campaignId.ToString("D3"), GeneralUtility.GetRandomAlphaNumericString(3));
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public List<Voucher> GetByCampaignId(int campaignId)
        {
            try
            {
                var predicate = Predicates.Field<Voucher>(f => f.Campaign_Id, Operator.Eq, campaignId);

                return GetList(predicate).ToList();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public Voucher GetByCode(string code)
        {
            try
            {
                var predicate = Predicates.Field<Voucher>(f => f.Code, Operator.Eq, code);

                return GetList(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public bool IsValid(Voucher voucher)
        {
            if (voucher == null)
                throw new VoucherNotFoundException();

            if (voucher.Used != null)
                throw new VoucherAlreadyUsedException();

            return true;
        }

        public void SetAsUsed(Voucher voucher)
        {
            try
            {
                if (voucher == null)
                    throw new ArgumentNullException();

                voucher.Used = DateTime.Now;

                Save(voucher);
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
