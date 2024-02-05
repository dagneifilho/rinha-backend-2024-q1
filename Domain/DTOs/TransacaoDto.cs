using System.Numerics;
using Domain.DTOs;
using Newtonsoft.Json;

namespace Domain;

public class TransacaoDto : BaseResponse
{
    [JsonProperty("limite")]
    public Int128 Limite {get;set;}
    [JsonProperty("saldo")]
    public Int128 Saldo {get;set;}
}
