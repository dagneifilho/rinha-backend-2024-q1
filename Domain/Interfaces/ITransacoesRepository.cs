using Domain.Entities;

namespace Domain;

public interface ITransacoesRepository : IDisposable
{
    Task NovaTransacao(Transacao transacao);
    Task<List<Transacao>> GetTransacoesByIdCliente(int id);
    
}
