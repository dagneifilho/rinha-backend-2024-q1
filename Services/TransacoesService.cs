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
    private readonly IUnitOfWork _unitOfWork;
    public TransacoesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> NovaTransacao(NovaTransacao transacao)
    {
        Cliente cliente = (Cliente)await _unitOfWork.ClientesRepository.GetByIdAsync(transacao.ClienteId);
        if (cliente is null)
            throw new ClienteInexistenteException(transacao.ClienteId);
        if(cliente.Saldo - transacao.Valor < cliente.Limite*(-1))
            throw new LimiteInsuficienteException();
        var transacaoDb = new Transacao
        {
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            Descricao = transacao.Descricao,
            RealizadaEm = DateTime.Now,
            Cliente = cliente
        };
        EfetuaTransacao(cliente, transacaoDb);
        await _unitOfWork.TransacoesRepository.NovaTransacao(transacaoDb);
        await _unitOfWork.Commit();
        var transacaoDto = new TransacaoDto(cliente.Limite, cliente.Saldo);
        return transacaoDto;
    }
    public void EfetuaTransacao(Cliente cliente, Transacao transacao)
    {
        switch(transacao.Tipo)
        {
            case TipoTransacao.d:
                cliente.Saldo -= transacao.Valor;
                break;
            default:
                cliente.Saldo += transacao.Valor;
                break;
        }

    }

    public async Task<BaseResponse> Extrato(int id)
    {

        var cliente = (Cliente) await _unitOfWork.ClientesRepository.GetByIdAsync(id);
        if (cliente is null)
            throw new ClienteInexistenteException(id);
        var transacoes = await _unitOfWork.TransacoesRepository.GetTransacoesByIdCliente(id);
        SaldoDto saldo = new();
        saldo.FromCliente(cliente);
        var transacoesDto = new List<TransacaoDetailedDto>();

        transacoes.ForEach(t => transacoesDto.Add(new TransacaoDetailedDto(
            t.Valor,
            t.Tipo,
            t.Descricao,
            t.RealizadaEm
        )));

        return new ExtratoDto
        {
            Saldo = saldo,
            UltimasTransacoes = transacoesDto
        };
    } 

    public void Dispose()
    {
        _unitOfWork.Dispose();
        GC.SuppressFinalize(this);
    }
}
