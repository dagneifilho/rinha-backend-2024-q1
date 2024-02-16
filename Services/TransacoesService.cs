using Domain;
using Domain.DTOs;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Services;

public class TransacoesService : ITransacoesService
{
    private readonly ITransacoesRepository _transacoesRepository;
    private readonly IClientesRepository _clientesRepository;
    public TransacoesService(ITransacoesRepository transacoesRepository, IClientesRepository clientesRepository)
    {
        _transacoesRepository = transacoesRepository;
        _clientesRepository = clientesRepository;
    }

    public async Task<BaseResponse> NovaTransacao(Transacao transacao)
    {
        (long saldo, long limite) = await _transacoesRepository.NovaTransacao(transacao);
        var transacaoDto = new TransacaoDto(limite, saldo);
        return transacaoDto;
    }
    public async Task<BaseResponse> Extrato(int id)
    {
        var cliente = await _clientesRepository.GetByIdAsync(id);
        if (cliente is null)
            throw new ClienteInexistenteException(id);
        var transacoes = await _transacoesRepository.GetTransacoesByIdCliente(id);
        return new ExtratoDto
        {
            Saldo = new SaldoDto{Total = cliente.Saldo, Limite = cliente.Limite, DataExtrato = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ")},
            UltimasTransacoes = transacoes.Select(t => new TransacaoDetailedDto(t))
        };
    } 

    protected virtual void Dispose(bool disposing)
    {
        _clientesRepository.Dispose();
        _transacoesRepository.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
