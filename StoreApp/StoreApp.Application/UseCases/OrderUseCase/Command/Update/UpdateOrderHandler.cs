using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Update
{
    public class UpdateOrderHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderCommand, Result>
    {
        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.Id);
            if (order == null)
            {
                throw new Exception($"Không tìm thấy đơn hàng với mã: {request.Id}");
            }
            order.Update(request.Status);
            await orderRepository.Update(order);
            return new Result { Success = true, Message = "Cập nhật đơn hàng thành công" };
        }
    }
}
