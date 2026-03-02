using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Cancel
{
    public class CancelGRNHandler(IGRNRepository grnRepository) : IRequestHandler<CancelGRNCommand, Unit>
    {
        public async Task<Unit> Handle(CancelGRNCommand request, CancellationToken cancellationToken)
        {
            var grn = await grnRepository.GetById(request.Id);
            if (grn is null)
            {
                throw new NotFoundException($"Không tìm thấy phiếu nhập hàng với Id: {request.Id}");
            }
            // Cập nhật trạng thái phiếu nhập hàng thành "Đã hủy"
            grn.CancelGRN();
            await grnRepository.Update(grn);
            return Unit.Value;
        }
    }
}
