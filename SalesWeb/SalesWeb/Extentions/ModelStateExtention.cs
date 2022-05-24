using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SalesWeb.Extentions;

public static class ModelStateExtention 
{
    public static List<string> GetErrors(this ModelStateDictionary modelState, string errorMessage)
    {
        var results = new List<string> {"Check data and try again."};
        results.Add(errorMessage);
        foreach (var item in modelState.Values)
            results.AddRange(item.Errors.Select(error => error.ErrorMessage));
        return results;
    }
}