using System.Text.Json.Serialization;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.DTOs;

public class TransacaoDetailedDto
{
    public int Valor {get;set;}
    public TipoTransacao Tipo {get;set;}
    public string Descricao {get;set;}
    public string RealizadaEm {get;set;}
    public TransacaoDetailedDto() 
    {

    }
    public TransacaoDetailedDto(Transacao transacao)
    {   
        Valor = transacao.Valor;
        Tipo = transacao.Tipo;
        Descricao = transacao.Descricao;
        RealizadaEm = transacao.RealizadaEm.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
    }

}
