using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.DeleteClient;

public class DeleteClientCommand : IDeleteClientModel
{
    private readonly IDataBaseService _service;

    public DeleteClientCommand(IDataBaseService service)
    {
        _service = service;
    }

    public async Task<bool> Execute(DeleteClientModel model)
    {
        var client = await _service.Clientes.Include(x => x.Telefonos)
                                            .Include(x => x.Direcciones)
                                            .FirstOrDefaultAsync(x => x.Codigo == model.Codigo);

        if (client is null)
            return false;

        _service.Telefonos.RemoveRange(client.Telefonos); 
        _service.Direcciones.RemoveRange(client.Direcciones);
        
        _service.Clientes.Remove(client);

        return await _service.SaveAsync();

    }
}