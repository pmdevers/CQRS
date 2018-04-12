using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS.InMemory
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddInMemoryEventStore(this CQRSBuilder builder)
        {
            builder.Services.AddSingleton<IEventStore, EventStore>();
            return builder;
        }
    }
}
