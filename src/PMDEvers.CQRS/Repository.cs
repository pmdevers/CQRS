using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Factories;
using PMDEvers.CQRS.Interfaces;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS
{
    public class Repository<T> : IRepository<T>
        where T : AggregateRoot
    {
        private readonly IServiceBus _serviceBus;
        private readonly AggregateInstanceFactory _aggregateInstanceFactory;
        private readonly IEventStore _eventStore;
        private readonly IList<T> _requestCache = new List<T>();

        public Repository(IEventStore eventStore, IServiceBus serviceBus, AggregateInstanceFactory aggregateInstanceFactory)
        {
            _eventStore = eventStore;
            _serviceBus = serviceBus;
            _aggregateInstanceFactory = aggregateInstanceFactory;
        }

        public async Task<T> GetCurrentStateAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var aggregate = _requestCache.FirstOrDefault(x => x.Id == id);
            if (aggregate != null)
            {
                return aggregate;
            }

            var events = (await _eventStore.FindByIdAsync(id, -1, cancellationToken)).ToList();
            if (!events.Any())
            {
                return null;
            }

            aggregate = (T)_aggregateInstanceFactory(typeof(T));
            aggregate.LoadFromHistory(events);

            _requestCache.Add(aggregate);

            return aggregate;
        }

        public async Task<T> GetStateAsync(Guid id, int expectedVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cached = _requestCache.FirstOrDefault(x => x.Id == id);
            if (cached != null)
            {
                return cached;
            }

            var events = await _eventStore.FindByHistoryAsync(id, expectedVersion, cancellationToken);
            if (!events.Any())
            {
                return null;
            }

            var aggregate = (T)_aggregateInstanceFactory(typeof(T));
            aggregate.LoadFromHistory(events);

            _requestCache.Add(aggregate);

            return aggregate;
        }

        public async Task SaveAsync(T aggregate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cached = _requestCache.FirstOrDefault(x => x.Id == aggregate.Id);
            if (cached != null)
            {
                _requestCache.Remove(cached);

            }

            _requestCache.Add(aggregate);

            var events = aggregate.FlushUncommittedChanges();
            foreach (EventBase @event in events)
            {
                await _eventStore.SaveAsync(@event, cancellationToken);
                await _serviceBus.PublishAsync((dynamic)@event, cancellationToken);
            }
        }
    }
}
