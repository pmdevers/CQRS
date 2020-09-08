using System;
using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.Sample.Domain.Events
{
    [Serializable]
    public class SampleCreated : EventBase
    {
        public SampleCreated(Guid sampleId) : base(sampleId)
        {
        }
    }
}
