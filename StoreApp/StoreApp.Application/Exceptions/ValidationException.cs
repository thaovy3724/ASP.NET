using System.Net;

namespace StoreApp.Application.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Một hoặc nhiều lỗi xác thực đã xảy ra", HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }

        public ValidationException(string field, string error)
            : base("Một hoặc nhiều lỗi xác thực đã xảy ra", HttpStatusCode.BadRequest)
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, [error] }
            };
        }
    }
}
