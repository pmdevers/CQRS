using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Sample.Domain.Commands;
using PMDEvers.CQRS.Sample.Domain.Events;

namespace PMDEvers.CQRS.Sample.Domain
{
    public class SampleAggregate : AggregateRoot
    {
        private AggregateChildCollection<AggregateChild> _collection = new AggregateChildCollection<AggregateChild>();
        public static SampleAggregate Create()
        {
            var a = new SampleAggregate();
            a.ApplyChange(new SampleCreated(a.Id));
            return a;
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<SampleCreated>(Apply);
            RegisterApplier<ChildEventBase>(_collection.Apply);
        }

        private void Apply(SampleCreated obj)
        {
            Id = obj.AggregateId;
            _collection.Initialize(this);
        }
    }
}
