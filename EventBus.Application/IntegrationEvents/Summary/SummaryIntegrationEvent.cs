using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Events.Summary
{
    public class SummaryIntegrationEvent : IIntegrationEvent
    {
        public SummaryIntegrationEvent(int id, long quantity, decimal total)
        {
            Id = id;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; private set; }

        public long Quantity { get; private set; }
        public decimal Total { get; private set; }
    }
}
