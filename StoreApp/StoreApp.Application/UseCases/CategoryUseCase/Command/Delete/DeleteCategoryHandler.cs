using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Delete
{
    public class DeleteCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, Result>
    {
        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra category có tồn tại không
            var category = await categoryRepository.GetById(request.Id);
            if (category is null)
            {
                throw new ConflictException("Thể loại không tồn tại.");
            }
            if(await categoryRepository.IsExistProductOfCategory(request.Id))
            {
                throw new ConflictException("Thể loại đang được sử dụng, không thể xóa.");
            }
            // Thực hiện xóa
            await categoryRepository.Delete(category);
            return new Result(
                Success: true,
                Message: "Xóa thể loại thành công."
            );
        }
    }
}
