using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;
using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.Models;

public class Transacao
{
    [Newtonsoft.Json.JsonIgnore]
    public int Id {get;set;}
    [Required]
    [Newtonsoft.Json.JsonIgnore]
    public int ClienteId {get;set;}
    [Required]
    [JsonProperty("valor")]
    public int Valor {get;set;}
    [Required]
    [JsonProperty("tipo")]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoTransacao Tipo {get;set;}
    [Required]
    [JsonProperty("descricao")]
    [Length(1,10)]
    public string Descricao {get;set;}
    [Newtonsoft.Json.JsonIgnore]
    public DateTime RealizadaEm {get;set;} = DateTime.Now;

}
