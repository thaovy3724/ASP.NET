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

namespace StoreApp.Application.UseCases.CustomerUseCase.Query.GetList
{
    public class GetListCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<GetListCustomerQuery, ResultWithData<List<CustomerDTO>>>
    {
        public async Task<ResultWithData<List<CustomerDTO>>> Handle(GetListCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await customerRepository.GetAll();
            var customerDTO = customers
                .Select(customer => customer.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return new ResultWithData<List<CustomerDTO>>(
                Success: true,
                Message: "Lấy danh sách khách hàng thành công.",
                Data: customerDTO
            );
        }
    }
}
