using System.Text.Json.Serialization;
using Domain.DTOs;
using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.DTOs;

public class TransacaoDetailedDto
{
    public long Valor {get;set;}
    public TipoTransacao Tipo {get;set;}
    public string Descricao {get;set;}
    public string RealizadaEm {get;set;}
    public TransacaoDetailedDto() 
    {

    }
    public TransacaoDetailedDto(long valor, TipoTransacao tipo, string descricao, DateTime realizadaEm)
    {   
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;
        RealizadaEm = realizadaEm.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
    }

}
