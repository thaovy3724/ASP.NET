using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetOne
{
    public class GetGRNHandler(IGRNRepository grnRepository) : IRequestHandler<GetGRNQuery, GRNDTO>
    {
        public async Task<GRNDTO> Handle(GetGRNQuery request, CancellationToken cancellationToken)
        {
            var grn = await grnRepository.GetById(request.Id);
            if (grn is null)
            {
                throw new NotFoundException($"Không tìm thấy tồn kho với Id: {request.Id}");
            }

            return grn.ToDTO();
        }
    }
}
