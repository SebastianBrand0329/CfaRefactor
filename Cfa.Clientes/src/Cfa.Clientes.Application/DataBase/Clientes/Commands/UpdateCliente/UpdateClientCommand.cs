using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.UpdateCliente;

public class UpdateClientCommand : IUpdateClientCommand
{
    private readonly IDataBaseService _service;
    private readonly IMapper _mapper;

    public UpdateClientCommand(IDataBaseService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<bool> Execute(UpdateClientModel model)
    {
        var client = await _service.Clientes.FirstOrDefaultAsync(x => x.Codigo == model.Codigo);

        if (client is null)
            return false;

        var isDocumentExists = await _service.Clientes.AnyAsync(x => x.NumeroDocumento == model.NumeroDocumento && x.Codigo != model.Codigo);

        if (isDocumentExists)
            return false;

        client.TipoDocumento = model.TipoDocumento;
        client.NumeroDocumento = model.NumeroDocumento;
        client.FechaNacimiento = model.FechaNacimiento;

        return await _service.SaveAsync();
    }
}