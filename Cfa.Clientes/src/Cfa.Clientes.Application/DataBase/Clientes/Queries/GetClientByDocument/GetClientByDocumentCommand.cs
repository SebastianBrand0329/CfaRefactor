using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDocument;

public class GetClientByDocumentCommand : IGetClientByDocumentCommand
{
    private readonly IDataBaseService _service;

    public GetClientByDocumentCommand(IDataBaseService service)
    {
        _service = service;
    }

    public async Task<List<GetClientByDocumentModel>> Execute()
    {
        var client = await _service.Clientes.OrderByDescending(x => x.NumeroDocumento)
                                            .Select(c => new GetClientByDocumentModel
                                            {
                                                NumeroDocumento = c.NumeroDocumento,
                                                Nombres = $"{c.Nombres} {c.Apellido1} {c.Apellido2}"
                                            }).ToListAsync();

        return client;
    }
}