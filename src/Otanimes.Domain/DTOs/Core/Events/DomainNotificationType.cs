namespace Otanimes.Domain.DTOs.Core.Events;

public enum DomainNotificationType
{
    Unauthorized,
    Forbidden,
    NotFound,
    AlreadyExists,
    ValidationError,
    UnprocessableEntity
}