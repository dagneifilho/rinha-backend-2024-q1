using Domain.DTOs;
using Domain.Models;

namespace Domain.Interfaces;

public interface ITransacoesService : IDisposable
{
    Task<BaseResponse> NovaTransacao(NovaTransacao transacao);
    Task<BaseResponse> Extrato(int id);
}
