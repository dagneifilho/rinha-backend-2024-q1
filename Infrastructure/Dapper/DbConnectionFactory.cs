using System.Data;
using Npgsql;

namespace Infrastructure.Dapper;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly DatabaseConfig _config;
    public DbConnectionFactory(DatabaseConfig config)
    {
        _config = config;
    }
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_config.ConnectionString);
    }
}
