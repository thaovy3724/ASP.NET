using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Exceptions
{
    public class ProductCannotBeDeletedException(string message) : DomainException(message)
    {
    }
}
