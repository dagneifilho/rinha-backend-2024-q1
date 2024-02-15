using Domain.Enums;

namespace Domain.Entities;

public class Transacao : BaseEntity
{
    public long Valor {get;set;}
    public string Tipo {get;set;}
    public string Descricao {get;set;}
    public DateTime RealizadaEm {get;set;}
    public int ClienteId {get;set;}
}
