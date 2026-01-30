using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
}
