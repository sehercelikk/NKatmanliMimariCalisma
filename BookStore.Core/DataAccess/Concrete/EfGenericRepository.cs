using BookStore.Core.DataAccess.Abstract;
using BookStore.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.DataAccess.Concrete;

public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class, IEntity, new()
{
    protected readonly DbContext _context;
    public EfGenericRepository(DbContext context)
    {
        _context = context;
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate); 
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
        return entity;
    }

    public async Task<TEntity> FindByIdAsync(int id) => await _context.FindAsync<TEntity>(id);

    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {

                    query = query.Include(includeProperty);
                }
            }
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
        }
        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<int> MaxAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> colum)
    {
        return await _context.Set<TEntity>().Where(predicate).MaxAsync(colum);
    }

    public async Task<int> MinAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> colum)
    {
        return await _context.Set<TEntity>().Where(predicate).MinAsync(colum);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
        return entity;
    }
}
