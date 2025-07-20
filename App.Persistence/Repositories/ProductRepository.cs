using App.Application.Contracts.Interfaces;
using App.Domain.Entities;
using App.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
    public class ProductRepository(AppDbContext _context) : GenericRepository<Product,int>(_context), IProductRepository
    {
        //Aşağıdaki yapının farklı kullanımı yukarda parantez içerisinde
        //ctor kullanımının farklı kullanımı!
        //public ProductRepository(AppDbContext _context) : base(_context)
        //{
        //}

        public async Task<List<Product>> GetPagedProductListAsync(int pageNumber, int pageSize)
        {
            //1.sayfada 10 veri listeleme=>(1-0)*10
            //2.sayfada 10 veri listeleme=>(2-1)*10
            var calculate = (pageNumber - 1) * pageSize;
            var productList=await _context.Products.ToListAsync();
            var pagination =productList.Skip(calculate).Take(pageSize).ToList();
            return pagination;
        }
        Task<List<Product>> IProductRepository.GetTopPriceProductsAsync()
        {
            return _context.Products.OrderByDescending(x => x.Price).ToListAsync();
        }
    }
}
