using System;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS
{
    public abstract class AggregateChild
    {
        private readonly object _collection;
        private Dictionary<Type, Action<ChildEventBase>> _eventAppliers = new Dictionary<Type, Action<ChildEventBase>>();
        
        public AggregateChild(AggregateChildCollection collection)
        {
            Id = Guid.NewGuid();
            _collection = collection;
        }

        public Guid Id { get; protected set; }

        protected abstract void RegisterAppliers();

        protected void RegisterApplier<TEvent>(Action<TEvent> applier) where TEvent : ChildEventBase
        {
            _eventAppliers.Add(typeof(TEvent), x => applier((TEvent)x));
        }

        public bool Apply(ChildEventBase @event)
        {
            Type eventType = @event.GetType();

            if (!_eventAppliers.ContainsKey(eventType))
                return false;

            _eventAppliers[eventType](@event);
            return true;
        }
    }
}
