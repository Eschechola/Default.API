using MediatR;
using System.Threading.Tasks;
using Otanimes.Domain.DTOs.Core.Events;
using Otanimes.Domain.Interfaces.Core.Events;

namespace Otanimes.Core.Events.Notifications.Publishers;

public class DomainNotificationPublisher(IMediator mediator) : IDomainNotificationPublisher
{
    public async Task PublishNotificationAsync<TNotification>(TNotification notification) 
        where TNotification : Notification
        => await mediator.Publish(notification);
}