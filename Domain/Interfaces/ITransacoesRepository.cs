using Domain.Entities;

namespace Domain;

public interface ITransacoesRepository : IDisposable
{
    Task<(long, long)> NovaTransacao(Transacao transacao);
    Task<List<Transacao>> GetTransacoesByIdCliente(int id);
    
}
