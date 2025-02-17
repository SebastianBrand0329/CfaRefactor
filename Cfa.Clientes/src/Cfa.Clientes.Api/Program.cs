using Cfa.Clientes.Api;
using Cfa.Clientes.Application;
using Cfa.Clientes.Persistence;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddWebApi()
                .AddApplication()
                .AddPersistence(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
