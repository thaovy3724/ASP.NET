using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderCommand(Guid? CustomerId, DateTime OrderDate, Guid UserId, Guid? PromoId, Core.ValueObject.OrderStatus OrderStatus, decimal DiscountAmount, decimal TotalAmount, IEnumerable<OrderItemDTO> Items)
        : IRequest<ResultWithData<OrderDTO>>;
}
