// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;

using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.Commands
{
    public abstract class CommandBase<TResult> : CommandBase, ICommand<TResult>
    {
        protected CommandBase(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }

    public abstract class CommandBase : ICommand
    {
        protected CommandBase(Guid aggregateId)
        {
            AggregateId = aggregateId;
            MessageType = GetType().Name;
            TimeStamp = DateTimeOffset.UtcNow;
        }

        public Guid AggregateId { get; protected set; }
        public string MessageType { get; protected set; }
        public DateTimeOffset TimeStamp { get; }

        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
