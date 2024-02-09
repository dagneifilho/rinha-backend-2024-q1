namespace Domain.Interfaces;
public interface IUnitOfWork:IDisposable
{
    ITransacoesRepository TransacoesRepository {get;}
    IClientesRepository ClientesRepository {get;}
    Task Commit();
}
