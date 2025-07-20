using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Contracts.Caching
{
    public interface ICacheService
    {
        Task<T?> TGetListAllAsync<T>(string cacheKey);
        Task TCreateAsync<T>(string cacheKey, T t,TimeSpan cacheTime);
        Task TRemoveAsync(string cacheKey);
    }
}
