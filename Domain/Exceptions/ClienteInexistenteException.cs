namespace Domain.Exceptions;

public class ClienteInexistenteException : Exception
{
    public int Id {get;set;}
    public ClienteInexistenteException(int id) 
    {
        Id = id;
    }
}
