using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PromotionUseCase.Query.Search
{
    public sealed record SearchPromotionQuery(string? Keyword) : IRequest<ResultWithData<List<PromotionDTO>>>;                      
}
