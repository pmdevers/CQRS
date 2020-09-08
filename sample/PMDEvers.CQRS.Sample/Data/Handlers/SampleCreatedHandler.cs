using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Sample.Domain;
using PMDEvers.CQRS.Sample.Domain.Events;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.Sample.Data.Handlers
{
    public class SampleCreatedHandler : IAsyncEventHandler<SampleCreated>
    {
        public Task HandleAsync(SampleCreated @event)
        {
            return Task.CompletedTask;
        }
    }
}
