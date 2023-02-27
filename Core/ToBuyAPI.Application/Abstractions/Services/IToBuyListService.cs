using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}
