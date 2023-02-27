using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.DTOs.ToBuyList;

namespace ToBuyAPI.Application.Abstractions.Services
{
	public interface IToBuyListService
	{
		Task<IResult> AddAsync(CreateToBuyList model);
		Task<IResult> DeleteAsync(DeleteToBuyList model);
		Task<IResult> DeleteByIdAsync(string id);
		Task<IResult> DeleteRangeAsync(List<DeleteToBuyList> models);
		Task<IResult> UpdateAsync(UpdateToBuyList model);
		Task<IDataResult<List<ListItemToBuyList>>> GetAllAsync();
		Task<IDataResult<ListItemToBuyList>> GetByIdAsync(string id);
	}
}
