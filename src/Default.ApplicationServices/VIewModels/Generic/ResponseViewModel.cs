namespace Default.ApplicationServices.VIewModels.Generic;

public record ResponseViewModel<TResponse>
{
    public string Message { get; set; }
    public TResponse Data { get; set; }

    public ResponseViewModel()
    {
    }

    public ResponseViewModel(string message)
    {
        Message = message;
    }
}