// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;

namespace PMDEvers.CQRS.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(ValidationResult validationResult, Type commandType)
            : base($"Command: '{commandType.Name}' is Invalid")
        {
        }
    }
}
