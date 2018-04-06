// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;

using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.Events
{
    [Serializable]
    public class EventBase : IEvent
    {
        private EventBase() { }
        protected EventBase(Guid aggregateId)
        {
            AggregateId = aggregateId;
            MessageType = GetType().Name;
            Timestamp = DateTimeOffset.UtcNow;
            Username = Thread.CurrentPrincipal?.Identity?.Name ?? "Unknown";
        }

        public string MessageType { get; private set;  }
        public DateTimeOffset Timestamp { get; private set;}
        public Guid AggregateId { get; private set; }
        public int Version { get; set; }
        public string Username { get; private set;}
    }
}
