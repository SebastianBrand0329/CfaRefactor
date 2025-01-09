using Cfa.Clientes.Domain.Entities.Address;
using Cfa.Clientes.Domain.Entities.Phone;
using System.ComponentModel.DataAnnotations;

namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;

public class CreateClientModel
{
    [Key]
    public int Codigo { get; set; }
    public string TipoDocumento { get; set; } = null!;
    public long NumeroDocumento { get; set; }
    public string Nombres { get; set; } = null!;
    public string Apellido1 { get; set; } = null!;
    public string Apellido2 { get; set; } = null!;
    public string Genero { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    public string Email { get; set; } = null!;
    public List<TelefonoModel> Telefonos { get; set; } = null!;
    public List<DireccionModel> Direcciones { get; set; } = null!;
}