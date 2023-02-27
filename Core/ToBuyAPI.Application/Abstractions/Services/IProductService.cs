using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.DTOs.Product;

namespace ToBuyAPI.Application.Abstractions.Services
{
	public interface IProductService
	{
		Task<IResult> AddAsync(CreateProduct model);
		Task<IResult> DeleteAsync(DeleteProduct model);
		Task<IResult> DeleteByIdAsync(string id);
		Task<IResult> DeleteRangeAsync(List<DeleteProduct> models);
		Task<IResult> UpdateAsync(UpdateProduct model);
		Task<IDataResult<List<ListItemProduct>>> GetAllAsync();
		Task<IDataResult<ListItemProduct>> GetByIdAsync(string id);
		Task<IDataResult<List<ListItemProduct>>> GetByCategoryIdAsync(string id);
		Task<IDataResult<List<ListItemProduct>>> GetByListOfCategoryIdAsync(List<string> ids);
	}
}
