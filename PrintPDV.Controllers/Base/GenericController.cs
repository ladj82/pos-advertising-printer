using System;
using System.Linq;
using PrintPDV.Utility;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Base
{
    public class GenericController<T> : BaseController<T> where T : class, IGenericEntity
    {
        public override void Validate(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(typeof(T).FullName);

            var errors = entity.ValidateAnnotations();

            if (errors != null && errors.Any())
                throw new AnnotationException(errors);
        }
    }
}
