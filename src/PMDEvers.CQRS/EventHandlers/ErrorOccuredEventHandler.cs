// Copyright (c) Patrick Evers. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using PMDEvers.CQRS.Events;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.EventHandlers
{
    public class ErrorOccuredEventHandler : IEventHandler<ErrorOccourd>, IDisposable
    {
        private List<ErrorOccourd> _events = new List<ErrorOccourd>();

        public void Dispose()
        {
            _events = new List<ErrorOccourd>();
        }

        public void Handle(ErrorOccourd @event)
        {
            _events.Add(@event);
        }

        public virtual List<ErrorOccourd> GetEvents()
        {
            return _events;
        }

        public virtual bool HasEvents()
        {
            return _events.Any();
        }
    }
}
