using Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;
using Cfa.Clientes.Application.DataBase.Clientes.Commands.DeleteClient;
using Cfa.Clientes.Application.DataBase.Clientes.Commands.UpdateCliente;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetAllClientFilter;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientAddress;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientPhone;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDate;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDocument;
using Cfa.Clientes.Application.Exceptions;
using Cfa.Clientes.Application.Features;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cfa.Clientes.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[TypeFilter(typeof(ExceptionManager))]
public class ClientesController : ControllerBase
{
    // Se debe validar al momento de guardar la información del cliente, que el tipo y número de documento, no se encuentre registrado en base de datos, en caso de ser así, no se debe permitir registrar la información en el sistema

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateClientModel model, [FromServices] ICreateClientCommand createClient, [FromServices] IValidator<CreateClientModel> validator)
    {
        var validate = await validator.ValidateAsync(model);

        if (!validate.IsValid)
        {

            return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService<CreateClientModel>.Response(StatusCodes.Status400BadRequest, validate.Errors));

        }

        var data = await createClient.Execute(model);

        return StatusCode(StatusCodes.Status201Created, ResponseApiService<CreateClientModel>.Response(StatusCodes.Status201Created, data));
    }

    // Se debe permitir actualizar la información recopilada de los clientes a excepción del código, para ello se requiere cambiar la fecha de nacimiento, el tipo y número de documento

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateClientModel model, [FromServices] IUpdateClientCommand updateClient, [FromServices] IValidator<UpdateClientModel> validator)
    {
        var validate = await validator.ValidateAsync(model);

        if (!validate.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));

        var data = await updateClient.Execute(model);

        return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
    }

    // Se debe permitir eliminar el cliente y toda la información registrada del mismo de la base de datos

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteClientModel model, [FromServices] IDeleteClientModel deleteClient, [FromServices] IValidator<DeleteClientModel> validator)
    {
        var validate = await validator.ValidateAsync(model);

        if (!validate.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));

        var data = await deleteClient.Execute(model);

        return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
    }

    // Consultar los clientes por su nombre completo o parte de su nombre y devolver los resultados de la búsqueda en orden ascendente (A a Z).

    [HttpGet("getbyfilter")]
    public async Task<IActionResult> GetByFilter([FromBody] string search, [FromServices] IGetAllClientFilterQuery filterQuery)
    {
        var data = await filterQuery.Execute(search);

        if (data == null || data.Count == 0)
            return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

        return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
    }

    // Consultar los clientes por número de documento, el resultado de la búsqueda debe organizarse de mayor a menor, mostrando el número de documento y nombre completo del cliente

    [HttpGet("getbyDocument")]
    public async Task<IActionResult> GetByDocument([FromServices] IGetClientByDocumentCommand getClient)
    {
        var data = await getClient.Execute();

        if (data == null || data.Count == 0)
            return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

        return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
    }

    // Consultar los clientes por fecha de nacimiento, de acuerdo a la elección de un rango de fechas, es decir debe permitir elegir una fecha inicial y final de consulta,  el resultado debe permitir visualizar los clientes de acuerdo a la fecha de nacimiento más antigua a la más reciente , seguida del nombre completo del cliente

    [HttpGet("getbyDate")]
    public async Task<IActionResult> GetbyDate([FromBody] GetClientByDateModelInput getClientByDate, [FromServices] IGetClientByDateCommand getClient, [FromServices] IValidator<GetClientByDateModelInput> validator)
    {
        var validate = await validator.ValidateAsync(getClientByDate);

        if (!validate.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));

        var data = await getClient.Execute(getClientByDate);

        if (data == null || data.Count == 0)
            return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

        return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
    }

    // Consultar los clientes que tienen más de un teléfono, el resultado de la búsqueda debe retornar el nombre completo del cliente y la cantidad de números registrados en base de datos.

    [HttpGet("getbyClientPhone")]
    public async Task<IActionResult> getbyClientPhone([FromServices] IGetByClientPhoneCommand getByClient)
    {
        var data = await getByClient.Execute();

        if (data == null || data.Count == 0)
            return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

        return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
    }

    // Consultar los clientes que tienen más de una dirección registrada, el resultado a mostrar debe devolver la primera dirección, seguida del nombre completo por cada cliente.

    [HttpGet("getbyClientAddress")]
    public async Task<IActionResult> getbyClientAddress([FromServices] IGetByClientAddressCommand getByClient)
    {
        var data = await getByClient.Execute();

        if (data == null || data.Count == 0)
            return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound));

        return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
    }
}