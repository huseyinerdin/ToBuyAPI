using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        Task<IDataResult<List<DetailCategory>>> GetAllDetailAsync();
        Task<IDataResult<DetailCategory>> GetByIdAsync(string id);
    }
}
