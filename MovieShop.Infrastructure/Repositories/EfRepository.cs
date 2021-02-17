using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;
        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual T GetByIdAsync(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            return entity;
        }
        public virtual IEnumerable<T> ListAllAsync()
        {
            return _dbContext.Set<T>().ToList();
        }
        public virtual IEnumerable<T> ListAsync(Expression<Func<T, bool>> filter)
        {
            var filteredList = _dbContext.Set<T>().Where(filter).ToList();
            return filteredList;
        }
        public virtual int GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbContext.Set<T>().Where(filter).Count();
            }
            return _dbContext.Set<T>().Count();
        }
        public virtual bool GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbContext.Set<T>().Where(filter).Any();
            }
            return false;
        }
        public virtual T AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public virtual T UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }
        public virtual T DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
