using System.Buffers;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging.Console;

namespace WebApi.Middlewares;

public abstract class BasicMiddleware
{
    private readonly RequestDelegate _next;
    protected static readonly ILogger Logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("ErrorHandlingMiddleware");
    public BasicMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public abstract (HttpStatusCode code, string message) GetResponse(Exception ex);
    public async Task Invoke(HttpContext context)
    {
        try 
        {
            
            await _next(context);
        }
        catch(Exception ex)
        {
            var response = context.Response;
        
            var (status, message) = GetResponse(ex);
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
    }
}
