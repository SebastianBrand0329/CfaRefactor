using Cfa.Clientes.Domain.Entities.Phone;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cfa.Clientes.Persistence.Configuration
{
    public class TelefonoConfiguration
    {
        public TelefonoConfiguration(EntityTypeBuilder<TelefonoEntity> entityBuilder)
        {
            entityBuilder.HasKey(x => x.TelefonoID);

     
            entityBuilder.Property(x => x.Telefono)
                .IsRequired() 
                .HasMaxLength(15) 
                .IsUnicode(false); 

            // Configuración de la relación con ClienteEntity
            entityBuilder.HasOne(x => x.Cliente)
                .WithMany(c => c.Telefonos) 
                .HasForeignKey(x => x.CodigoCliente) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}