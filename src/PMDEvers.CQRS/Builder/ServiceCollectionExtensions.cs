using System;

using Microsoft.Extensions.DependencyInjection;

using PMDEvers.CQRS.EventHandlers;
using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static CQRSBuilder AddCQRS(this IServiceCollection services, Action<CQRSOptions> options = null)
        {
            var opt = new CQRSOptions();
            options?.Invoke(opt);

            var builder = new CQRSBuilder(services);

            builder.AddEventHandler<ErrorOccourd, ErrorOccuredEventHandler>();

            return builder;
        }
    }
}
