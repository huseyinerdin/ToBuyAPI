using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.DTOs.Category;
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

		//Task<IDataResult<List<ListItemCategory>>> GetAllAsync();
		//Task<IDataResult<ListItemCategory>> GetByIdAsync(string id);
	}
}
