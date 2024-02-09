using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ClientesRepository : IClientesRepository
{
    private readonly DatabaseContext _context;
    public ClientesRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<BaseEntity> GetByIdAsync(int id)
    {
        return await _context.Clientes.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public void Update(BaseEntity cliente)
    {
        _context.Clientes.Update((Cliente)cliente);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
