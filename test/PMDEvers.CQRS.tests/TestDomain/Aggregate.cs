using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.tests.TestDomain.Children;
using PMDEvers.CQRS.tests.TestDomain.Children.Events;
using PMDEvers.CQRS.tests.TestDomain.Events;

namespace PMDEvers.CQRS.tests.TestDomain
{
    public class Aggregate : AggregateRoot
    {
        private AggregateChildCollection<ChildAggregate> _children = new AggregateChildCollection<ChildAggregate>();
        
        public static Aggregate Create()
        {
            var a = new Aggregate();
            a.ApplyChange(new CreatedEvent(a.Id));
            return a;
        }

        public void AddChild()
        {
            var e = new ChildAggregate(_children);
            _children.Add(e);
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<CreatedEvent>(OnCreated);
            RegisterApplier<ChildEventBase>(_children.Apply);
        }

        private void OnCreated(CreatedEvent e)
        {
            Id = e.AggregateId;
            _children.Initialize(this);
        }
    }
}
