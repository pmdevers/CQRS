using System;

using Microsoft.Extensions.DependencyInjection;

using PMDEvers.CQRS.EventHandlers;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Factories;

namespace PMDEvers.CQRS.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static CQRSBuilder AddCQRS(this IServiceCollection services, Action<CQRSOptions> options = null)
        {
            var opt = new CQRSOptions();
            options?.Invoke(opt);

            var builder = new CQRSBuilder(services);

            if (opt.InstanceFactory == null)
            {
                services.AddSingleton<AggregateInstanceFactory>(x => x.GetService);
            }
            else
            {
                services.AddSingleton(x => opt.InstanceFactory);
            }
            
            services.AddSingleton(x => opt.UsernameAccessor);

            builder.AddEventHandler<ErrorOccourd, ErrorOccuredEventHandler>();

            return builder;
        }
    }
}
