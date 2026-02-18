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
                throw new ConflictException("Email đã tồn tại!");
            }
            if(await customerRepository.IsPhoneExists(request.phone))
            {
                throw new ConflictException("Số điện thoại đã tồn tại!");
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
