using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Infra.EventBus.Events
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        void Send(TEvent @event);
    }
}
