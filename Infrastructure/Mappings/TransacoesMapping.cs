using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

public class TransacoesMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes",schema:"dbo");
        builder.HasOne(t => t.Cliente).WithMany(c => c.Transacoes).OnDelete(DeleteBehavior.ClientCascade);

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("Id").HasColumnType("int").ValueGeneratedOnAdd();
        builder.Property(t => t.Tipo).HasColumnName("Tipo").HasColumnType("char").IsRequired(true);
        builder.Property(t => t.RealizadaEm).HasColumnName("RealizadaEm").HasColumnType("datetime").IsRequired(true);            
    }
}
