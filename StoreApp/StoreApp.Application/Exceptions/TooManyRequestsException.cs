namespace StoreApp.Application.Exceptions
{
    public class TooManyRequestsException(string message)
        : ApplicationException(message, System.Net.HttpStatusCode.TooManyRequests);
}