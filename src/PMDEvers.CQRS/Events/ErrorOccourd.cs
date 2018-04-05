// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace PMDEvers.CQRS.Events
{
    [Serializable]
    public class ErrorOccourd : EventBase
    {
        public ErrorOccourd(string key, string value)
            : base(Guid.NewGuid())
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }

        public override string ToString()
        {
            return $"Error '{Key}': {Value}";
        }
    }
}
