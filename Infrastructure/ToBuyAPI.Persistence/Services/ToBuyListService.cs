using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.ToBuyList;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Services.ResultService;

namespace ToBuyAPI.Persistence.Services
{
	public class ToBuyListService : IToBuyListService
	{
		private readonly IMapper _mapper;
		private readonly IToBuyListReadRepository _toBuyListReadRepository;
		private readonly IToBuyListWriteRepository _toBuyListWriteRepository;
		private readonly ICategoryReadRepository _categoryReadRepository;


		public ToBuyListService(IMapper mapper, IToBuyListReadRepository toBuyListReadRepository, IToBuyListWriteRepository toBuyListWriteRepository, ICategoryReadRepository categoryReadRepository)
		{
			_mapper = mapper;
			_toBuyListReadRepository = toBuyListReadRepository;
			_toBuyListWriteRepository = toBuyListWriteRepository;
			_categoryReadRepository = categoryReadRepository;
		}

		#region Write Methods
		public async Task<IResult> AddAsync(CreateToBuyList model)
		{
			Result result = new();
			ToBuyList toBuyList = _mapper.Map<ToBuyList>(model);
			List<Category> categories = _categoryReadRepository.GetWhere(x => model.CategoryIds.Contains(x.Id.ToString())).ToList();
			foreach (var category in categories)
			{
				toBuyList.Categories.Add(category);
			}
			result.IsSuccess = await _toBuyListWriteRepository.AddAsync(toBuyList);
			if (result.IsSuccess)
			{
				await _toBuyListWriteRepository.SaveAsync();
				result.Message = "Ekleme işlemi başarılı.";
			}
			else
			{
				result.Message = "Ekleme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> DeleteAsync(DeleteToBuyList model)
		{
			Result result = new();
			ToBuyList toBuyList = _mapper.Map<ToBuyList>(model);
			result.IsSuccess = _toBuyListWriteRepository.Remove(toBuyList);
			if (result.IsSuccess)
			{
				await _toBuyListWriteRepository.SaveAsync();
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
			result.IsSuccess = await _toBuyListWriteRepository.RemoveAsync(id);
			if (result.IsSuccess)
			{
				await _toBuyListWriteRepository.SaveAsync();
				result.Message = "Silme işlemi başarılı.";
			}
			else
			{
				result.Message = "Silme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> DeleteRangeAsync(List<DeleteToBuyList> models)
		{
			Result result = new();
			List<ToBuyList> toBuyLists = _mapper.Map<List<ToBuyList>>(models);
			result.IsSuccess = _toBuyListWriteRepository.Remove(toBuyLists);
			if (result.IsSuccess)
			{
				await _toBuyListWriteRepository.SaveAsync();
				result.Message = "Silme işlemi başarılı.";
			}
			else
			{
				result.Message = "Silme işlemi başarısız.";
			}
			return result;
		}
		public async Task<IResult> UpdateAsync(UpdateToBuyList model)
		{
			Result result = new();
			ToBuyList toBuyList = await _toBuyListReadRepository.GetSingleAsync(x => x.Id.ToString() == model.Id);
			if (toBuyList == null)
			{
				result.IsSuccess = false;
				result.Message = "Güncelleme işlemi başarısız.";
				return result;
			}
			toBuyList.Name = model.Name;
			toBuyList.CustomerId = Guid.Parse(model.CustomerId);
			if (model.IsCompleted)
			{
				toBuyList.CompletedDate = DateTime.Now;
			}
			toBuyList.Categories = _categoryReadRepository.GetWhere(x => model.CategoryIds.Contains(x.Id.ToString())).ToList();
			result.IsSuccess = _toBuyListWriteRepository.Update(toBuyList);
			if (result.IsSuccess)
			{
				result.Message = "Güncelleme işlemi başarılı.";
				await _toBuyListWriteRepository.SaveAsync();
			}
			else
			{
				result.Message = "Güncelleme işlemi başarısız.";
			}
			return result;
		}
		#endregion
	}
}
