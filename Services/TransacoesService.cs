using Domain;
using Domain.DTOs;
using Domain.Entities;
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

    public async Task<BaseResponse> NovaTransacao(NovaTransacao transacao)
    {
        Cliente cliente = (Cliente)await _clientesRepository.GetByIdAsync(transacao.ClienteId);
        if (cliente is null)
            throw new ClienteInexistenteException(transacao.ClienteId);
        (long saldo, long limite) = await _transacoesRepository.NovaTransacao(transacao.ToEntity());
        var transacaoDto = new TransacaoDto(limite, saldo);
        return transacaoDto;
    }
    public async Task<BaseResponse> Extrato(int id)
    {

        var cliente = (Cliente) await _clientesRepository.GetByIdAsync(id);
        if (cliente is null)
            throw new ClienteInexistenteException(id);
        var transacoes = await _transacoesRepository.GetTransacoesByIdCliente(id);
        SaldoDto saldo = new();
        saldo.FromCliente(cliente);
        var transacoesDto = new List<TransacaoDetailedDto>();

        transacoes.ForEach(t => transacoesDto.Add(new TransacaoDetailedDto(
            t.Valor,
            (TipoTransacao)Enum.Parse(typeof(TipoTransacao),t.Tipo),
            t.Descricao,
            t.RealizadaEm
        )));

        return new ExtratoDto
        {
            Saldo = saldo,
            UltimasTransacoes = transacoesDto
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
