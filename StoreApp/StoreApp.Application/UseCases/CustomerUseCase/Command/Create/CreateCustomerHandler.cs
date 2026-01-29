using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CustomerUseCase.Command.Create
{
    public class CreateCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<CreateCustomerCommand, ResultWithData<CustomerDTO>>
    {
        public async Task<ResultWithData<CustomerDTO>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if(await customerRepository.IsEmailExists(request.email))
            {
                return new ResultWithData<CustomerDTO>
                (
                    Success : false,
                    Message : "Email đã tồn tại!",
                    Data : null
                );
            }
            if(await customerRepository.IsPhoneExists(request.phone))
            {
                return new ResultWithData<CustomerDTO>
                (
                    Success : false,
                    Message : "Số điện thoại đã tồn tại!",
                    Data : null
                );
            }
            var customer = new Customer(request.name, request.phone, request.email, request.address, DateTime.Now);
            await customerRepository.Create(customer);
            return new ResultWithData<CustomerDTO>
            (
                Success : true,
                Message : "Tạo khách hàng thành công!",
                Data : customer.ToDTO()
            );
        }
    }
}
