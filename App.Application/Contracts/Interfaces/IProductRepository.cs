using App.Domain.Entities;

namespace App.Application.Contracts.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        Task<List<Product>> GetTopPriceProductsAsync();
        Task<List<Product>> GetPagedProductListAsync(int pageNumber, int pageSize);
    }
}
