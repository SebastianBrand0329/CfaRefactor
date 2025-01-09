namespace Cfa.Clientes.Application.DataBase.Clientes.Commands.UpdateCliente;

public interface IUpdateClientCommand
{
    Task<bool> Execute(UpdateClientModel model);
}