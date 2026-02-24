using System.Net;

namespace StoreApp.Application.Exceptions
{
    internal class ConflictException(string message) : ApplicationException(message, HttpStatusCode.Conflict)
    {
    }
}
