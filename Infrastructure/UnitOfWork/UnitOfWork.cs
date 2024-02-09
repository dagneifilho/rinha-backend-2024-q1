using Azure.Identity;
using Domain;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IClientesRepository clientesRepository;
    private ITransacoesRepository transacoesRepository;
    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }
    public ITransacoesRepository TransacoesRepository {
        get 
        {
            if (this.transacoesRepository is null)
                this.transacoesRepository = new TransacoesRepository(_context);
            return this.transacoesRepository;
        }
    }

    public IClientesRepository ClientesRepository 
    {
        get 
        {
            if (this.clientesRepository is null)
                this.clientesRepository = new ClientesRepository(_context);
            return this.clientesRepository;
        }
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    protected void Dispose(bool disposing) 
    {
        TransacoesRepository.Dispose();
        ClientesRepository.Dispose();
        _context.Dispose();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
