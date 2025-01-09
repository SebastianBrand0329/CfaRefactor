namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetAllClientFilter;

public class GetAllClientFilterModel
{
    public long Codigo { get; set; }
    public string TipoDocumento { get; set; } = null!;
    public long NumeroDocumento { get; set; }
    public string Nombres { get; set; } = null!;
    public string Apellido1 { get; set; } = null!;
    public string Apellido2 { get; set; } = null!;
    public string Genero { get; set; } = null!;
    public DateTime FechaNacimiento { get; set; }
    public string Email { get; set; } = null!;
    public List<GetTelefonoModel> Telefonos { get; set; } = null!;
    public List<GetDireccionModel> Direcciones { get; set; } = null!;
}