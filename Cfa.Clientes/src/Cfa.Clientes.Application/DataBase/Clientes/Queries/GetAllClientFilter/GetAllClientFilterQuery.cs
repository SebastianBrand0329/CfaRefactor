using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetAllClientFilter;

public class GetAllClientFilterQuery : IGetAllClientFilterQuery
{
    private readonly IDataBaseService _service;

    public GetAllClientFilterQuery(IDataBaseService service)
    {
        _service = service;
    }

    public async Task<List<GetAllClientFilterModel>> Execute(string Search)
    {
        var result = await (from client in _service.Clientes
                            where client.Nombres.Contains(Search)
                            orderby client.Nombres ascending
                            select new GetAllClientFilterModel
                            {
                                Codigo = client.Codigo,
                                TipoDocumento = client.TipoDocumento,
                                NumeroDocumento = client.NumeroDocumento,
                                Nombres = client.Nombres,
                                Apellido1 = client.Apellido1,
                                Apellido2 = client.Apellido2,
                                Genero = client.Genero,
                                FechaNacimiento = client.FechaNacimiento,
                                Email = client.Email,
                                Telefonos = _service.Telefonos
                                    .Where(t => t.CodigoCliente == client.Codigo)
                                    .Select(t => new GetTelefonoModel
                                    {
                                        TelefonoId = t.TelefonoID,
                                        Telefono = t.Telefono,
                                        CodigoCliente = t.CodigoCliente,
                                        TipoTelefono = t.TipoTelefono
                                    }).ToList(),
                                Direcciones = _service.Direcciones
                                    .Where(d => d.CodigoCliente == client.Codigo)
                                    .Select(d => new GetDireccionModel
                                    {
                                        DireccionId = d.DireccionID,
                                        Direccion = d.Direccion,
                                        CodigoCliente = d.CodigoCliente,
                                        TipoDireccion = d.TipoDireccion
                                    }).ToList()
                            }).ToListAsync();

        return result;
    }
}