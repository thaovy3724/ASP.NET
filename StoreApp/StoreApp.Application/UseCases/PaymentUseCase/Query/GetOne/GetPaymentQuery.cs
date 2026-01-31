using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PaymentUseCase.Query.GetOne
{
    public sealed record GetPaymentQuery(Guid Id) : IRequest<ResultWithData<PaymentDTO>>;
}
