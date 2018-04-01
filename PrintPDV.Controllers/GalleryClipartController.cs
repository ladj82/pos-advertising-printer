using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DapperExtensions;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public class GalleryClipartController : GenericController<GalleryClipart>, IGalleryClipartController
    {
        public static IGalleryClipartController Instance
        {
            get { return SingletonUtility<GalleryClipartController>.Instance; }
        }

        public override void Validate(GalleryClipart entity)
        {
            
        }

        public List<GalleryClipart> GetByGalleryType(Enumerations.GalleryType galleryType)
        {
            try
            {
                var predicate = Predicates.Field<GalleryClipart>(f => f.GalleryType, Operator.Eq, galleryType);

                return GetList(predicate).ToList();
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
