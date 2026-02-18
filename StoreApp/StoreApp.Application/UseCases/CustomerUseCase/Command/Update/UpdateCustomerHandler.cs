using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CustomerUseCase.Command.Update
{
    public class UpdateCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<UpdateCustomerCommand, Result>
    {
        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetById(request.Id);
            if (customer is null)
            {
                throw new NotFoundException("Khách hàng không tồn tại!");
            }
            customer.Update(request.Name, request.Phone, request.Email, request.Address);
            await customerRepository.Update(customer);
            return new Result
            (
                Success: true,
                Message: "Cập nhật khách hàng thành công!"
            );
        }

    }
}
