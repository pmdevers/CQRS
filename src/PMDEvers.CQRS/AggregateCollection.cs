using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS
{
    public abstract class AggregateChildCollection
    {
        
    }

    public class AggregateChildCollection<T> : AggregateChildCollection, IReadOnlyCollection<T>
        where T : AggregateChild
    {
        
        private Dictionary<Guid, T> _children = new Dictionary<Guid, T>();
        public AggregateRoot Parent { get; private set; }
        
        public void ApplyChange(EventBase e)
        {
            Parent?.ApplyChange(e);
        }


        public void Initialize(AggregateRoot parent)
        {
            Parent = parent;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; set; }

        public void Apply(ChildEventBase e)
        {
            
            _children[e.ChildId]?.Apply(e);
        }
    }
}
