using App.Repositories.Entities;
using App.Repositories.RepositoryInterfaces;
using App.Repositories.UnitOfWorkPattern;
using App.Services.DTOs.CategoryDTOs;
using App.Services.Results;
using App.Services.ServiceInterfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Services
{
    public class CategoryService(ICategoryRepository _cRepository, IUnitOfWork _unitOfWork,IMapper _mapper) : GenericService<Category>, ICategoryService
    {
        public async Task<ServiceResult<int>> TCreateCategoryAsync(CreateCategoryDTO p)
        {
            var anyCategory = await _cRepository.GetByFilter(x => x.Name == p.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Fail("Aynı Kategori İsmi Veri Tabanında Mevcuttur.", System.Net.HttpStatusCode.BadRequest);
            }
            var mapValue = _mapper.Map<Category>(p);
            await _cRepository.CreateAsync(mapValue);
            await _unitOfWork.CommitAsync();
            return ServiceResult<int>.SuccessAsCreated(mapValue.Id, $"api/categories/{mapValue.Id}");
        }

        public async Task<ServiceResult> TDeleteCategoryAsync(int id)
        {
            var findCategory = await _cRepository.GetValueByIdAsync(id);
            _cRepository.Delete(findCategory!);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<CategoryDTO>> TGetCategoryByIdAsync(int id)
        {
            var findCategory=await _cRepository.GetValueByIdAsync(id);
            if(findCategory is null)
            {
                ServiceResult.Fail("İlgili Kategori Bulunamadı!", System.Net.HttpStatusCode.NotFound);
            }
            var mapValues =  _mapper.Map<CategoryDTO>(findCategory);
            return ServiceResult<CategoryDTO>.Success(mapValues, System.Net.HttpStatusCode.OK);
        }

        public async Task<ServiceResult<CategoryWithProductsDTO>> TGetCategoryWithProductsAsync(int id)
        {
            var findCategory = await _cRepository.GetValueByIdAsync(id);
            if(findCategory is null)
            {
                return ServiceResult<CategoryWithProductsDTO>.Fail("İlgili Kategori Bulunamadı!", HttpStatusCode.NotFound);
            }
            var mapValue = _mapper.Map<CategoryWithProductsDTO>(findCategory);
            return ServiceResult<CategoryWithProductsDTO>.Success(mapValue, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDTO>>> TGetCategoryWithProductsAsync()
        {
            var categoryWithProducts=await _cRepository.GetCategoryByProducts().ToListAsync();
            var mapValues = _mapper.Map<List<CategoryWithProductsDTO>>(categoryWithProducts);
            return ServiceResult<List<CategoryWithProductsDTO>>.Success(mapValues, HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<CategoryDTO>>> TGetListCategoryAsync()
        {
            var Categories =await _cRepository.GetListAll().ToListAsync();
            var mapValues= _mapper.Map<List<CategoryDTO>>(Categories);
            return ServiceResult<List<CategoryDTO>>.Success(mapValues, System.Net.HttpStatusCode.OK);
        }

        public async Task<ServiceResult> TUpdateCategoryAsync(UpdateCategoryDTO p)
        {
            var findCategory=await _cRepository.GetValueByIdAsync(p.Id);
            bool anyCategory = await _cRepository.GetByFilter(x => x.Name == p.Name && x.Id != p.Id).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult.Fail("Veri Tabanında Aynı İsimde Kategori Bulunuyor");
            }
            var mapValue = _mapper.Map(p, findCategory);
            _cRepository.Update(mapValue!);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }
    }
}
