

using App.Application.Features.DTOs.CategoryDTOs;
using App.Application.Results;
using App.Domain.Entities;

namespace App.Application.Features.ServiceInterfaces
{
    public interface ICategoryService:IGenericService<Category>
    {
        Task<ServiceResult<int>> TCreateCategoryAsync(CreateCategoryDTO p);
        Task<ServiceResult> TUpdateCategoryAsync(UpdateCategoryDTO p);
        Task<ServiceResult> TDeleteCategoryAsync(int id);
        Task<ServiceResult<CategoryWithProductsDTO>> TGetCategoryWithProductsAsync(int id);
        Task<ServiceResult<List<CategoryWithProductsDTO>>> TGetCategoryWithProductsAsync();
        Task<ServiceResult<CategoryDTO>> TGetCategoryByIdAsync(int id);
        Task<ServiceResult<List<CategoryDTO>>> TGetListCategoryAsync();
    }
}
