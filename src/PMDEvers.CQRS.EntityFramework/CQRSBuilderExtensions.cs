using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

namespace PMDEvers.CQRS.EntityFramework
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddEntityFramework(this CQRSBuilder builder)
        {
            builder.Services.AddDbContext<EventContext>();
            return builder.AddEventStore<EventStore>();
        }
    }
}
