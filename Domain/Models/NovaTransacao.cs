using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.Models;

public class NovaTransacao
{
    [Required]
    public int ClienteId {get;set;}
    [Required]
    [JsonProperty("valor")]
    public long Valor {get;set;}
    [Required]
    [JsonProperty("tipo")]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoTransacao Tipo {get;set;}
    [Required]
    [JsonProperty("descricao")]
    [Length(1,10)]
    public string Descricao {get;set;}

    public Transacao ToEntity()
    {
        return new Transacao 
        {
            Valor = this.Valor,
            Tipo = Tipo.ToString(),
            Descricao = this.Descricao,
            RealizadaEm = DateTime.Now,
            ClienteId = ClienteId
        
        };
    }
}
