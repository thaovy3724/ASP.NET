using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public sealed record DeleteListOfProductCommand(List<Guid> ProductIds) : IRequest<Unit>;
}
