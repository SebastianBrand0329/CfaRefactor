namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDate;

public  interface IGetClientByDateCommand
{
    Task<List<GetClientByDateModel>> Execute(GetClientByDateModelInput getClient);
}