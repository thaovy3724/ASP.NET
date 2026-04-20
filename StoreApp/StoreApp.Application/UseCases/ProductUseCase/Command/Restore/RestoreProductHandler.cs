using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Restore
{
    public class RestoreProductHandler(IProductRepository productRepository) : IRequestHandler<RestoreProductCommand, Unit>
    {
        public async Task<Unit> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.Id);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            try
            {
                product.Restore();
            }
            catch (StoreApp.Core.Exceptions.DomainException ex)
            {
                throw new ConflictException(ex.Message);
            }

            await productRepository.Update(product);
            return Unit.Value;
        }
    }
}