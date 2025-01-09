namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetAllClientFilter;

public interface IGetAllClientFilterQuery
{
    Task<List<GetAllClientFilterModel>> Execute(string Search);
}