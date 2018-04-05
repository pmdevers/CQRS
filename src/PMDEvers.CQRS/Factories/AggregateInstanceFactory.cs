using System;

namespace PMDEvers.CQRS.Factories
{
    public delegate object AggregateInstanceFactory(Type serviceType);
}
