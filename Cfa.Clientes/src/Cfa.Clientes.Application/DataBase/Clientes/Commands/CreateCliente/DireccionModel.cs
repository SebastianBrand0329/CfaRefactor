namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;

public class DireccionModel
{
    public long DireccionId { get; set; }  // Asegúrate de que sea long si corresponde a DireccionEntity
    public string Direccion { get; set; } = null!;
    public long CodigoCliente { get; set; }
    public string TipoDireccion { get; set; } = null!;
}