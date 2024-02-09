using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransacoesRepository : ITransacoesRepository
{
    private readonly DatabaseContext _context;
    public TransacoesRepository(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<List<Transacao>> GetTransacoesByIdCliente(int id)
    {
        return await _context.Transacoes.AsNoTracking().Where(t => t.Cliente.Id.Equals(id)).OrderByDescending(t => t.RealizadaEm).ToListAsync();
    }

    public async Task NovaTransacao(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
