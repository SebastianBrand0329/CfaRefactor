namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientAddress;

public interface IGetByClientAddressCommand
{
    Task<List<GetByClientAddressModel>> Execute();
}