using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Default.Domain.Entities;
using Default.Domain.Interfaces.Infrastructure;
using Default.Domain.Interfaces.Infrastructure.Repositories;
using Default.Domain.Structs;
using Default.Infrastructure.Context;

namespace Default.Infrastructure.Repositories;

public abstract class Repository<TEntity>(DefaultContext context) : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public abstract IUnitOfWork UnitOfWork { get; }

    public void Create(TEntity entity)
        => _dbSet.Add(entity);

    public void CreateRange(IEnumerable<TEntity> entities)
        => _dbSet.AddRange(entities);

    public void Update(TEntity entity)
        => context.Entry(entity).State = EntityState.Modified;

    public void RemoveRange(IEnumerable<TEntity> entities)
        => _dbSet.RemoveRange(entities);

    public void Remove(TEntity entity)
        => _dbSet.Remove(entity);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> query)
        => await _dbSet.AnyAsync(query);

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? query = null)
        => query is null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(query);

    public async Task<TEntity> SingleAsync()
        => await _dbSet.SingleAsync();

    public async Task<TEntity> GetByIdAsync(Guid id, string includeProperties = "", bool noTracking = true)
    {
        var query = _dbSet as IQueryable<TEntity>;

        query = ApplyQueryIncludedProperties(query, includeProperties);
        query = query.Where(entity => entity.Id == id);

        return noTracking
            ? await query.AsNoTracking().FirstOrDefaultAsync()
            : await query.AsTracking().FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
        => await ExistsAsync(entity => entity.Id == id);

    public async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>>? query = null, string includeProperties = "",
        bool noTracking = true)
        => noTracking
            ? await BuildQuery(query, includeProperties).AsNoTracking().FirstOrDefaultAsync()
            : await BuildQuery(query, includeProperties).FirstOrDefaultAsync();

    public async Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? query = null, string includeProperties = "",
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool noTracking = true)
        => noTracking
            ? await BuildQuery(query, includeProperties, orderBy).AsNoTracking().ToListAsync()
            : await BuildQuery(query, includeProperties, orderBy).ToListAsync();
    
    public async Task<Paginated<TEntity>> GetAllPaginatedAsync(
        Expression<Func<TEntity, bool>>? query = null, string includeProperties = "",
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool noTracking = true,
        int page = 1, int itemsPerPage = 10)
    {
        var queryable = BuildQuery(query, includeProperties, orderBy);

        var totalItemsCount = await CountAsync(query);
        itemsPerPage = FormatItemsPerPage(itemsPerPage, totalItemsCount);

        var pagesCount = GetPagesCount(totalItemsCount, itemsPerPage);
        page = FormatPage(page, pagesCount);

        queryable = SkipItems(queryable, page, itemsPerPage);
        queryable = TakeItems(queryable, totalItemsCount, itemsPerPage);

        var items = noTracking
            ? await queryable.AsNoTracking().ToListAsync()
            : await queryable.ToListAsync();

        return new Paginated<TEntity>(
            page: page,
            pagesCount: pagesCount,
            itemsPerPage: itemsPerPage,
            totalItemsCount: totalItemsCount,
            items: items);
    }

    private IQueryable<TEntity> TakeItems(IQueryable<TEntity> queryable, int totalItemsCount, int itemsPerPage)
        => totalItemsCount < itemsPerPage
            ? queryable
            : queryable.Take(itemsPerPage);

    private IQueryable<TEntity> SkipItems(IQueryable<TEntity> queryable, int page, int itemsPerPage)
        => page < 1
            ? queryable
            : queryable.Skip((page - 1) * itemsPerPage);

    private int FormatPage(int page, int pagesCount)
    {
        if (page < 1)
            return 1;

        return page > pagesCount
            ? pagesCount
            : page;
    }

    private int FormatItemsPerPage(int itemsPerPage, int totalItemsCount)
    {
        if (itemsPerPage > totalItemsCount)
            return totalItemsCount;

        return itemsPerPage < 1
            ? 1
            : itemsPerPage;
    }

    private int GetPagesCount(int totalItemsCount, int itemsPerPage)
    {
        if (itemsPerPage == 0 || totalItemsCount == 0)
            return 0;

        var pagesCount = Convert.ToDouble(totalItemsCount) / Convert.ToDouble(itemsPerPage);

        return pagesCount < 1 ? 1 : (int)Math.Ceiling(pagesCount);
    }

    private IQueryable<TEntity> BuildQuery(
        Expression<Func<TEntity, bool>>? predicate = null, string includeProperties = "",
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        var query = _dbSet as IQueryable<TEntity>;

        query = ApplyQueryPredicate(query, predicate);
        query = ApplyQueryIncludedProperties(query, includeProperties);
        query = ApplyOrderBy(query, orderBy);

        return query;
    }

    private IQueryable<TEntity> ApplyQueryPredicate(IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? predicate)
        => predicate != null
            ? query.Where(predicate)
            : query;

    private IQueryable<TEntity> ApplyQueryIncludedProperties(IQueryable<TEntity> query, string includeProperties)
    {
        if (string.IsNullOrEmpty(includeProperties))
            return query;

        foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(property.Trim());

        return query;
    }

    private IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy)
    {
        if (orderBy != null)
            query = orderBy(query);

        return query;
    }
}