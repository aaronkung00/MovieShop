using Microsoft.EntityFrameworkCore;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _deContext;
        public EfRepository(MovieShopDbContext deContext) 
        {
            _deContext = deContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _deContext.Set<T>().FindAsync(id);
            return entity;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _deContext.Set<T>().AddAsync(entity);
            await _deContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            // Connected entities and diconnected
            // var test = new Movie { title = "abc" }
            // _dbcontext.add(test);
            // _dbContext.SaveChange();

            // Updating ---- Connected method
            // var dbMovie = dbContext.Movies.Find(23);
            // dbMovie.Revenue = 20000;
            // dbContext.Update();
            // _dbContext.SaveChange();

            _deContext.Entry(entity).State = EntityState.Modified;
            await _deContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _deContext.Set<T>().Remove(entity);
            await _deContext.SaveChangesAsync();
        }


        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
           if(filter != null)
            {
                return await _deContext.Set<T>().Where(filter).CountAsync();
            }

            return await _deContext.Set<T>().CountAsync();
        }

        public async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter)
        {
            if (filter != null && await _deContext.Set<T>().Where(filter).AnyAsync())
                return true;
            return false;
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _deContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _deContext.Set<T>().Where(filter).ToListAsync();
        }

 
    }
}
