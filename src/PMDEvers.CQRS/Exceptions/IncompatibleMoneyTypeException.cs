// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace PMDEvers.CQRS.Exceptions
{
    public class IncompatibleMoneyTypeException : Exception
    {
        public IncompatibleMoneyTypeException(Type money1, Type money2, string message)
            : base(message)
        {
        }
    }
}
