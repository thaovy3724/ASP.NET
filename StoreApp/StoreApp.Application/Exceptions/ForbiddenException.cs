using System.Net;

namespace StoreApp.Application.Exceptions
{
    public class ForbiddenException(string message)
        : ApplicationException(message, HttpStatusCode.Forbidden);
}