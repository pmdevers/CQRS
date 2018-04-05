// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace PMDEvers.CQRS.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(Type aggregateType, Guid id)
            : base($"The Aggregate '{aggregateType.Name}' with id: '{id}' was not found.")
        {
        }
    }
}
