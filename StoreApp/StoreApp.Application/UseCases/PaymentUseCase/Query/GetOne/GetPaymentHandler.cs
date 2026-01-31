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

namespace StoreApp.Application.UseCases.PaymentUseCase.Query.GetOne
{
    internal class GetPaymentHandler(IPaymentRepository paymentRepository) : IRequestHandler<GetPaymentQuery, ResultWithData<PaymentDTO>>
    {
        public async Task<ResultWithData<PaymentDTO>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await paymentRepository.GetById(request.Id);
            if (payment == null)
            {
                return new ResultWithData<PaymentDTO>(false, "Không tìm thấy phương thức thanh toán", null);
            }
            var paymentDTO = payment.ToDTO();
            return new ResultWithData<PaymentDTO>(true, "Thành công", paymentDTO);
        }
    }
}
