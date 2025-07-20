using App.Domain.Entities;

namespace App.Application.Contracts.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        Task<Category?> GetCategoryWithProductsAsync(int id);
        Task<List<Category>> GetCategoryWithProductsAsync();
       IQueryable<Category> GetCategoryWithProducts();
    }
}
