using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientPhone;

public class GetByClientPhoneCommand : IGetByClientPhoneCommand
{
    private readonly IDataBaseService _service;

    public GetByClientPhoneCommand(IDataBaseService service)
    {
        _service = service;
    }

    public async Task<List<GetByClientPhoneModel>> Execute()
    {
        var client = await _service.Clientes.Where(x => x.Telefonos.Count > 1)
                                            .Select(x => new GetByClientPhoneModel
                                            {
                                                Nombre = $"{x.Nombres} {x.Apellido1} {x.Apellido2}",
                                                CantidadTelefonos = x.Telefonos.Count
                                            }).ToListAsync();

        return client;
    }
}