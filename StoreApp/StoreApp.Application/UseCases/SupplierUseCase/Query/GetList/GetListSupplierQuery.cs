using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public sealed record GetListSupplierQuery : IRequest<ResultWithData<List<SupplierDTO>>>;
}
