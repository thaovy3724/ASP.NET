using System.Net;

namespace StoreApp.Application.Exceptions
{
    public class ConflictException(string message) : ApplicationException(message, HttpStatusCode.Conflict);
}
