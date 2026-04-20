using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.BulkDelete
{
    public class BulkDeleteGRNHandler(
        IGRNRepository grnRepository
    ) : IRequestHandler<BulkDeleteGRNCommand, Unit>
    {
        public async Task<Unit> Handle(BulkDeleteGRNCommand request, CancellationToken cancellationToken)
        {
            var ids = request.Ids?
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList() ?? [];

            if (ids.Count == 0)
            {
                throw new BadRequestException("Phải chọn ít nhất một phiếu nhập để xóa.");
            }

            var grns = await grnRepository.GetByIds(ids);

            var foundIds = grns.Select(x => x.Id).ToHashSet();
            var missingIds = ids.Where(id => !foundIds.Contains(id)).ToList();

            if (missingIds.Count > 0)
            {
                throw new NotFoundException("Có phiếu nhập không tồn tại.");
            }

            var completedGrn = grns.FirstOrDefault(x => x.Status == GRNStatus.Completed);

            if (completedGrn is not null)
            {
                throw new ConflictException("Không thể xóa phiếu nhập đã hoàn thành vì sẽ làm sai lịch sử nhập kho.");
            }

            await grnRepository.DeleteRange(grns);

            return Unit.Value;
        }
    }
}