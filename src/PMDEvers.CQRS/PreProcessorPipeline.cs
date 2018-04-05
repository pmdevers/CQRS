// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;

using PMDEvers.CQRS.Commands;
using PMDEvers.CQRS.Exceptions;
using PMDEvers.Servicebus.Pipeline;

namespace PMDEvers.CQRS
{
    public class CommandValidationProcessor<TCommand> : ICommandPreProcessor<TCommand>
        where TCommand : CommandBase
    {
        public Task ProcessAsync(TCommand command)
        {
            if (!command.IsValid())
                throw new InvalidCommandException(command.ValidationResult, command.GetType());

            return Task.CompletedTask;
        }
    }
}
