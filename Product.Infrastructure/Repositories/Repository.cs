using Microsoft.EntityFrameworkCore;
using Product.Application.Repositories;
using Product.Infrastructure.Data;
using System.Linq.Expressions;
using System.Linq;
using Product.Domain.Common;

namespace Product.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    #region Ctor
    protected readonly ApplicationDbContext _db;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<TEntity>();
    }

    #endregion



    #region Normal

    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("داده معتبر نمی باشد");
        }

        _dbSet.Update(entity);
    }

    public void Delete(object id)
    {
        TEntity entity = GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("داده معتبر نمی باشد");
        }

        _dbSet.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Delete(Expression<Func<TEntity, bool>> expression)
    {
        var entities = GetMany(expression);
        if (!entities.Any())
        {
            throw new ArgumentException("داده معتبر نمی باشد");
        }
        _dbSet.RemoveRange(entities);
    }

    public TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }
    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity Get(Expression<Func<TEntity, bool>>? where = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }
        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        return query.FirstOrDefault();
    }

    public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }
        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        if (orderby != null)
        {
            return orderby(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public IEnumerable<TEntity> Take(int take, Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }
        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        if (orderby != null)
        {
            return orderby(query).Take(take).ToList();
        }
        else
        {
            return query.Take(take).ToList();
        }
    }

    public bool Any()
    {
        return _dbSet.Any();
    }

    #endregion


    #region Async
    public Task InsertAsync(TEntity entity)
    {
        _dbSet.AddAsync(entity);
        return Task.CompletedTask;
    }
    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (string item in includs)
        {
            query = query.Include(item);
        }

        if (orderby != null)
        {
            return await orderby(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? where = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }

        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity> GetAsyncNoTracking(Expression<Func<TEntity, bool>>? where = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }

        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }
        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        if (orderby != null)
        {
            return await orderby(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<IEnumerable<TEntity>> TakeAsync(int take, Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }
        var includs = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in includs)
        {
            query = query.Include(item);
        }

        if (orderby != null)
        {
            return await orderby(query).Take(take).ToListAsync();
        }
        else
        {
            return await query.Take(take).ToListAsync();
        }
    }


    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
    {
        return await _dbSet.AnyAsync(where);
    }



    #endregion


    #region Dispose
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    ~Repository()
    {
        Dispose(false);
    }
    #endregion
}
