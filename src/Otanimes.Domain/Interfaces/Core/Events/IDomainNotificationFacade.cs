using System;
using System.Threading.Tasks;

namespace Otanimes.Domain.Interfaces.Core.Events;

public interface IDomainNotificationFacade
{
    Task PublishInvalidCredentialsAsync();
    Task PublishForbiddenAsync(string validationMessage);
    Task PublishValidationErrorAsync(string validationMessage);
    Task PublishUnprocessableEntityAsync(string entityName, string errors);
    Task PublishNotFoundAsync(string entityName);
    Task PublishNotFoundAsync(string entityName, Guid entityId);
    Task PublishAlreadyExistsAsync(string entityName);
    Task PublishAlreadyExistsAsync(string entityName, Guid entityId);
}