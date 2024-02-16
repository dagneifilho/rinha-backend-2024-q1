using Domain.Models;
using Newtonsoft.Json;

namespace Domain.DTOs;

public class SaldoDto
{
    public long Total {get;set;}
    public string DataExtrato {get;set;}
    public long Limite {get;set;}
    public SaldoDto()
    {

    }
    public void FromCliente(Cliente cliente)
    {
        Total = cliente.Saldo;
        Limite = cliente.Limite;
        DataExtrato = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
    }
}
