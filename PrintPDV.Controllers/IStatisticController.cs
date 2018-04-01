using System;
using System.Collections.Generic;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;

namespace PrintPDV.Controllers
{
    public interface IStatisticController : IBaseController<Statistic>
    {
        List<Statistic> GetByDateRange(DateTime startDateTime, DateTime endDateTime);

        void Sync();
    }
}
