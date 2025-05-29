using BookStore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.DataAccess.Abstract;

public interface IGenericDal<T> where T : class, IEntity, new()
{
    Task<T> GetAsync(Expression<Func<T,bool>> filter=null, params Expression<Func<T, object>>[] includeProperties);
    Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null,params Expression<Func<T, object>>[] includeProperties);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T> FindByIdAsync(int id);
    Task<bool> AnyAsync(Expression<Func<T,bool>> predicate);
    Task<int> MaxAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> colum);
    Task<int> MinAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> colum);
}
