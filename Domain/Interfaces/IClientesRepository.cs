using Domain.Entities;

namespace Domain;

public interface IClientesRepository : IDisposable
{
    Task<BaseEntity> GetByIdAsync(int id);
    void Update(BaseEntity cliente);

}
