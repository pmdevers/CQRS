using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PMDEvers.CQRS.EntityFramework.Serializers;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS.EntityFramework
{
    public class EventStore : IEventStore
    {
        private readonly EventContext _context;
        private readonly IEventSerializer _serializer;

        public EventStore(EventContext context, IEventSerializer serializer)
        {
            _context = context;
            _serializer = serializer;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync(EventBase @event, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existing = await _context.Events.FirstOrDefaultAsync(x => 
                x.AggregateId == @event.AggregateId && 
                x.Version == @event.Version, cancellationToken);

            if(existing != null)
                throw new EventCollisionException(@event.AggregateId, @event.Version);

            var storeEvent = new Event
            {
                Id = Guid.NewGuid(),
                AggregateId = @event.AggregateId,
                Data = _serializer.Serializer(@event),
                Version = @event.Version,
                TimeStamp = @event.Timestamp
            };

            _context.Events.Add(storeEvent);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<EventBase>> FindByIdAsync(Guid aggregateId, int fromVersion,
            CancellationToken cancellationToken = default)
        {
            var result = (await _context.Events.Where(x => x.AggregateId == aggregateId && x.Version >= fromVersion)
                                        .ToListAsync(cancellationToken))
                         .OrderBy(x => x.Version)
                         .Select(x => _serializer.Deserializer(x.Data));

            return result;
        }

        public async Task<IEnumerable<EventBase>> FindByHistoryAsync(Guid aggregateId, int tillVersion,
            CancellationToken cancellationToken = default)
        {
            var result = (await _context.Events.Where(x => x.AggregateId == aggregateId && x.Version <= tillVersion)
                                        .ToListAsync(cancellationToken))
                         .OrderBy(x => x.Version)
                         .Select(x => _serializer.Deserializer(x.Data));

            return result;
        }

        public async Task<IEnumerable<EventBase>> FindAllAsync(DateTime tillDate,
            CancellationToken cancellationToken = default)
        {
            var result = (await _context.Events.Where(x => x.TimeStamp <= tillDate)
                                        .ToListAsync(cancellationToken))
                         .OrderBy(x => x.Version)
                         .Select(x => _serializer.Deserializer(x.Data));

            return result;
        }
    }
}
