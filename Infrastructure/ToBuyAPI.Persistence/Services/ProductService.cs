using AutoMapper;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Category;
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
		#region Write Methods
		public async Task<IResult> AddAsync(CreateProduct model)
		{
			Result result = new();
			Product product = _mapper.Map<Product>(model);
			List<Category> categories = _categoryReadRepository.GetWhere(x => model.CategoryIds.Contains(x.Id.ToString())).ToList();
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
			product.Categories.Clear();
			product.Categories = _categoryReadRepository.GetWhere(x=>model.CategoryIds.Contains(x.Id.ToString())).ToList();
			result.IsSuccess = _productWriteRepository.Update(product);
			if (result.IsSuccess)
			{
				if (model.isImagesUpdated)
				{
					await _productImageFileService.DeleteAllByProductId(product.Id.ToString());
					await _productImageFileService.AddAsync(product.Id.ToString(), model.ProductImageFiles);
				}
				result.Message = "Ürün Güncelleme işlemi başarılı. ";
				await _productWriteRepository.SaveAsync();
			}
			else
			{
				result.Message = "Güncelleme işlemi başarısız.";
			}
			return result;
		}
		#endregion

		#region Read Methods
		public async Task<IDataResult<List<ListItemProduct>>> GetAllAsync()
		{
			DataResult<List<ListItemProduct>> dataResult = new();
			List<Product> products = _productReadRepository.GetAll(false).ToList();
			if (products==null)
			{
				dataResult.IsSuccess = false;
				dataResult.Message = "Verileri okuma işlemi başarısız.";
			}
			else
			{
				dataResult.IsSuccess = true;
				dataResult.Message = "Verileri okuma işlemi başarılı.";
				dataResult.Result = _mapper.Map<List<ListItemProduct>>(products);
			}
			return dataResult;
		}
		public async Task<IDataResult<ListItemProduct>> GetByIdAsync(string id)
		{
			DataResult<ListItemProduct> dataResult = new();
			Product product = await _productReadRepository.GetByIdAsync(id);
			if (product == null)
			{
				dataResult.IsSuccess = false;
				dataResult.Message = "Verileri okuma işlemi başarısız.";
			}
			else
			{
				dataResult.IsSuccess = true;
				dataResult.Message = "Verileri okuma işlemi başarılı.";
				dataResult.Result = _mapper.Map<ListItemProduct>(product);
				dataResult.Result.Categories = _mapper.Map<List<ListItemCategory>>(product.Categories);
			}
			return dataResult;
		}
		public async Task<IDataResult<List<ListItemProduct>>> GetByCategoryIdAsync(string id)
		{
			DataResult<List<ListItemProduct>> dataResult = new();
			List<Product> products = _productReadRepository.GetWhere(x=>x.Categories.Any(y=>y.Id.ToString()==id)).ToList();
			if (products == null)
			{
				dataResult.IsSuccess = false;
				dataResult.Message = "Verileri okuma işlemi başarısız.";
			}
			else
			{
				dataResult.IsSuccess = true;
				dataResult.Message = "Verileri okuma işlemi başarılı.";
				dataResult.Result = _mapper.Map<List<ListItemProduct>>(products);
			}
			return dataResult;
		}
		public async Task<IDataResult<List<ListItemProduct>>> GetByListOfCategoryIdAsync(List<string> ids)
		{
			DataResult<List<ListItemProduct>> dataResult = new();
			List<Product> products = _productReadRepository.GetWhere(x => x.Categories.Any(y => ids.Contains(y.Id.ToString()))).ToList();
			if (products == null)
			{
				dataResult.IsSuccess = false;
				dataResult.Message = "Verileri okuma işlemi başarısız.";
			}
			else
			{
				dataResult.IsSuccess = true;
				dataResult.Message = "Verileri okuma işlemi başarılı.";
				dataResult.Result = _mapper.Map<List<ListItemProduct>>(products);
			}
			return dataResult;
		}

		#endregion
	}
}

