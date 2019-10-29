using System;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.tests.TestDomain.Events
{
    public class CreatedEvent : EventBase
    {
        public CreatedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
