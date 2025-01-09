namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.UpdateCliente;

public class UpdateClientModel
{
    public long Codigo { get; set; }
    public string TipoDocumento { get; set; } = null!;
    public long NumeroDocumento { get; set; }
    public DateTime FechaNacimiento { get; set; }
}