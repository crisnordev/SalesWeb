using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SalesWeb.Extentions;

public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState, string errorMessage)
    {
        var result = new List<string>{errorMessage};
        foreach (var item in modelState.Values)
            result.AddRange(item.Errors.Select(error => error.ErrorMessage));
        return result;
    }
}