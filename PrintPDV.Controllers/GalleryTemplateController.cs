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
    public class GalleryTemplateController : GenericController<GalleryTemplate>, IGalleryTemplateController
    {
        public static IGalleryTemplateController Instance
        {
            get { return SingletonUtility<GalleryTemplateController>.Instance; }
        }

        public override void Validate(GalleryTemplate entity)
        {
            base.Validate(entity);
        }

        public List<GalleryTemplate> GetByGalleryType(Enumerations.GalleryType galleryType)
        {
            try
            {
                var predicate = Predicates.Field<GalleryTemplate>(f => f.GalleryType, Operator.Eq, galleryType);

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
