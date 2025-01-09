using AutoMapper;
using Cfa.Clientes.Application.Helpers.ConverToDate;
using Cfa.Clientes.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;

public class CreateClientCommand : ICreateClientCommand
{
    private readonly IDataBaseService _service;
    private readonly IMapper _mapper;

    public CreateClientCommand(IDataBaseService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<CreateClientModel> Execute(CreateClientModel model)
    {
        var client = await ValidateClient(model);

        if (!client)
        {
            model.FechaNacimiento = ConvertToDate.ConvertToDates(model.FechaNacimiento);
            var entity = _mapper.Map<ClienteEntity>(model);
            await _service.Clientes.AddAsync(entity);
            await _service.Direcciones.AddRangeAsync(entity.Direcciones);
            await _service.Telefonos.AddRangeAsync(entity.Telefonos);
            await _service.SaveAsync();
            return model;
        }

        throw new Exception("El tipo y número de documento ya están registrados.");
    }

    private async Task<bool> ValidateClient(CreateClientModel model)
    {
        var client = await _service.Clientes.FirstOrDefaultAsync(x => x.NumeroDocumento == model.NumeroDocumento &&
                                                                 x.TipoDocumento == model.TipoDocumento);
        if (client is null)
            return false;
        return true;
    }
}