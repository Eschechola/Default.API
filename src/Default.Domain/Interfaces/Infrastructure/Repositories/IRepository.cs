using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Default.Domain.Entities;
using Default.Domain.Structs;

namespace Default.Domain.Interfaces.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    IUnitOfWork UnitOfWork { get; }
    
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> query);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? query = null);
    Task<TEntity> SingleAsync();
    Task<TEntity> GetByIdAsync(Guid id, string includeProperties = "", bool noTracking = true);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>>? query = null,
        string includeProperties = "", bool noTracking = true);
    Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? query = null, string includeProperties = "",
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool noTracking = true);
    
    Task<Paginated<TEntity>> GetAllPaginatedAsync(
        Expression<Func<TEntity, bool>>? query = null, string includeProperties = "",
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool noTracking = true,
        int page = 1, int itemsPerPage = 10);
}