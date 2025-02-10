namespace Default.Domain.DTOs.Core.Events;

public record DomainNotification : Notification
{
    public string Message { get; private set; }
    public DomainNotificationType DomainNotificationType { get; private set; }

    public DomainNotification(string message, DomainNotificationType domainNotificationType)
    {
        Message = message;
        DomainNotificationType = domainNotificationType;
    }
}