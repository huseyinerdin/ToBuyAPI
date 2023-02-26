using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.DTOs.Product;

namespace ToBuyAPI.Application.Abstractions.Services
{
	public interface IProductImageFileService
	{
		Task<IResult> AddAsync(string productId, Microsoft.AspNetCore.Http.IFormFileCollection model);
		Task<IResult> DeleteByIdAsync(string id);
		Task<IResult> DeleteAllByProductId(string id);
	}
}
