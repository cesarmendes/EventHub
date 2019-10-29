using EventBus.Infra.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Infra.EventBus.Bus
{
    public interface IEventBus : IDisposable
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
        void Unsubscribe<TEvent>() where TEvent : IEvent;
    }
}
