using System.Data;
using System.Diagnostics.Eventing.Reader;
using Dapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Dapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransacoesRepository : ITransacoesRepository
{
    private readonly IDbConnection _connection;
    public TransacoesRepository(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();
    }


    public async Task<List<Transacao>> GetTransacoesByIdCliente(int id)
    {
        DynamicParameters parameters = new DynamicParameters();
        var query = " select \"Id\",\"Valor\", \"Descricao\",\"Tipo\", \"RealizadaEm\" from \"Transacoes\" t  where \"ClienteId\" = @id ";
        parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
        return (await _connection.QueryAsync<Transacao>(query, parameters)).ToList();
    }
    public async Task<(long, long)> NovaTransacao(Transacao transacao)
    {
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", transacao.ClienteId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@valor", transacao.Valor, DbType.Int64,ParameterDirection.Input);
            parameters.Add("@tipo", transacao.Tipo, DbType.String,ParameterDirection.Input);
            parameters.Add("@descricao", transacao.Descricao, DbType.String, ParameterDirection.Input);
            parameters.Add("@realizadaEm", transacao.RealizadaEm, DbType.Date, ParameterDirection.Input);
            var query = @"SELECT realizar_transacao(@id,@valor,@tipo,@descricao,@realizadaEm);";
            var obj = await _connection.QuerySingleAsync<Record>(query,parameters);
            var result = obj.realizar_transacao;
            var saldo = (long)result[0];
            var limite = (long)result[1];
            return (saldo, limite);
        }
        catch (Exception ex)
        {
            if(ex.Message.Contains("limite insuficiente"))
                throw new LimiteInsuficienteException();
            throw new Exception(ex.Message);
        }        
    }

    private class Record
    {
        public Object[] realizar_transacao {get;set;}
    }

    protected virtual void Dispose(bool disposing)
    {
        _connection.Dispose();
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }


}
