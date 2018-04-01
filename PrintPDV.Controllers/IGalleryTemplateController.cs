using System.Collections.Generic;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public interface IGalleryTemplateController : IBaseController<GalleryTemplate>
    {
        List<GalleryTemplate> GetByGalleryType(Enumerations.GalleryType galleryType);
    }
}
