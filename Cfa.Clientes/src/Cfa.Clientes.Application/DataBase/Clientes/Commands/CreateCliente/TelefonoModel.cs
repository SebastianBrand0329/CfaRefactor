namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;

public class TelefonoModel
{
    public long TelefonoId { get; set; }  // Asegúrate de que sea long si corresponde a TelefonoEntity
    public long Telefono { get; set; }
    public long CodigoCliente { get; set; }
    public string TipoTelefono { get; set; } = null!;
}