using System;
using MediatR;

namespace Default.Domain.DTOs.Core.Events;

public abstract record Notification : INotification
{
    public Guid Hash { get; private set; } = Guid.NewGuid();
    public DateTime PublishedAt { get; private set; } = DateTime.Now;
}