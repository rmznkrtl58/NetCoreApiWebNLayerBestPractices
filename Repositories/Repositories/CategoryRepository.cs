using App.Repositories.Context;
using App.Repositories.Entities;
using App.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories
{
    public class CategoryRepository : GenericRepository<Category,int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext _context) : base(_context)
        {
        }
        //IQuerybale geçmemin sebebi belki orderby yapılır veya take metodu kullanıp listenin 3tanesi seçilir service katmanında oyuzden querybale geçiyorum. ikinci bir olayda burda task metod kullanmıyorum zaten servicede tolistAsync kullanacağımdan dolayı burayı task yapmıyorum
        IQueryable<Category> ICategoryRepository.GetCategoryByProducts()
        {
            var values = _context.Categories.Include(x => x.Products).AsQueryable();
            return values;
        }

        async Task<Category?> ICategoryRepository.GetCategoryWithProductsAsync(int id)
        {
            //Category id'si parametremden gelen id değerine eşit olan categorynin ürünlerini getir.
            var value = await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
            return value;
        }
    }
}
