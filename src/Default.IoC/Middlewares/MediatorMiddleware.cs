using Default.Core.Events.Notifications.Facades;
using Default.Core.Events.Notifications.Handlers;
using Default.Core.Events.Notifications.Publishers;
using Default.Domain.DTOs.Core.Events;
using Default.Domain.Interfaces.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Default.IoC.Middlewares;

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