using MediatR;
using StoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.PaymentCallback
{
    public sealed record PaymentCallbackCommand(Dictionary<string, string> PaymentParam) : IRequest<PaymentResponseModel>;
}
