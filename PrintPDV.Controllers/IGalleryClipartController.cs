using System.Collections.Generic;
using PrintPDV.Controllers.Base;
using PrintPDV.Models;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers
{
    public interface IGalleryClipartController : IBaseController<GalleryClipart>
    {
        List<GalleryClipart> GetByGalleryType(Enumerations.GalleryType galleryType);
    }
}
