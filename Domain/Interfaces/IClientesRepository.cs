

using Domain.Models;

namespace Domain;

public interface IClientesRepository : IDisposable
{
    Task<Cliente> GetByIdAsync(int id);

}
