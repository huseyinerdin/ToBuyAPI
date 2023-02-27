using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.Abstractions.Storage;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Services.ResultService;

namespace ToBuyAPI.Persistence.Services
{
	public class ProductImageFileService : IProductImageFileService
	{
		private readonly IStorageService _storageService;
		private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
		private readonly IProductImageFileReadRepository _productImageFileReadRepository;

		public ProductImageFileService(IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository)
		{
			_storageService = storageService;
			_productImageFileWriteRepository = productImageFileWriteRepository;
			_productImageFileReadRepository = productImageFileReadRepository;
		}

		public async Task<IResult> AddAsync(string productId, Microsoft.AspNetCore.Http.IFormFileCollection models)
		{
			Result result = new();

			#region File Local Upload
			var uploadResult = await _storageService.UploadAsync("images", models);
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
				//await _productImageFileWriteRepository.SaveAsync();
				result.Message = "Resim ekleme işlemi başarılı.";
			}
			else
			{
				result.Message = "Resim ekleme işlemi başarısız.";
			}
			return result;
			#endregion
		}

		public async Task<IResult> DeleteByIdAsync(string id)
		{
			Result result = new();
			ProductImageFile productImageFile = await _productImageFileReadRepository.GetByIdAsync(id);
			result.IsSuccess =  _productImageFileWriteRepository.Remove(productImageFile);
			if (result.IsSuccess)
			{
				await _storageService.DeleteAsync(productImageFile.Path, productImageFile.FileName);
				await _productImageFileWriteRepository.SaveAsync();
				result.Message = "Silme işlemi başarılı.";
			}
			else
			{
				result.Message = "Silme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> DeleteAllByProductId(string id)
		{
			Result result = new();
			List<ProductImageFile> productImageFiles =  _productImageFileReadRepository.GetWhere(x=>x.ProductId == Guid.Parse(id)).ToList();
			result.IsSuccess = _productImageFileWriteRepository.Remove(productImageFiles);
			if (result.IsSuccess)
			{
				foreach (var productImage in productImageFiles)
				{
					string filePath = productImage.Path.Replace("\\"+productImage.FileName,"");
					await _storageService.DeleteAsync(filePath, productImage.FileName);
				}
				await _productImageFileWriteRepository.SaveAsync();
				result.Message = "Silme işlemi başarılı.";
			}
			else
			{
				result.Message = "Silme işlemi başarısız.";
			}
			return result;
		}
	}
}
