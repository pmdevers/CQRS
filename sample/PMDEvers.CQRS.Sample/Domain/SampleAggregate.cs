using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Sample.Domain.Events;
using PMDEvers.CQRS.Sample.Domain.Services;


namespace PMDEvers.CQRS.Sample.Domain
{
    public class SampleAggregate : AggregateRoot
    {
        private readonly IExampleService _service;

        public string Value { get; private set; }

        public SampleAggregate(IExampleService service)
        {
            _service = service;
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<SampleCreated>(Apply);
        }

        private void Apply(SampleCreated obj)
        {
            Id = obj.AggregateId;
            Value = obj.value;
        }

        protected override void Create()
        {
            ApplyChange(new SampleCreated(Id)
            {
                value = _service.GetValue()
            });
        }
    }
}
