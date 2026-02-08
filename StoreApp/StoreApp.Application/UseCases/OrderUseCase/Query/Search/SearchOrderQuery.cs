using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.Search
{
    public sealed record SearchOrderQuery(string? keyword) : IRequest<ResultWithData<List<OrderDTO>>>;
}
