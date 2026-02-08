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

namespace StoreApp.Application.UseCases.PaymentUseCase.Query.GetList
{
    public class GetListPaymentHandler(IPaymentRepository paymentRepository) : IRequestHandler<GetListPaymentQuery, ResultWithData<List<PaymentDTO>>>
    {
        public async Task<ResultWithData<List<PaymentDTO>>> Handle(GetListPaymentQuery request, CancellationToken cancellationToken)
        {
            var payments = await paymentRepository.GetAll();

            var paymentDTOs = payments.Select(p => p.ToDTO()).ToList();
            return new ResultWithData<List<PaymentDTO>>(true, "Thành công", paymentDTOs);
        }
    }
}
