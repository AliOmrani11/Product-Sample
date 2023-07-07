using Product.Domain.Common;
using System.Linq.Expressions;

namespace Product.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    void Update(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entity);
    void Delete(Expression<Func<TEntity, bool>> expression);
    TEntity GetById(object id);
    IEnumerable<TEntity> GetAll();
    TEntity Get(Expression<Func<TEntity, bool>>? where = null, string include = "");
    IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "");
    IEnumerable<TEntity> Take(int take, Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "");
    bool Any();

    //-------------------------------------------------------------------------------------

    Task InsertAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "");
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? where = null, string include = "");
    Task<TEntity> GetAsyncNoTracking(Expression<Func<TEntity, bool>>? where = null, string include = "");
    Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "");
    Task<IEnumerable<TEntity>> TakeAsync(int take, Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderby = null, string include = "");
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);
}
