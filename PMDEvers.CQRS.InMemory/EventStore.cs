using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS.InMemory
{
    public class EventStore : IEventStore
    {
        private readonly List<EventBase> _events;

        public EventStore()
        {
            _events = new List<EventBase>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

        }

        public Task SaveAsync(EventBase @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            _events.Add(@event);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<EventBase>> FindByIdAsync(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(_events.Where(x => x.AggregateId == aggregateId && x.Version >= fromVersion));
        }

        public Task<IEnumerable<EventBase>> FindByHistoryAsync(Guid aggregateId, int tillVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(_events.Where(x => x.AggregateId == aggregateId && x.Version <= tillVersion));
        }

        public Task<IEnumerable<EventBase>> FindAllAsync(DateTime tillDate, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(_events.Where(x => x.Timestamp <= tillDate));
        }
    }
}
