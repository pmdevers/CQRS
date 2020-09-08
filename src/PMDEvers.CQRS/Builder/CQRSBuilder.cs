using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PMDEvers.CQRS.Interfaces;
using PMDEvers.Servicebus;
using PMDEvers.Servicebus.Interfaces;

namespace PMDEvers.CQRS
{
    public class CQRSBuilder
    {
        private readonly ServiceBusBuilder _serviceBusBuilder;

        public IServiceCollection Services { get; }

        public CQRSBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _serviceBusBuilder = new ServiceBusBuilder(services);
        }

        public CQRSBuilder AddEventStore<TEventStore>()
            where TEventStore : class, IEventStore
        {
            Services.AddScoped<IEventStore, TEventStore>();
            return this;
        }

        public CQRSBuilder AddAggregate<TAggregate>()
            where TAggregate : AggregateRoot
        {
            Services.AddTransient<TAggregate>();
            Services.AddScoped<IRepository<TAggregate>, Repository<TAggregate>>();
            return this;
        }

        public CQRSBuilder AddEventHandler<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : class
        {
            var type = typeof(TEvent);
            if (type.GetCustomAttribute(typeof(SerializableAttribute)) == null)
            {
                throw new Exception($"Event {type.Name} must be marked as serializable.");
            }

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

        public CQRSBuilder AddQueryHandler<TQuery, TResult, TQueryHandler>()
            where TQuery : class, IQuery<TResult>
            where TQueryHandler : class
        {
            _serviceBusBuilder.AddQueryHandler<TQuery, TResult, TQueryHandler>();
            return this;
        }
    }
}
