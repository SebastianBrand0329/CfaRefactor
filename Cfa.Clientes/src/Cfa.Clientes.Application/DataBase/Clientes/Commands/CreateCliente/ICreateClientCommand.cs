namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;

public interface ICreateClientCommand
{
    Task<CreateClientModel> Execute(CreateClientModel model);
}