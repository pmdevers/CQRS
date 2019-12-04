using System;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.tests.TestDomain.Children.Events
{
    public class ChildCreated : ChildEventBase
    {
        public ChildCreated(Guid aggregateId, Guid childId) : base(aggregateId, childId)
        {
        }
    }
}
