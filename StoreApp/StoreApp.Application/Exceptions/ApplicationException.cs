using System.Net;

namespace StoreApp.Application.Exceptions
{
    public abstract class ApplicationException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
