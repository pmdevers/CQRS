using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Sample.Domain.Events;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.Sample.Read.Handlers
{
    public class SampleCreatedHandler : ICancellableAsyncEventHandler<SampleCreated>
    {
        public Task HandleAsync(SampleCreated @event, CancellationToken cancellationToken)
        {
            
        }
    }
}
