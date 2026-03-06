namespace StoreApp.Application.Exceptions
{
    public class BadRequestException(string message) : ApplicationException(message, System.Net.HttpStatusCode.BadRequest);
}
