using System;

namespace PMDEvers.CQRS.Events
{
    public abstract class ChildEventBase : EventBase
    {
        protected ChildEventBase(Guid aggregateId, Guid childId) : base(aggregateId)
        {
            ChildId = childId;
        }

        public Guid ChildId { get; }
    }
}
