using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T : class // Generic constraint
    {
        //Reading
       T GetByIdAsync(int id);
        IEnumerable<T> ListAllAsync();
        // LINQ list of movies on some where condition where m.title = "Avengere", m.revenue > 10000000
        IEnumerable<T> ListAsync(Expression<Func<T, bool>> filter);
        int GetCountAsync(Expression<Func<T, bool>> filter = null);
        bool GetExistsAsync(Expression<Func<T, bool>> filter = null);
        // C Creating
        T AddAsync(T entity);
        // U Update
        T UpdateAsync(T entity);
        // D Delete
        T DeleteAsync(T entity);
    }
}
