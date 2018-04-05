// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace PMDEvers.CQRS.Exceptions
{
    public class NoEventApplyMethodRegisteredException : Exception
    {
        public NoEventApplyMethodRegisteredException(Type eventType, AggregateRoot aggregateRoot)
            : base($"The Aggregate: {aggregateRoot.GetType().Name} has no ApplyMethod for type: {eventType.Name}.")
        {
        }
    }
}
