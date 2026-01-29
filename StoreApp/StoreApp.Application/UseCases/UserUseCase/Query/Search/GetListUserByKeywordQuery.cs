using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Query.Search
{
    public sealed record GetListUserByKeywordQuery(string keyword) : IRequest<ResultWithData<List<UserDTO>>>;
}
