using PMDEvers.CQRS.tests.TestDomain.Events;

namespace PMDEvers.CQRS.tests.TestDomain
{
    public class Aggregate : AggregateRoot
    {
        public static Aggregate Create()
        {
            var a = new Aggregate();
            a.ApplyChange(new CreatedEvent(a.Id));
            return a;
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<CreatedEvent>(OnCreated);    
        }

        private void OnCreated(CreatedEvent e)
        {
            Id = e.AggregateId;
        }
    }
}
