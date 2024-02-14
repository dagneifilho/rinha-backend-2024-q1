using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

public class ClientesMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Transacoes).WithOne(t => t.Cliente);

        builder.Property(c => c.Id).HasColumnName("Id").HasColumnType("int").ValueGeneratedOnAdd();
        builder.Property(c => c.Saldo).HasColumnName("Saldo").HasColumnType("bigint");
        builder.Property(c => c.Limite).HasColumnName("Limite").HasColumnType("bigint");
    }
}
