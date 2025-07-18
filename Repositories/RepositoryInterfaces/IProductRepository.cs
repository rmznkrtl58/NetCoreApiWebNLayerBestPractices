using App.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.RepositoryInterfaces
{
    public interface IProductRepository:IGenericRepository<Product,int>
    {
        Task<List<Product>> GetTopPriceProductsAsync();
    }
}
