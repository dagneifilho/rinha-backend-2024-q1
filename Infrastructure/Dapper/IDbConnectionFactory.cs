using System.Data;

namespace Infrastructure.Dapper;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
