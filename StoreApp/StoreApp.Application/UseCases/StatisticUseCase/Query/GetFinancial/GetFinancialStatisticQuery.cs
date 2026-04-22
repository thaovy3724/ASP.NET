using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.StatisticUseCase.Query.GetFinancial
{
    public sealed record GetFinancialStatisticQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<List<FinancialStatisticDTO>>;
}