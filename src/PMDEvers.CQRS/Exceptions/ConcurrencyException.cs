// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace PMDEvers.CQRS.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(Guid aggregateId)
            : base($"The aggregateid: '{aggregateId}' is outdated!")
        {
        }
    }
}
