using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi;

public static class Util
{
    public static IEnumerable<string> GetModelStateErrors(ModelStateDictionary modelState)
    {
        return modelState.Values.SelectMany(m => m.Errors).Select(x => x.ErrorMessage);
    }
}
