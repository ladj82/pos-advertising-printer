using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DapperExtensions;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;

namespace PrintPDV.Controllers
{
    public class StatisticController : GenericController<Statistic>, IStatisticController
    {
        public static IStatisticController Instance
        {
            get { return SingletonUtility<StatisticController>.Instance; }
        }

        public override void Validate(Statistic entity)
        {
            base.Validate(entity);
        }

        public List<Statistic> GetByDateRange(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                pg.Predicates.Add(Predicates.Field<Statistic>(f => f.TimeStamp, Operator.Ge, startDateTime));
                pg.Predicates.Add(Predicates.Field<Statistic>(f => f.TimeStamp, Operator.Le, endDateTime));

                return GetList(pg).ToList();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public void Sync()
        {
            try
            {
                var predicate = Predicates.Field<Statistic>(f => f.Synced, Operator.Eq, null);

                GetList(predicate).ToList().ForEach(item =>
                {
                    var syncRequest = new SyncRequest
                    {
                        License = AppLicenseController.Instance.ActiveLicense.Key,
                        FingerPrint = SecurityUtility.GetMd5Hash(AppConfigUtility.FingerPrint),
                        TimeStamp = item.TimeStamp,
                        Action = item.Action,
                        Value = item.Value.FromJSON<Dictionary<string, string>>()
                    };

                    WebServiceUtility.PostAsync(AppConfigUtility.SyncStatisticUrl, syncRequest.ToJSON());
                    WebServiceUtility.PostAsyncOnComplete += response =>
                    {
                        if (response.Success)
                        {
                            item.Synced = DateTime.Now;
                            Save(item);
                        }
                        else
                        {
                            throw new Exception(response.Message);
                        }
                    };
                });
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        private class SyncRequest
        {
            public string License { get; set; }

            public string FingerPrint { get; set; }

            public DateTime TimeStamp { get; set; }

            public string Action { get; set; }

            public Dictionary<string, string> Value { get; set; }
        }
    }
}
