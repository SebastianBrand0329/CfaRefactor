using AutoMapper;
using Cfa.Clientes.Application.Configuration;
using Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;
using Cfa.Clientes.Application.DataBase.Clientes.Commands.DeleteClient;
using Cfa.Clientes.Application.DataBase.Clientes.Commands.UpdateCliente;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetAllClientFilter;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientAddress;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetByClientPhone;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDate;
using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDocument;
using Cfa.Clientes.Application.Validators.Cliente;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cfa.Clientes.Application;

public static class DependencyInjectionService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new MapperProfile());
        });


        services.AddSingleton(mapper.CreateMapper());

        #region Client 
        services.AddTransient<ICreateClientCommand, CreateClientCommand>();
        services.AddTransient<IUpdateClientCommand, UpdateClientCommand>();
        services.AddTransient<IDeleteClientModel, DeleteClientCommand>();
        services.AddTransient<IGetAllClientFilterQuery, GetAllClientFilterQuery>();
        services.AddTransient<IGetClientByDocumentCommand, GetClientByDocumentCommand>();
        services.AddTransient<IGetClientByDateCommand, GetClientByDateCommand>();
        services.AddTransient<IGetByClientPhoneCommand, GetByClientPhoneCommand>();
        services.AddTransient<IGetByClientAddressCommand, GetByClientAddressCommand>();
        #endregion

        #region Validator
        services.AddScoped<IValidator<CreateClientModel>, ClienteValidator>();
        services.AddScoped<IValidator<UpdateClientModel>, UpdateClientValidator>();
        services.AddScoped<IValidator<DeleteClientModel>, DeleteClientValidator>();
        services.AddScoped<IValidator<GetClientByDateModelInput>, GetByDateValidator>();

        #endregion


        return services;
    }
}