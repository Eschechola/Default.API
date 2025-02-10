using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Default.Domain.DTOs.Core.Events;

namespace Default.Core.Events.Notifications.Handlers;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private readonly ICollection<DomainNotification> _notifications;

    public IReadOnlyCollection<DomainNotification> Notifications 
        => _notifications as List<DomainNotification>;

    public DomainNotificationHandler()
    {
        _notifications ??= new List<DomainNotification>();
    }

    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);
        return Task.CompletedTask;
    }

    public bool HasNotifications()
        => _notifications.Count > 0;

    public DomainNotification GetFirstNotification()
        => _notifications.FirstOrDefault();
}