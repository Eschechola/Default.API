using System.Threading.Tasks;
using Default.Domain.DTOs.Core.Events;

namespace Default.Domain.Interfaces.Core.Events;

public interface IDomainNotificationPublisher
{
    Task PublishNotificationAsync<TNotification>(TNotification notification) 
        where TNotification : Notification;
}