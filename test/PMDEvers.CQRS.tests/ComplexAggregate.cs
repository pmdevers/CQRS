using System;

namespace PMDEvers.CQRS.tests
{
    public class ComplexAggregate : AggregateRoot
    {
        public IServiceProvider ServiceProvider { get; }

        public ComplexAggregate(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected override void RegisterAppliers()
        {
            
        }

        protected override void Create()
        {
            
        }
    }
}
