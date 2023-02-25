using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Product;
using ToBuyAPI.Application.Repositories;
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

		public async Task<IResult> AddAsync(CreateProduct model)
		{
			Result result = new ();
			Product product = _mapper.Map<Product>(model);
			foreach (var categoryId in model.CategoryIds)
			{
				product.Categories.Add(_categoryReadRepository.GetByIdAsync(categoryId).Result);
			}

			result.IsSuccess = await _productWriteRepository.AddAsync(product);

			if (result.IsSuccess)
			{
				//_productReadRepository.GetSingleAsync(x => x.Name = product.Name);
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
	}
}

