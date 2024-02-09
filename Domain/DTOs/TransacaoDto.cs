using System.Numerics;
using Domain.DTOs;
using Domain.Entities;
using Newtonsoft.Json;

namespace Domain;

public class TransacaoDto : BaseResponse
{
    public long Limite {get;set;}
    public long Saldo {get;set;}
    public TransacaoDto(long limite, long saldo )
    {
        Limite = limite;
        Saldo = saldo;
    }

}
