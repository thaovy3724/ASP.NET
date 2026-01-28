using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Results
{
    public record Result
    (
        bool Success = true,
        string Message = "Thao tác thành công"
    );
}
