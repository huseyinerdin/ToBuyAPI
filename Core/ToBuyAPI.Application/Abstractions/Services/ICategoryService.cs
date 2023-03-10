using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<IResult> AddAsync(CreateCategory model);
        Task<IResult> AddRangeAsync(List<CreateCategory> models);
        Task<IResult> DeleteAsync(DeleteCategory model);
        Task<IResult> DeleteByIdAsync(string id);
        Task<IResult> DeleteRangeAsync(List<DeleteCategory> models);
        Task<IResult> UpdateAsync(UpdateCategory model);

        Task<IDataResult<List<ListItemCategory>>> GetAllAsync();
        Task<IDataResult<ListItemCategory>> GetByIdAsync(string id);
    }
}
