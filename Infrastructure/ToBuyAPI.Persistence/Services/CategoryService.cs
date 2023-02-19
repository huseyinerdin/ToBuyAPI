using AutoMapper;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Abstractions.Result;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.DTOs.Category;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Services.ResultService;

namespace ToBuyAPI.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository, IMapper mapper)
        {
            _categoryWriteRepository = categoryWriteRepository;
            _categoryReadRepository = categoryReadRepository;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(CreateCategory model)
        {
            Result result = new();
            Category category = _mapper.Map<Category>(model);
            result.IsSuccess = await _categoryWriteRepository.AddAsync(category);

            if (result.IsSuccess)
            {
                await _categoryWriteRepository.SaveAsync();
                result.Message = "Ekleme işlemi başarılı.";
            }
            else
            {
                result.Message = "Ekleme işlemi başarısız.";
            }
            return result;    
        }

        public async Task<IResult> AddRangeAsync(List<CreateCategory> models)
        {
            Result result = new();
            List<Category> categories = _mapper.Map<List<Category>>(models);
            result.IsSuccess = await _categoryWriteRepository.AddRangeAsync(categories);
            if (result.IsSuccess)
            {
                await _categoryWriteRepository.SaveAsync();
                result.Message = "Ekleme işlemleri başarılı.";
            }
            else
            {
                result.Message = "Ekleme işlemleri başarısız.";
            }
            return result;
        }

        public async Task<IResult> DeleteAsync(DeleteCategory model)
        {
            Result result = new();
            Category category = _mapper.Map<Category>(model);
            result.IsSuccess = _categoryWriteRepository.Remove(category);

            if (result.IsSuccess)
            {
                await _categoryWriteRepository.SaveAsync();
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
            result.IsSuccess = await _categoryWriteRepository.RemoveAsync(id);
            if (result.IsSuccess)
            {
                await _categoryWriteRepository.SaveAsync();
                result.Message = "Silme işlemi başarılı.";
            }
            else
            {
                result.Message = "Silme işlemi başarısız.";
            }
            return result;

        }

        public async Task<IResult> DeleteRangeAsync(List<DeleteCategory> models)
        {
            Result result = new();
            List<Category> categories = _mapper.Map<List<Category>>(models);
            result.IsSuccess = _categoryWriteRepository.Remove(categories);
            if (result.IsSuccess)
            {
                await _categoryWriteRepository.SaveAsync();
                result.Message = "Silme işlemleri başarılı.";
            }
            else
            {
                result.Message = "Silme işlemleri başarısız.";
            }
            return result;
        }
        public async Task<IResult> UpdateAsync(UpdateCategory model)
        {
            Result result = new();
            Category category = await _categoryReadRepository.GetByIdAsync(model.Id);
            if (category == null) 
            {
                result.IsSuccess = false;
                result.Message = "Güncelleme işlemi başarısız.";
                return result;
            }
            category.Name = model.Name;
            result.IsSuccess = _categoryWriteRepository.Update(category);
            if (result.IsSuccess)
            {
                await _categoryWriteRepository.SaveAsync();
                result.Message = "Güncelleme işlemi başarılı.";
            }
            else
            {
                result.Message = "Güncelleme işlemi başarısız.";
            }
            return result;
        }

        public async Task<IDataResult<List<ListItemCategory>>> GetAllAsync()
        {
            DataResult<List<ListItemCategory>> dataResult = new();
            List<Category> categories = _categoryReadRepository.GetAll(false).ToList();
            if (categories == null)
            {
                dataResult.IsSuccess = false;
                dataResult.Message = "Verileri okuma işlemi başarısız.";
            }
            else
            {
                dataResult.IsSuccess = true;
                dataResult.Message = "Verileri okuma işlemi başarılı.";
                dataResult.Result = _mapper.Map<List<ListItemCategory>>(categories);
            }
            return dataResult;
        }

        public async Task<IDataResult<List<DetailCategory>>> GetAllDetailAsync()
        {
            DataResult<List<DetailCategory>> dataResult = new();
            List<Category> categories = _categoryReadRepository.GetAll(false).ToList();
            if (categories == null)
            {
                dataResult.IsSuccess = false;
                dataResult.Message = "Verileri okuma işlemi başarısız.";
            }
            else
            {
                dataResult.IsSuccess = true;
                dataResult.Message = "Verileri okuma işlemi başarılı.";
                dataResult.Result = _mapper.Map<List<DetailCategory>>(categories);
            }
            return dataResult;
        }

        public async Task<IDataResult<DetailCategory>> GetByIdAsync(string id)
        {
            DataResult<DetailCategory> dataResult = new();
            Category category = await _categoryReadRepository.GetByIdAsync(id);
            if (category == null)
            {
                dataResult.IsSuccess = false;
                dataResult.Message = "Veri okuma işlemi başarısız.";
            }
            else
            {
                dataResult.IsSuccess = true;
                dataResult.Message = "Veri okuma işlemi başarılı.";
                dataResult.Result = _mapper.Map<DetailCategory>(category);
            }
            return dataResult;
        }


    }
}
