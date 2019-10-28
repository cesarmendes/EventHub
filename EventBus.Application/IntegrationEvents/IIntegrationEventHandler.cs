using EventBus.Infra.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Events
{
    public interface IIntegrationEventHandler<in TEvent> : IEventHandler<TEvent>
        where TEvent : IIntegrationEvent
    {
    }
}
