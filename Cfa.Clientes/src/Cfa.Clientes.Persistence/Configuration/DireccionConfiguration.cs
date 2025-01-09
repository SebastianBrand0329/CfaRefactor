using Cfa.Clientes.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cfa.Clientes.Persistence.Configuration
{
    public class DireccionConfiguration
    {
        public DireccionConfiguration(EntityTypeBuilder<DireccionEntity> entityBuilder)
        {

            entityBuilder.HasKey(x => x.DireccionID);

          
            entityBuilder.Property(x => x.Direccion)
                .IsRequired() 
                .HasMaxLength(255); 

            // Configuración de la relación con ClienteEntity
            entityBuilder.HasOne(x => x.Cliente)
                .WithMany(c => c.Direcciones) 
                .HasForeignKey(x => x.CodigoCliente) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}