using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Sample.Domain.Events;


namespace PMDEvers.CQRS.Sample.Domain
{
    public class SampleAggregate : AggregateRoot
    {
        public SampleAggregate()
        {
            
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<SampleCreated>(Apply);
        }

        private void Apply(SampleCreated obj)
        {
            Id = obj.AggregateId;
        }

        protected override void Create()
        {
            ApplyChange(new SampleCreated(Id));
        }
    }
}
