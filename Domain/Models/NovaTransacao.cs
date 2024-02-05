using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.Models;

public class NovaTransacao
{
    [Required]
    public int ClienteId {get;set;}
    [Required]
    [JsonProperty("valor")]
    public Int64 Valor {get;set;}
    [Required]
    [JsonProperty("tipo")]
    public TipoTransacao Tipo {get;set;}
    [Required]
    [JsonProperty("descricao")]
    public string Descricao {get;set;}
}
