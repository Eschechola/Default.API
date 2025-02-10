using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Otanimes.Core.Events.Notifications.Facades;
using Otanimes.Core.Events.Notifications.Handlers;
using Otanimes.Core.Events.Notifications.Publishers;
using Otanimes.Domain.DTOs.Core.Events;
using Otanimes.Domain.Interfaces.Core.Events;

namespace Otanimes.IoC.Middlewares;

public static class MediatorMiddleware
{
    public static IServiceCollection AddMediatorHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDomainNotificationPublisher, DomainNotificationPublisher>();
        services.AddScoped<IDomainNotificationFacade, DomainNotificationFacade>();
        
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        return services;
    }
}