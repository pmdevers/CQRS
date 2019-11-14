using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Interfaces;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS
{
    public class Repository<T> : IRepository<T>
        where T : AggregateRoot
    {
        private readonly ISnapshotStore<T> _snapshotStore;
        private readonly IServiceBus _serviceBus;
        private readonly IEventStore _eventStore;
        

        public Repository(IEventStore eventStore, IServiceBus serviceBus, ISnapshotStore<T> snapshotStore)
        {
            _eventStore = eventStore;
            _serviceBus = serviceBus;
            _snapshotStore = snapshotStore;
        }

        public async Task<T> GetCurrentStateAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var aggregate = _snapshotStore.GetSnapshot(id);
            var events = (await _eventStore.FindByIdAsync(id, aggregate.Version, cancellationToken)).ToList();
            if (!events.Any())
            {
                return null;
            }

            aggregate.LoadFromHistory(events);

            return aggregate;
        }

        public async Task<T> GetStateAsync(Guid id, int expectedVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            var events = await _eventStore.FindByHistoryAsync(id, expectedVersion, cancellationToken);
            if (!events.Any())
            {
                return null;
            }

            var aggregate = Activator.CreateInstance<T>();
            aggregate.LoadFromHistory(events);

            return aggregate;
        }

        public async Task SaveAsync(T aggregate, CancellationToken cancellationToken = default(CancellationToken))
        {
            _snapshotStore.TakeSnapshot(aggregate);

            var events = aggregate.FlushUncommittedChanges();
            foreach (EventBase @event in events)
            {
                await _eventStore.SaveAsync(@event, cancellationToken);
                await _serviceBus.PublishAsync((dynamic)@event, cancellationToken);
            }
        }
    }
}
