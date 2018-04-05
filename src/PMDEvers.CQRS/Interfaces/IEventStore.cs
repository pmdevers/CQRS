// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.Interfaces
{
    public interface IEventStore : IDisposable
    {
        Task SaveAsync(EventBase @event, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<EventBase>> FindByIdAsync(Guid aggregateId, int fromVersion,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<EventBase>> FindByHistoryAsync(Guid aggregateId, int tillVersion,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<EventBase>> FindAllAsync(DateTime tillDate, CancellationToken cancellationToken = default(CancellationToken));
    }
}
