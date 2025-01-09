using Cfa.Clientes.Domain.Entities.Client;
using System.ComponentModel.DataAnnotations;

namespace Cfa.Clientes.Domain.Entities.Address;

public class DireccionEntity
{
    public long DireccionID { get; set; } 
    public long CodigoCliente { get; set; } 
    public string Direccion { get; set; }
    public string TipoDireccion { get; set; } 

    public ClienteEntity Cliente { get; set; }
}