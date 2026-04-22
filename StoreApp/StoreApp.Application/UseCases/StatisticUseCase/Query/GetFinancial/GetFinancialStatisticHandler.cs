using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.StatisticUseCase.Query.GetFinancial
{
    public class GetFinancialStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetFinancialStatisticQuery, List<FinancialStatisticDTO>>
    {
        public async Task<List<FinancialStatisticDTO>> Handle(
            GetFinancialStatisticQuery request,
            CancellationToken cancellationToken)
        {
            return await statisticRepository.GetFinancialStatisticAsync(
                request.FromDate,
                request.ToDate,
                cancellationToken);
        }
    }
}