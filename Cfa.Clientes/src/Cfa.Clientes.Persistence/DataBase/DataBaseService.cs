using Cfa.Clientes.Application.DataBase;
using Cfa.Clientes.Domain.Entities.Address;
using Cfa.Clientes.Domain.Entities.Client;
using Cfa.Clientes.Domain.Entities.Phone;
using Cfa.Clientes.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cfa.Clientes.Persistence.DataBase;

public class DataBaseService : DbContext, IDataBaseService
{
    public DataBaseService(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<ClienteEntity> Clientes { get; set; }
    public DbSet<DireccionEntity> Direcciones { get; set; }
    public DbSet<TelefonoEntity> Telefonos { get; set; }

    public async Task<bool> SaveAsync()
    {
        return await SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        EntityConfiguation(modelBuilder);
    }

    private void EntityConfiguation(ModelBuilder modelBuilder)
    {
        new ClienteConfiguration(modelBuilder.Entity<ClienteEntity>());
        new DireccionConfiguration(modelBuilder.Entity<DireccionEntity>());
        new TelefonoConfiguration(modelBuilder.Entity<TelefonoEntity>());
    }

}