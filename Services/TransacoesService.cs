using Domain.DTOs;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Services;

public class TransacoesService : ITransacoesService
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public Task<BaseResponse> NovaTransacao(NovaTransacao transacao)
    {
        if(transacao.ClienteId == 1)
            throw new ClienteInexistenteException(transacao.ClienteId);
        throw new LimiteInsuficienteException();
    }
}
