using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Update
{
    public class UpdateGRNHandler(IGRNRepository grnRepository, IProductRepository productRepository) : IRequestHandler<UpdateGRNCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateGRNCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra xem phiếu nhập có tồn tại không
            var grn = await grnRepository.GetByIdWithItems(request.Id);
            if (grn is null)
            {
                throw new NotFoundException("Phiếu nhập không tồn tại");
            }

            // Kiểm tra xem tất cả sản phẩm trong phiếu nhập có tồn tại không
            foreach (var item in request.Items)
            {
                var product = await productRepository.GetById(item.ProductId);
                if (product is null)
                {
                    throw new NotFoundException($"Sản phẩm với ID {item.ProductId} không tồn tại");
                }
            }

            // Lưu lại danh sách sản phẩm cũ để so sánh
            var oldProductIds = grn.Items
                .Select(x => x.ProductId)
                .ToHashSet();

            // Bắt đầu transaction để đảm bảo tính toàn vẹn dữ liệu
            await grnRepository.BeginTransactionAsync();
            try
            {
                var newItems = request.Items
                    .Select(item => new GRNDetail(
                        grn.Id,
                        item.ProductId,
                        item.Quantity,
                        item.Price
                    ))
                    .ToList();

                // Cập nhật phiếu nhập với danh sách sản phẩm mới
                grn.UpdateItem(newItems);

                foreach (var item in grn.Items.Where(x => !oldProductIds.Contains(x.ProductId)))
                {
                    grnRepository.MarkDetailAsAdded(item);
                }

                // Cập nhật các sản phẩm đã tồn tại
                await grnRepository.SaveChangesAsync();
                await grnRepository.CommitTransactionAsync();
                return Unit.Value;
            }
            catch
            {
                await grnRepository.RollbackTransactionAsync();
                // Nếu có lỗi xảy ra, rollback transaction và ném lại lỗi để xử lý ở tầng trên
                throw;
            }
        }
    }
}