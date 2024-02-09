using Domain.Enums;

namespace Domain.Entities;

public class Transacao : BaseEntity
{
    public long Valor {get;set;}
    public TipoTransacao Tipo {get;set;}
    public string Descricao {get;set;}
    public DateTime RealizadaEm {get;set;}
    public Cliente Cliente {get;set;}


    
}
