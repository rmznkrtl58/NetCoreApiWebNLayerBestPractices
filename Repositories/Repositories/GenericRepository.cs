using App.Repositories.Context;
using App.Repositories.Entities;
using App.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace App.Repositories.Repositories
{
    public class GenericRepository<T, TId>(AppDbContext _context) : IGenericRepository<T,TId> where T : BaseEntity<TId>where TId:struct
    {
        protected AppDbContext _context;
        private readonly DbSet<T>_dbSet=_context.Set<T>();
        async ValueTask IGenericRepository<T,TId>.CreateAsync(T t)
        {
            await _dbSet.AddAsync(t);
        }

        void IGenericRepository<T,TId>.Delete(T t)
        {
            _dbSet.Remove(t);
        }

         IQueryable<T> IGenericRepository<T, TId>.GetByFilter(Expression<Func<T, bool>> filter)
        {
            var values = _dbSet.Where(filter).AsNoTracking();
            return values;
        }

        IQueryable<T> IGenericRepository<T, TId>.GetListAll()
        {
            var values = _dbSet.AsQueryable().AsNoTracking();
            return values;
        }

        async ValueTask<T?> IGenericRepository<T, TId>.GetValueByIdAsync(int id)
        {
            var findValue=await _dbSet.FindAsync(id);
            return findValue;
        }

        void IGenericRepository<T, TId>.Update(T t)
        {
            _dbSet.Update(t);
        }
        async Task<bool> IGenericRepository<T, TId>.AnyAsync(TId id)
        {
            var anyState = await _dbSet.AnyAsync(x => x.Id.Equals(id));
            return anyState;
        }
    }
}
