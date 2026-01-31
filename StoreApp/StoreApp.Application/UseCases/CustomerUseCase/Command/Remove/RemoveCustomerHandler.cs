using MediatR;
using StoreApp.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CustomerUseCase.Command.Remove
{
    public class RemoveCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<RemoveCustomerCommand, Results.Result>
    {
        public async Task<Results.Result> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetById(request.Id);
            if (customer is null)
            {
                return new Results.Result
                (
                    Success: false,
                    Message: "Khách hàng không tồn tại!"
                );
            }
            if(await customerRepository.IsExistOderOfCustomer(request.Id))
            {
                return new Results.Result
                (
                    Success: false,
                    Message: "Khách hàng đang có đơn hàng, không thể xóa!"
                );
            }
            await customerRepository.Delete(customer);
            return new Results.Result
            (
                Success: true,
                Message: "Xóa khách hàng thành công!"
            );
        }
    }
}
