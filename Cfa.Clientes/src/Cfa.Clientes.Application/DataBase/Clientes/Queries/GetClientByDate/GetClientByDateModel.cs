namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDate;

public class GetClientByDateModel
{
    public DateTime FechaNacimiento { get; set; }
    public string Nombre { get; set; } = null!;
}