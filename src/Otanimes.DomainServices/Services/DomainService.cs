using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Otanimes.Domain.Entities;
using Otanimes.Domain.Interfaces;
using Otanimes.Domain.Interfaces.Core.Events;
using Otanimes.Domain.Interfaces.Infrastructure.Repositories;

namespace Otanimes.DomainServices.Services;

public abstract class DomainService(IServiceProvider provider)
{
    protected readonly IDomainNotificationFacade domainNotification = provider.GetService<IDomainNotificationFacade>();
    
    #region Generic
    
    protected async Task<bool> IsInvalidAsync<TEntity>(TEntity entity)
        where TEntity : IAggregateRoot
    {
        entity.Validate();
        
        if (entity.IsValid())
            return false;

        await domainNotification.PublishUnprocessableEntityAsync("Entity", entity.ErrorsToString());
        return true;
    }
    
    protected async Task<bool> NotExistsAsync<TEntity>(Guid id, IRepository<TEntity> repository)
        where TEntity : Entity
    {
        if (await repository.ExistsByIdAsync(id))
            return false;
        
        await domainNotification.PublishNotFoundAsync(typeof(TEntity).Name, id);
        return true;
    }
    
    protected async Task<bool> NotExistsAsync<TEntity>(Guid[] ids, IRepository<TEntity> repository)
        where TEntity : Entity
    {
        foreach (var id in ids)
            if (await NotExistsAsync(id, repository))
                return true;

        return false;
    }
    
    protected async Task<bool> NotFilledOrNotExistsAsync<TEntity>(Guid? id, IRepository<TEntity> repository)
        where TEntity : Entity
        => id is not null &&
           await NotExistsAsync(id.Value, repository);
    
    #endregion
}