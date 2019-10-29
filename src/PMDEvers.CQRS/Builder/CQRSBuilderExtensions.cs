using System;

using Microsoft.Extensions.DependencyInjection;

using PMDEvers.Servicebus;

namespace PMDEvers.CQRS
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddCommandHandler<TCommand, TReturn, TCommandHandler>(this CQRSBuilder builder)
            where TCommand : ICommand<TReturn>
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (typeof(ICommandHandler<TCommand, TReturn>).IsAssignableFrom(typeof(TCommandHandler)))
            {
                builder.Services.AddScoped(typeof(ICommandHandler<TCommand, TReturn>), typeof(TCommandHandler));
            }

            if (typeof(IAsyncCommandHandler<TCommand, TReturn>).IsAssignableFrom(typeof(TCommandHandler)))
            {
                builder.Services.AddScoped(typeof(IAsyncCommandHandler<TCommand, TReturn>), typeof(TCommandHandler));
            }

            if (typeof(ICancellableAsyncCommandHandler<TCommand, TReturn>).IsAssignableFrom(typeof(TCommandHandler)))
            {
                builder.Services.AddScoped(typeof(ICancellableAsyncCommandHandler<TCommand, TReturn>), typeof(TCommandHandler));
            }

            return builder;
        }
    }
}