using System.Net;
using Domain.Exceptions;
using WebApi.Middlewares;

namespace WebApi.Midlewares;

public class ExceptionHandlerMiddleware : BasicMiddleware
{
    public ExceptionHandlerMiddleware(RequestDelegate next) : base(next){}

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {   
        string message;
        switch (exception)
        {
            case ClienteInexistenteException:
                var clienteInexistenteException = (ClienteInexistenteException)exception;
                message = $"O cliente com o id {clienteInexistenteException.Id} não existe!";
                Logger.LogInformation(message);
                return (HttpStatusCode.NotFound, message);
            case LimiteInsuficienteException:
                message = $"O cliente não possui limite suficiente para a transação";
                Logger.LogInformation(message);
                return (HttpStatusCode.UnprocessableEntity, message);
            default:
                message = "Ocorreu um erro no servidor: ";
                Logger.LogError(message + exception.Message);
                return (HttpStatusCode.InternalServerError, message);
        }
    }
}
