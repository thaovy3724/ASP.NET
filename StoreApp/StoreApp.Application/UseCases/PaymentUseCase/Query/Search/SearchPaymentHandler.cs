using MediatR;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PaymentUseCase.Query.Search
{
    public class SearchPaymentHandler(IPaymentRepository paymentRepository) : IRequestHandler<SearchPaymentQuery, ResultWithData<List<DTOs.PaymentDTO>>>
    {
        public async Task<ResultWithData<List<DTOs.PaymentDTO>>> Handle(SearchPaymentQuery request, CancellationToken cancellationToken)
        {
            var payments = await paymentRepository.SearchByKeyword(request.Keyword);
            var paymentDTOs = payments.Select(p => p.ToDTO()).ToList();
            return new ResultWithData<List<DTOs.PaymentDTO>>(true, "Thành công", paymentDTOs);
        }
    }
}
