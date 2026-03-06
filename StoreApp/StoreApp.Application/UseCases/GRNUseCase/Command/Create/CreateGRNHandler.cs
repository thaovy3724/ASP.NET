using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public class CreateGRNHandler(IGRNRepository grnRepository, 
                                ISupplierRepository supplierRepository,
                                IProductRepository productRepository) : IRequestHandler<CreateGRNCommand, GRNDTO>
    {
        public async Task<GRNDTO> Handle(CreateGRNCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra tồn tại nhà cung cấp (Supplier)
            var supplier = await supplierRepository.GetById(request.SupplierId);
            if(supplier is null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại");
            }

            //Bắt đầu Transaction để đảm bảo tính nguyên tử (Atomicity)
            await grnRepository.BeginTransactionAsync();

            try
            {
                // Tạo mới GRN
                var grn = new GRN(request.SupplierId);

                foreach(var item in request.Items)
                {
                    var product = await productRepository.GetById(item.ProductId);
                    if (product is null)
                    {
                        throw new NotFoundException($"Sản phẩm với ID {item.ProductId} không tồn tại");
                    }

                    grn.AddItem(item.ProductId, item.Quantity, item.Price);
                }
                await grnRepository.Create(grn);

                // 6. Xác nhận giao dịch thành công
                await grnRepository.CommitTransactionAsync();
                return grn.ToDTO();
            }
            catch (Exception ex)
            {
                await grnRepository.RollbackTransactionAsync();
                if (ex is NotFoundException) throw;

                // Lỗi hệ thống chưa biết
                throw new Exception("Hệ thống không thể xử lý phiếu nhập kho lúc này. Vui lòng liên hệ quản trị viên.");
            }
        }
    }
}
