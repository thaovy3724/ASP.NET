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

namespace StoreApp.Application.UseCases.CustomerUseCase.Query.Search
{
    public class GetListCustomerByKeywordHandler(ICustomerRepository customerRepository) : IRequestHandler<GetListCustomerByKeywordQuery, ResultWithData<List<CustomerDTO>>>
    {
        public async Task<ResultWithData<List<CustomerDTO>>> Handle(GetListCustomerByKeywordQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.keyword))
            {
                return new ResultWithData<List<CustomerDTO>>(
                    Success: false,
                    Message: "Từ khóa tìm kiếm không được để trống.",
                    Data: null
                );
            }
            var customers = await customerRepository.SearchByKeyword(request.keyword);
            var customerDTO = customers
                .Select(customer => customer.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return new ResultWithData<List<CustomerDTO>>(
                Success: true,
                Message: "Tìm kiếm khách hàng thành công.",
                Data: customerDTO
            );
        }
    }
}
