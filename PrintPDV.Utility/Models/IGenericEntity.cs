
namespace PrintPDV.Utility.Models
{
    public interface IGenericEntity : IGenericEntity<int>
    {

    }

    public interface IGenericEntity<out TId>
    {
        TId Id { get; }
    }
}
