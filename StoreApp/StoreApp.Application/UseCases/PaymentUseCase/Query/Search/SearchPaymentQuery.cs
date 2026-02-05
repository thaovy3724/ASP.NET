using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PaymentUseCase.Query.Search
{
    public sealed record SearchPaymentQuery(string Keyword) : IRequest<Results.ResultWithData<List<DTOs.PaymentDTO>>>;
}
