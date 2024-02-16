using Infrastructure;
using Infrastructure.Dapper;

namespace WebApi.Configurations;

public static class DatabaseConfiguration
{
    public static void AddDatabaseConfiguration(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("PostgreSQL");
        services.AddSingleton(_ => new DatabaseConfig{ConnectionString = connectionString});
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
    }
}
