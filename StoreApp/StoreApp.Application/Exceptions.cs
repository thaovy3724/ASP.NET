using System.Net;

namespace StoreApp.Application
{
    public abstract class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }
    public class ConflictException : BaseException
    {
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }
    public sealed class ValidationException : BaseException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("One or more validation errors occurred.", HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }

        public ValidationException(string field, string error)
            : base("One or more validation errors occurred.", HttpStatusCode.BadRequest)
        {
            Errors = new Dictionary<string, string[]>
        {
            { field, [error] }
        };
        }
    }
}
