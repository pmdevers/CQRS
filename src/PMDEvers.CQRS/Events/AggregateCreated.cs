using System;
using System.Collections.Generic;
using System.Text;

namespace PMDEvers.CQRS.Events
{
    public class AggregateCreated : EventBase
    {
        public AggregateCreated(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}
