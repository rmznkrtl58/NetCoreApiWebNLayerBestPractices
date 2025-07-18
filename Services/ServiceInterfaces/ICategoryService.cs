using App.Repositories.Entities;
using App.Services.DTOs.CategoryDTOs;
using App.Services.Results;

namespace App.Services.ServiceInterfaces
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
