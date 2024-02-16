
using Domain.Models;

namespace Domain;

public interface ITransacoesRepository : IDisposable
{
    Task<(int, int)> NovaTransacao(Transacao transacao);
    Task<List<Transacao>> GetTransacoesByIdCliente(int id);
    
}
