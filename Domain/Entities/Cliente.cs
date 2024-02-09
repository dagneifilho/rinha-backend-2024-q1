namespace Domain.Entities;

public class Cliente : BaseEntity
{
    public long Limite {get;set;}
    public long Saldo {get;set;}
    public List<Transacao> Transacoes {get;set;}
}
