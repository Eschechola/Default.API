using System.Threading.Tasks;
using Otanimes.Domain.DTOs.Core.Events;

namespace Otanimes.Domain.Interfaces.Core.Events;

public interface IDomainNotificationPublisher
{
    Task PublishNotificationAsync<TNotification>(TNotification notification) 
        where TNotification : Notification;
}