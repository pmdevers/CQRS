using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PMDEvers.CQRS.EntityFramework
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddEntityFramework(this CQRSBuilder builder, Action<DbContextOptionsBuilder> options = null)
        {
            builder.Services.AddDbContext<EventContext>(options);
            return builder.AddEventStore<EventStore>();
        }
    }
}
