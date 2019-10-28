using EventBus.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Domain.Entities
{
    public class Summary : Entity<int>
    {
        public Guid ProductGuid { get; set; }
        public long Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
