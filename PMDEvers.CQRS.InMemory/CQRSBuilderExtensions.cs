using System;
using System.Collections.Generic;
using System.Text;

namespace PMDEvers.CQRS.InMemory
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddInMemoryEventStore(this CQRSBuilder builder)
        {
            return builder.AddEventStore<EventStore>();
        }
    }
}
