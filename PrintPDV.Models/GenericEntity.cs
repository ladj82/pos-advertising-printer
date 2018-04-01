using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public abstract class GenericEntity : GenericEntity<int>
    {

    }

    public abstract class GenericEntity<TId> : IGenericEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
