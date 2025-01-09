using Cfa.Clientes.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cfa.Clientes.Persistence.Configuration;

public class ClienteConfiguration
{
    public ClienteConfiguration(EntityTypeBuilder<ClienteEntity> entityBuilder)
    {
   
        entityBuilder.HasKey(x => x.Codigo);


        entityBuilder.Property(x => x.TipoDocumento)
            .IsRequired(); 

        entityBuilder.Property(x => x.NumeroDocumento)
            .IsRequired() 
            .HasMaxLength(11); 

        entityBuilder.Property(x => x.Nombres)
            .IsRequired() 
            .HasMaxLength(30); 

        entityBuilder.Property(x => x.Apellido1)
            .IsRequired() 
            .HasMaxLength(30); 

        entityBuilder.Property(x => x.Apellido2)
            .HasMaxLength(30); 

        entityBuilder.Property(x => x.Genero)
            .IsRequired(); 

        entityBuilder.Property(x => x.FechaNacimiento)
            .IsRequired();


        // Configuración de la relación con Direcciones
        entityBuilder.HasMany(x => x.Direcciones)
            .WithOne(d => d.Cliente) 
            .HasForeignKey(d => d.CodigoCliente) 
            .OnDelete(DeleteBehavior.Cascade); 

        // Configuración de la relación con Teléfonos
        entityBuilder.HasMany(x => x.Telefonos)
            .WithOne(t => t.Cliente) 
            .HasForeignKey(t => t.CodigoCliente) 
            .OnDelete(DeleteBehavior.Cascade); 

    }
}