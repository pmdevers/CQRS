using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PMDEvers.CQRS.EntityFramework
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddEntityFramework(this CQRSBuilder builder, Action<CQRSEntityFrameworkOptions> options = null)
        {
            var option = new CQRSEntityFrameworkOptions();
            options?.Invoke(option);

            builder.Services.AddSingleton(x => option.EventSerializer);
            builder.Services.AddDbContext<EventContext>(option.ContextOptions);

            return builder.AddEventStore<EventStore>();
        }
    }
}
