using EventBus.Infra.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Infra.EventBus.Bus
{
    public interface IEventBus : IDisposable
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        void Subscribe<TEvent>() where TEvent : IEvent;
        void Subscribe<TEvent>(string name) where TEvent : IEvent;
        void Unsubscribe<TEvent>() where TEvent : IEvent;
        void Unsubscribe<TEvent>(string name) where TEvent : IEvent;
        void CreateQueue<TEvent>() where TEvent : IEvent;
    }
}
