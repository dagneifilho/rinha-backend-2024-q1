using System.Net;
using Domain.Exceptions;
using WebApi.Middlewares;

namespace WebApi.Midlewares;

public class ExceptionHandlerMiddleware : BasicMiddleware
{
    private readonly string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    public ExceptionHandlerMiddleware(RequestDelegate next) : base(next){}

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {   
        string message;
        switch (exception)
        {
            case ClienteInexistenteException:
                var clienteInexistenteException = (ClienteInexistenteException)exception;
                message = $"O cliente com o id {clienteInexistenteException.Id} não existe!";
                Log(message, LogLevel.Information);
                return (HttpStatusCode.NotFound, message);
            case LimiteInsuficienteException:
                message = $"O cliente não possui limite suficiente para a transação";
                Log(message,LogLevel.Information);
                return (HttpStatusCode.UnprocessableEntity, message);
            default:
                message = "Ocorreu um erro no servidor: ";
                Log(message + exception.Message,LogLevel.Error);
                return (HttpStatusCode.InternalServerError, message);
        }
    }
    private void Log(string message, LogLevel loglevel)
    {
        if(_environment is not null && _environment != "dev")
            Logger.Log(loglevel,message);
    }
}
