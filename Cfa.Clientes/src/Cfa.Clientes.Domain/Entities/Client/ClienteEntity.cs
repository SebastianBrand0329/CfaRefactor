using Cfa.Clientes.Domain.Entities.Address;
using Cfa.Clientes.Domain.Entities.Phone;
using System.ComponentModel.DataAnnotations;

namespace Cfa.Clientes.Domain.Entities.Client;

public class ClienteEntity
{
    public long Codigo { get; set; }  

    public string TipoDocumento { get; set; } = null!;
    public long NumeroDocumento { get; set; } 
    public string Nombres { get; set; } = null!;
    public string Apellido1 { get; set; } = null!;
    public string? Apellido2 { get; set; } 
    public string Genero { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Email { get; set; } = null!;

    public ICollection<DireccionEntity> Direcciones { get; set; } = new List<DireccionEntity>();
    public ICollection<TelefonoEntity> Telefonos { get; set; } = new List<TelefonoEntity>();
}