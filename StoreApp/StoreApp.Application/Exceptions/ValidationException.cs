using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
            public IDictionary<string, string[]> Errors { get; }
    
            public ValidationException(string field, string error)
                : base("One or more validation errors occurred.", System.Net.HttpStatusCode.BadRequest)
            {
                Errors = new Dictionary<string, string[]>
            {
                { field, [error] }
            };
        }
    }
}
