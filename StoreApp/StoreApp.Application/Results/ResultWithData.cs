namespace StoreApp.Application.Results
{
    public record ResultWithData<T>(bool Success, string Message, T? Data = default) : Result(Success, Message);
}
