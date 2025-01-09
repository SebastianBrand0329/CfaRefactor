using Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;

namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.DeleteClient;

public interface IDeleteClientModel
{
    Task<bool> Execute(DeleteClientModel model);
}