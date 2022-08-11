namespace SalesWeb.ViewModels.ErrorViewModels;

public class ErrorViewModel
{
    public ErrorViewModel()
    {
        Errors = new List<string>();
    }
    
    public ErrorViewModel(string error)
    {
        Errors.Add(error);
    }
    
    public ErrorViewModel(List<string> errors)
    {
        Errors = errors;
    }

    public List<string> Errors { get; set; } = new();
    
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    
}