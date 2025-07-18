

using App.Repositories.Context;
using App.Repositories.Entities;
using App.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Repositories
{
    public class ProductRepository(AppDbContext _context) : GenericRepository<Product,int>(_context), IProductRepository
    {   
        //Aşağıdaki yapının farklı kullanımı yukarda parantez içerisinde
        //ctor kullanımının farklı kullanımı!
        //public ProductRepository(AppDbContext _context) : base(_context)
        //{
        //}

        Task<List<Product>> IProductRepository.GetTopPriceProductsAsync()
        {
            return _context.Products.OrderByDescending(x => x.Price).ToListAsync();
        }
    }
}
