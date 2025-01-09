namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDocument;

public interface IGetClientByDocumentCommand
{
    Task<List<GetClientByDocumentModel>> Execute();
}