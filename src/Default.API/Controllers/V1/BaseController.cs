using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Default.ApplicationServices.VIewModels.Generic;
using Default.Core.Events.Notifications.Handlers;
using Default.Domain.DTOs.Core.Events;

namespace Default.API.Controllers.V1;

public abstract class BaseController(INotificationHandler<DomainNotification> handler) : ControllerBase
{
    private readonly DomainNotificationHandler _handler = handler as DomainNotificationHandler;

    protected new IActionResult Response(string message = "", HttpStatusCode code = HttpStatusCode.OK)
        => Response(null, message, code);

    protected new IActionResult Response(dynamic data, string message = "", HttpStatusCode code = HttpStatusCode.OK)
    {
        if (HasNotifications())
            return GetNotificationResponse();

        return StatusCode((int)code, new ResponseViewModel<object>
        {
            Data = data,
            Message = message,
        });
    }

    private bool HasNotifications()
        => _handler.HasNotifications();

    private IActionResult GetNotificationResponse()
    {
        var notification = _handler.GetFirstNotification();

        return notification.DomainNotificationType switch
        {
            DomainNotificationType.Unauthorized
                => Unauthorized(new ResponseViewModel<object>(notification.Message)),
            
            DomainNotificationType.Forbidden
                => StatusCode(403, new ResponseViewModel<object>(notification.Message)),
            
            DomainNotificationType.UnprocessableEntity
                => StatusCode(422, new ResponseViewModel<object>(notification.Message)),
            
            DomainNotificationType.NotFound
                => NotFound(new ResponseViewModel<object>(notification.Message)),
            
            DomainNotificationType.AlreadyExists or DomainNotificationType.ValidationError
                => BadRequest(new ResponseViewModel<object>(notification.Message)),
            
            _ => StatusCode(204)
        };
    }
}