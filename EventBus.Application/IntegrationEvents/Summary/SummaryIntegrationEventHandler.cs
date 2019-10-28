using EventBus.Infra.EventBus.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Events.Summary
{
    public class SummaryIntegrationEventHandler : IIntegrationEventHandler<SummaryIntegrationEvent>
    {
        private readonly IEventBus _bus;

        public SummaryIntegrationEventHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public void Send(SummaryIntegrationEvent @event)
        {
            _bus.Publish(@event);
        }
    }
}
