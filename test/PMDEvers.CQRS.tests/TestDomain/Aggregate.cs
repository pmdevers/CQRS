using PMDEvers.CQRS.tests.TestDomain.Events;

namespace PMDEvers.CQRS.tests.TestDomain
{
    public class Aggregate : AggregateRoot
    {
        
        protected override void RegisterAppliers()
        {
            RegisterApplier<CreatedEvent>(OnCreated);    
        }

        protected override void Create()
        {
            ApplyChange(new CreatedEvent(Id));
        }

        private void OnCreated(CreatedEvent e)
        {
            Id = e.AggregateId;
        }
    }
}
