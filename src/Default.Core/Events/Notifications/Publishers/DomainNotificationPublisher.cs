using MediatR;
using System.Threading.Tasks;
using Default.Domain.DTOs.Core.Events;
using Default.Domain.Interfaces.Core.Events;

namespace Default.Core.Events.Notifications.Publishers;

public class DomainNotificationPublisher(IMediator mediator) : IDomainNotificationPublisher
{
    public async Task PublishNotificationAsync<TNotification>(TNotification notification) 
        where TNotification : Notification
        => await mediator.Publish(notification);
}