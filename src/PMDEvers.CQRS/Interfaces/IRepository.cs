// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace PMDEvers.CQRS.Interfaces
{
    public interface IRepository<TState> where TState : AggregateRoot
    {
        Task<TState> GetCurrentStateAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<TState> GetStateAsync(Guid id, int expectedVersion, CancellationToken cancellationToken = default(CancellationToken));

        Task SaveAsync(TState aggregate, CancellationToken cancellationToken = default(CancellationToken));
    }
}
