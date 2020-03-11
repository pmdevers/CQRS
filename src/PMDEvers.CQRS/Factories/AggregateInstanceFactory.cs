using System;

namespace PMDEvers.CQRS.Factories
{
    public delegate object AggregateInstanceFactory(Type serviceType);

    public delegate string UsernameAccessor();
}
