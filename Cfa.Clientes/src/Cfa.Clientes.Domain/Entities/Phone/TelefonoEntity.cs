using Cfa.Clientes.Domain.Entities.Client;
using System.ComponentModel.DataAnnotations;

namespace Cfa.Clientes.Domain.Entities.Phone;

public class TelefonoEntity
{
    public long TelefonoID { get; set; } 
    public long CodigoCliente { get; set; } 
    public long Telefono { get; set; }
    public string TipoTelefono { get; set; } 

    public ClienteEntity Cliente { get; set; }
}