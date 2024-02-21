using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;
using Domain.Enums;
using Newtonsoft.Json;

namespace Domain.Models;

public class Transacao : IValidatableObject
{
    [Newtonsoft.Json.JsonIgnore]
    public int Id {get;set;}
    [Required]
    [Newtonsoft.Json.JsonIgnore]
    public int ClienteId {get;set;}
    [Required]
    [JsonProperty("valor")]
    public int? Valor {get;set;}
    [Required]
    [JsonProperty("tipo")]
    public string Tipo {get;set;}
    [Required]
    [JsonProperty("descricao")]
    [Length(1,10)]
    public string Descricao {get;set;}
    [Newtonsoft.Json.JsonIgnore]
    public DateTime RealizadaEm {get;set;} = DateTime.Now;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(string.IsNullOrEmpty(Tipo) || !Enum.TryParse(typeof(TipoTransacao), Tipo, out _))
            yield return new ValidationResult("The field 'tipo' is required and must be c or d", new string[]{"tipo"});
    }
}
