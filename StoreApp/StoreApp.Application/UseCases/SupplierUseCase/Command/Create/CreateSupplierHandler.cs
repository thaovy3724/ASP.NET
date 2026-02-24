using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Create
{
    public class CreateSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<CreateSupplierCommand, ResultWithData<SupplierDTO>>
    {
        public async Task<ResultWithData<SupplierDTO>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra trùng tên, email, số điện thoại của nhà cung cấp
            if (await supplierRepository.IsExist(s => s.Name == request.Name))
            {
                throw new ConflictException("Tên nhà cung cấp đã tồn tại.");
            }

            if (await supplierRepository.IsExist(s => s.Phone == request.Phone))
            {
                throw new ConflictException("Số điện thoại nhà cung cấp đã tồn tại.");
            }

            if (await supplierRepository.IsExist(s => s.Email == request.Email))
            {
                throw new ConflictException("Email nhà cung cấp đã tồn tại.");
            }

            // Tạo mới nhà cung cấp
            var supplier = new Supplier(
                request.Name,
                request.Phone,
                request.Email,
                request.Address
            );

            // Gọi repository để tạo nhà cung cấp mới
            await supplierRepository.Create(supplier);

            // Trả về kết quả thành công cùng với dữ liệu nhà cung cấp đã được tạo
            return new ResultWithData<SupplierDTO>(
                Success: true,
                Message: "Tạo nhà cung cấp thành công.",
                Data: supplier.ToDTO()
            );
        }
    }
}
