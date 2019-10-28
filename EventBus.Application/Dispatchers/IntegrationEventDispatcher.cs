using EventBus.Infra.EventBus.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Bus
{
    public class IntegrationEventDispatcher
    {
        private readonly IEventBus _queue;

        public IntegrationEventDispatcher(IEventBus queue)
        {
            _queue = queue;
        }
    }
}
