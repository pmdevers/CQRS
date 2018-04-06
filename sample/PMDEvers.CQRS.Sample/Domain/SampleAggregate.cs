using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PMDEvers.CQRS.Sample.Domain.Commands;
using PMDEvers.CQRS.Sample.Domain.Events;

namespace PMDEvers.CQRS.Sample.Domain
{
    public class SampleAggregate : AggregateRoot
    {

        public SampleAggregate()
        {
            ApplyChange(new SampleCreated(Id));
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<SampleCreated>(Apply);
        }

        private void Apply(SampleCreated obj)
        {
            Id = obj.AggregateId;
        }
    }
}
