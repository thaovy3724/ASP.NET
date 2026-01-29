using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.SupplierUseCase.Query.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne
{
    public sealed record GetSupplierQuery(Guid Id) : IRequest<ResultWithData<SupplierDTO>>;
}
