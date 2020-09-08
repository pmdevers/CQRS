// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Exceptions;

namespace PMDEvers.CQRS
{
    public abstract class AggregateRoot
    {
        private readonly List<EventBase> _changes;
        private readonly Dictionary<Type, Action<EventBase>> _eventAppliers;

        protected AggregateRoot()
        {
            Id = Guid.NewGuid();
            _changes = new List<EventBase>();
            _eventAppliers = new Dictionary<Type, Action<EventBase>>();
            RegisterApplier<AggregateCreated>(Apply);
            RegisterAppliers();
        }

        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        public DateTime CreationDate { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public string LastModifiedBy { get; private set; }

        protected abstract void RegisterAppliers();

        public TEvent GetLastEventOf<TEvent>() where TEvent : EventBase
        {
            lock (_changes)
            {
                return _changes.OfType<TEvent>().OrderByDescending(x => x.Version).LastOrDefault();
            }
        }

        protected void RegisterApplier<TEvent>(Action<TEvent> applier) where TEvent : EventBase
        {
            _eventAppliers.Add(typeof(TEvent), x => applier((TEvent)x));
        }

        public EventBase[] GetUncommittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }

        public EventBase[] FlushUncommittedChanges()
        {
            lock (_changes)
            {
                var changes = _changes.ToArray();
                var i = 0;
                foreach (EventBase @event in changes)
                {
                    if (@event.AggregateId == Guid.Empty && Id == Guid.Empty)
                        throw new AggregateOrEventMissingIdException(GetType(), @event.GetType());
                    i++;
                    @event.Version = Version + i;
                }
                Version = Version + changes.Length;
                _changes.Clear();
                return changes;
            }
        }

        public void LoadFromHistory(IEnumerable<EventBase> history)
        {
            foreach (EventBase @event in history)
            {
                if (@event.Version != Version + 1)
                    throw new EventsOutOfOrderException(Id);
                ApplyChange(@event, false);
            }
        }

        public void ApplyChange(EventBase @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(EventBase @event, bool isNew)
        {
            lock (_changes)
            {
                if (Apply(@event))
                    if (isNew)
                    {
                        _changes.Add(@event);
                    }
                    else
                    {
                        Id = @event.AggregateId;
                        ApplyAudit(@event);
                        Version++;
                    }
            }
        }

        private void ApplyAudit(EventBase @event)
        {
            if (@event.Version == 1)
            {
                CreatedBy = @event.Username;
                CreationDate = @event.Timestamp.DateTime;
            }
            else
            {
                LastModifiedBy = @event.Username;
                LastModifiedDate = @event.Timestamp.DateTime;
            }
        }

        private bool Apply(EventBase @event)
        {
            Type eventType = @event.GetType();

            if (!_eventAppliers.ContainsKey(eventType))
                return false;

            _eventAppliers[eventType](@event);
            return true;
        }

        public virtual void Apply(AggregateCreated e)
        {
            Id = e.AggregateId;
            CreatedBy = e.Username;
            CreationDate = e.Timestamp.DateTime;
            LastModifiedBy = e.Username;
            LastModifiedDate = e.Timestamp.DateTime;
        }
    }
}
