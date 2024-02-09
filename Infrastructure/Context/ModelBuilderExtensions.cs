using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente{Id  = 1, Limite = 100000, Saldo = 0},
            new Cliente{Id = 2, Limite = 80000, Saldo = 0},
            new Cliente{Id = 3, Limite = 1000000, Saldo = 0},
            new Cliente{Id = 4, Limite = 10000000, Saldo = 0},
            new Cliente{Id = 5, Limite = 500000, Saldo = 0}
        );
    }
}
