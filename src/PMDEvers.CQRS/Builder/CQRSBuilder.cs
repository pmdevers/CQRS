using System;

using Microsoft.Extensions.DependencyInjection;

using PMDEvers.CQRS.Interfaces;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS
{
    public class CQRSBuilder
    {
        private readonly IServiceCollection _services;
        private readonly ServiceBusBuilder _serviceBusBuilder;

        public IServiceCollection Services => _services;

        public CQRSBuilder(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _serviceBusBuilder = new ServiceBusBuilder(services);
        }

        public CQRSBuilder AddEventStore<TEventStore>()
            where TEventStore : class, IEventStore
        {
            _services.AddScoped<IEventStore, TEventStore>();
            return this;
        }

        public CQRSBuilder AddAggregate<TAggregate>()
            where TAggregate : AggregateRoot
        {
            _services.AddScoped<IRepository<TAggregate>, Repository<TAggregate>>();
            return this;
        }

        public CQRSBuilder AddEventHandler<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : class
        {
            _serviceBusBuilder.AddEventHandler<TEvent, TEventHandler>();
            return this;
        }
        public CQRSBuilder AddCommandHandler<TCommand, TCommandHandler>()
            where TCommand : ICommand
            where TCommandHandler : class
        {
            _serviceBusBuilder.AddCommandHandler<TCommand, TCommandHandler>();
            return this;
        }
    }
}
