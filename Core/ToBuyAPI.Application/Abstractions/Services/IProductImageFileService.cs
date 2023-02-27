using ToBuyAPI.Application.Abstractions.Result;

namespace ToBuyAPI.Application.Abstractions.Services
{
	public interface IProductImageFileService
	{
		Task<IResult> AddAsync(string productId, Microsoft.AspNetCore.Http.IFormFileCollection model);
		Task<IResult> DeleteByIdAsync(string id);
		Task<IResult> DeleteAllByProductId(string id);
	}
}
