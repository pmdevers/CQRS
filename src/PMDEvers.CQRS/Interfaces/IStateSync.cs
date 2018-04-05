// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace PMDEvers.CQRS.Interfaces
{
    public interface IStateSync
    {
        void UpdateState<T>(T aggregate) where T : AggregateRoot;

        void DeleteState<T>(Guid aggregateId) where T : AggregateRoot;

        void CreateState<T>(T aggregate) where T : AggregateRoot;
    }
}
