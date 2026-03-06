using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.Exceptions;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Update
{
    public class UpdateGRNHandler(IGRNRepository grnRepository, IProductRepository productRepository) : IRequestHandler<UpdateGRNCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateGRNCommand request, CancellationToken cancellationToken)
        {
            var grn = await grnRepository.GetById(request.Id);
            if(grn is null)
            {
                throw new NotFoundException("Phiếu nhập không tồn tại"); 
            }

            //Bắt đầu Transaction để đảm bảo tính nguyên tử (Atomicity)
            await grnRepository.BeginTransactionAsync();
            try
            {
                grn.UpdateItem(request.Items.Select(item=>new GRNDetail(
                    grn.Id, item.ProductId, item.Quantity, item.Price
                    )).ToList());
                await grnRepository.Update(grn);

                // 6. Xác nhận giao dịch thành công
                await grnRepository.CommitTransactionAsync();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                await grnRepository.RollbackTransactionAsync();
                if (ex is DomainException) throw;

                // Lỗi hệ thống chưa biết
                throw new Exception("Hệ thống không thể xử lý phiếu nhập kho lúc này. Vui lòng liên hệ quản trị viên.");
            }
        }
    }
}
