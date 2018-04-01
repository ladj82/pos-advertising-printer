using PrintPDV.Utility.Models;

namespace PrintPDV.Models
{
    public interface IPrinter : IGenericEntity
    {
        string Manufacturer { get; set; }

        string Name { get; set; }

        string Model { get; set; }

        Enumerations.ConnectionType ConnectionType { get; set; }

        string IpAddress { get; set; }
    }
}