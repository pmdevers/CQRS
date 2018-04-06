using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.Sample.Domain.Events
{
    public class SampleCreated : EventBase
    {
        public SampleCreated(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
