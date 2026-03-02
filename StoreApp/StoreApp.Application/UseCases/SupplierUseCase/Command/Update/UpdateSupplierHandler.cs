using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Update
{
    public class UpdateSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<UpdateSupplierCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetById(request.Id);
            if (supplier is null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại.");
            }

            if(await supplierRepository.IsExist(s => s.Name == request.Name && s.Id != request.Id))
            {
                throw new ConflictException("Tên nhà cung cấp đã tồn tại.");
            }

            if(await supplierRepository.IsExist(s => s.Phone == request.Phone && s.Id != request.Id))
            {
                throw new ConflictException("Số điện thoại nhà cung cấp đã tồn tại.");
            }

            if(await supplierRepository.IsExist(s => s.Email == request.Email && s.Id != request.Id))
            {
                throw new ConflictException("Email nhà cung cấp đã tồn tại.");
            }

            supplier.Update(
                request.Name,
                request.Phone,
                request.Email,
                request.Address
            );
            await supplierRepository.Update(supplier);
            return Unit.Value;
        }
    }
}
