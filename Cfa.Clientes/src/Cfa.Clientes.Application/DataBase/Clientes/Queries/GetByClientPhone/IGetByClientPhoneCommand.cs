namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientPhone;

public interface IGetByClientPhoneCommand
{
    Task<List<GetByClientPhoneModel>> Execute();
}