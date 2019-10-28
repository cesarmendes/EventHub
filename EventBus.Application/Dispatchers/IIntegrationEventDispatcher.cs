using EventBus.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Bus
{
    public interface IIntegrationEventDispatcher
    {
        void Send<TEvent>(TEvent command)
        where TEvent : IIntegrationEvent;
    }
}
