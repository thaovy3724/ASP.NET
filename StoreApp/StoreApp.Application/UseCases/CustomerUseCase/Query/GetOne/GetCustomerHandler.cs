using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CustomerUseCase.Query.GetOne
{
    public class GetCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomerQuery, ResultWithData<CustomerDTO>>
    {
        public string Message { get; private set; }

        public async Task<ResultWithData<CustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetById(request.Id);
            if(customer is null)
            {
                return new ResultWithData<CustomerDTO>
                (
                    Success : false,
                    Message : "Không tìm thấy thông tin khách hàng!",
                    Data : null
                );
            }
            return new ResultWithData<CustomerDTO>
            (
                Success: true,
                Message: "Lấy thông tin khách hàng thành công!",
                Data : customer.ToDTO()
            );
        }
    }
}
