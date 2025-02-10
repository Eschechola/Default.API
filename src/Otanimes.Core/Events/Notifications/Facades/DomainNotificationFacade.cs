using System;
using System.Threading.Tasks;
using Otanimes.Domain.DTOs.Core.Events;
using Otanimes.Domain.Interfaces.Core.Events;

namespace Otanimes.Core.Events.Notifications.Facades;

public class DomainNotificationFacade(IDomainNotificationPublisher domainNotificationPublisher) 
    : IDomainNotificationFacade
{
     public async Task PublishInvalidCredentialsAsync()
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: "Invalid Credentials, please try again.",
            DomainNotificationType.Unauthorized
        ));

    public async Task PublishValidationErrorAsync(string validationMessage)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: validationMessage,
            DomainNotificationType.ValidationError
        ));

    public async Task PublishForbiddenAsync(string validationMessage)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: validationMessage,
            DomainNotificationType.Forbidden
        ));

    public async Task PublishUnprocessableEntityAsync(string entityName, string errors)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName} is invalid: {errors}.",
            DomainNotificationType.UnprocessableEntity
        ));
    
    public async Task PublishNotFoundAsync(string entityName)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName} was not found.",
            DomainNotificationType.NotFound
        ));

    public async Task PublishNotFoundAsync(string entityName, Guid entityId)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName}: {entityId.ToString()} was not found.",
            DomainNotificationType.NotFound
        ));

    public async Task PublishAlreadyExistsAsync(string entityName)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName} already exists.",
            DomainNotificationType.AlreadyExists
        ));
    
    public async Task PublishAlreadyExistsAsync(string entityName, Guid entityId)
        => await domainNotificationPublisher.PublishNotificationAsync(new DomainNotification(
            message: $"{entityName}: {entityId.ToString()} already exists.",
            DomainNotificationType.AlreadyExists
        ));
}