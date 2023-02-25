using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.Abstractions.Storage;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Repositories;
using ToBuyAPI.Persistence.Services.ResultService;

namespace ToBuyAPI.Persistence.Services
{
	public class ProductImageFileService : IProductImageFileService
	{
		private readonly IStorageService _storageService;
		private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

		public ProductImageFileService(IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository)
		{
			_storageService = storageService;
			_productImageFileWriteRepository = productImageFileWriteRepository;
		}

		public async Task<IResult> AddAsync(string productId, Microsoft.AspNetCore.Http.IFormCollection models)
		{
			Result result = new();

			#region File Local Upload
			var uploadResult = await _storageService.UploadAsync("images", (Microsoft.AspNetCore.Http.IFormFileCollection)models);
			if (uploadResult == null)
			{
				result.IsSuccess = false;
				result.Message = "Resim ekleme işlemi başarısız.";
				return result;
			}
			#endregion

			#region File Database Upload
			List<ProductImageFile> createProductImageFiles = new();
			foreach (var file in uploadResult)
			{
				createProductImageFiles.Add(new ProductImageFile()
				{
					FileName = file.fileName,
					Storage = _storageService.StorageName,
					Path = file.path,
					ProductId = Guid.Parse(productId)
				});
			}
			result.IsSuccess = await _productImageFileWriteRepository.AddRangeAsync(createProductImageFiles);
			if (result.IsSuccess)
			{
				await _productImageFileWriteRepository.SaveAsync();
				result.Message = "Resim ekleme işlemi başarılı.";
			}
			else
			{
				result.Message = "Resim ekleme işlemi başarısız.";
			}
			return result;
			#endregion
		}
	}
}
