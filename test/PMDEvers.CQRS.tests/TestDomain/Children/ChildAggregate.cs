using System;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.tests.TestDomain.Children.Events;

namespace PMDEvers.CQRS.tests.TestDomain.Children
{
    public class ChildAggregate : AggregateChild
    {
        public ChildAggregate(AggregateChildCollection collection) : base(collection)
        {
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<ChildCreated>(OnCreated);    
        }

        private void OnCreated(ChildCreated e)
        {
            Id = e.ChildId;
        }
    }
}
