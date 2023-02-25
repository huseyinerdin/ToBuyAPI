using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Product;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Repositories;
using ToBuyAPI.Persistence.Services.ResultService;

namespace ToBuyAPI.Persistence.Services
{
	public class ProductService : IProductService
	{
		private readonly IMapper _mapper;
		private readonly IProductImageFileService _productImageFileService;
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IProductReadRepository _productReadRepository;
		private readonly ICategoryReadRepository _categoryReadRepository;
		public ProductService(IMapper mapper, IProductWriteRepository productWriteRepository, IProductImageFileService productImageFileService, IProductReadRepository productReadRepository, ICategoryReadRepository categoryReadRepository)
		{
			_mapper = mapper;
			_productWriteRepository = productWriteRepository;
			_productImageFileService = productImageFileService;
			_productReadRepository = productReadRepository;
			_categoryReadRepository = categoryReadRepository;
		}
		#region Write Methods
		public async Task<IResult> AddAsync(CreateProduct model)
		{
			Result result = new ();
			Product product = _mapper.Map<Product>(model);
			List<Category> categories =  _categoryReadRepository.GetWhere(x=>model.CategoryIds.Contains(x.Id.ToString())).ToList();
			foreach (var category in categories)
			{
				product.Categories.Add(category);
			}

			result.IsSuccess = await _productWriteRepository.AddAsync(product);

			if (result.IsSuccess)
			{
				var pictureResult = await _productImageFileService.AddAsync(product.Id.ToString(), model.ProductImageFiles);
				await _productWriteRepository.SaveAsync();
				result.Message = "Ürün ekleme işlemi başarılı. " + pictureResult.Message;
			}
			else
			{
				result.Message = "Ekleme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> DeleteAsync(DeleteProduct model)
		{
			Result result = new();
			Product product = _mapper.Map<Product>(model);
			result.IsSuccess = _productWriteRepository.Remove(product);
			if (result.IsSuccess)
			{
				await _productWriteRepository.SaveAsync();
				result.Message = "Silme işlemi başarılı.";
			}
			else
			{
				result.Message = "Silme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> DeleteByIdAsync(string id)
		{
			Result result = new();
			result.IsSuccess = await _productWriteRepository.RemoveAsync(id);
			if (result.IsSuccess)
			{
				await _productWriteRepository.SaveAsync();
				result.Message = "Silme işlemi başarılı.";
			}
			else
			{
				result.Message = "Silme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> DeleteRangeAsync(List<DeleteProduct> models)
		{
			Result result = new();
			List<Product> products = _mapper.Map<List<Product>>(models);
			result.IsSuccess = _productWriteRepository.Remove(products);
			if (result.IsSuccess)
			{
				await _productWriteRepository.SaveAsync();
				result.Message = "Silme işlemleri başarılı.";
			}
			else
			{
				result.Message = "Silme işlemleri başarısız.";
			}
			return result;
		}
		public async Task<IResult> UpdateAsync(UpdateProduct model)
		{
			Result result = new();
			Product product = await _productReadRepository.GetSingleAsync(x => x.Id.ToString() == model.Id);
			if (product == null)
			{
				result.IsSuccess = false;
				result.Message = "Güncelleme işlemi başarısız";
				return result;
			}
			product.Name = model.Name;
			product.Description = model.Description;
			List<Category> categories = _categoryReadRepository.GetWhere(x => model.CategoryIds.Contains(x.Id.ToString())).ToList();
			product.Categories = categories;
			if (model.isImagesUpdated)
			{
				await _productImageFileService.DeleteAllByProductId(product.Id.ToString());
				var pictureResult = await _productImageFileService.AddAsync(product.Id.ToString(), model.ProductImageFiles);
				await _productWriteRepository.SaveAsync();
				result.Message = "Ürün Güncelleme işlemi başarılı. " + pictureResult.Message;
			}
			else
			{
				result.Message = "Güncelleme işlemi başarısız.";
			}
			return result;
		}
		#endregion
		#region Read Methods
		public async Task<IDataResult<ListItemProduct>> 
		#endregion
	}
}

