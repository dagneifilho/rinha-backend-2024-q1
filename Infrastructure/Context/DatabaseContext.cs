using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<Transacao> Transacoes {get;set;}
    public DbSet<Cliente> Clientes {get;set;}
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Transacao>(new TransacoesMapping());
        modelBuilder.ApplyConfiguration<Cliente>(new ClientesMapping());
        modelBuilder.Seed();
    }

}
