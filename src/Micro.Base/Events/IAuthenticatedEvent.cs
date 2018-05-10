using System;

namespace Micro.Base.Events
{
    public class IAuthenticatedEvent : IEvent
    {
        Guid UserId { get; set; }

    }
}