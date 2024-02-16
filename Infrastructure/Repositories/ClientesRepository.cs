using System.Data;
using Dapper;
using Domain;
using Domain.Models;
using Infrastructure.Dapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ClientesRepository : IClientesRepository
{
    private readonly IDbConnection _connection;
    public ClientesRepository(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
    }

    public async Task<Cliente> GetByIdAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
        var query = "SELECT \"Id\", \"Limite\", \"Saldo\" FROM \"Clientes\" WHERE \"Id\" = @id;";
        return await _connection.QueryFirstOrDefaultAsync<Cliente>(query, parameters);
    }

    protected virtual void Dispose(bool disposing)
    {
        _connection.Dispose();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
