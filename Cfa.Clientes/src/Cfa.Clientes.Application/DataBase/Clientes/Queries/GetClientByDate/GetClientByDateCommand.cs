using Cfa.Clientes.Application.Helpers.ConverToDate;
using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDate;

public class GetClientByDateCommand : IGetClientByDateCommand
{
    private readonly IDataBaseService _service;

    public GetClientByDateCommand(IDataBaseService service)
    {
        _service = service;
    }

    public async Task<List<GetClientByDateModel>> Execute(GetClientByDateModelInput getClient)
    {
        var client = await _service.Clientes.Where(x => x.FechaNacimiento >= getClient.FechaInicial && x.FechaNacimiento <= getClient.FechaFinal)
                                            .OrderBy(x => x.FechaNacimiento)
                                            .Select(x => new GetClientByDateModel
                                            {
                                                FechaNacimiento = x.FechaNacimiento,
                                                Nombre = $"{x.Nombres} {x.Apellido1} {x.Apellido2}"
                                            }).ToListAsync();

        client.ForEach(x => x.FechaNacimiento = ConvertToDate.ConvertToDates(x.FechaNacimiento));

        return client;
    }
}