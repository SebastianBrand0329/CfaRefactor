using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientAddress;

public class GetByClientAddressCommand : IGetByClientAddressCommand
{
    private readonly IConfiguration _configuration;

    public GetByClientAddressCommand(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<GetByClientAddressModel>> Execute()
    {
        SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
        List<GetByClientAddressModel> clients = new();
        try
        {
            sqlConnection.Open();
            var param = new DynamicParameters();
            var result = await sqlConnection.QueryAsync<GetByClientAddressModel>("[ConsultarClientesDirecciones]", commandType: CommandType.StoredProcedure);
            clients = result.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Se produjo un error al obtener los clientes " + ex.Message);
        }
        finally
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        return clients;
    }
}