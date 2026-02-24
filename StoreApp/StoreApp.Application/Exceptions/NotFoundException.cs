namespace StoreApp.Application.Exceptions
{
    public class NotFoundException(string message) : ApplicationException(message, System.Net.HttpStatusCode.NotFound)
    {
    }
}
