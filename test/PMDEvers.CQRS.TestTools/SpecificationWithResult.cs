using System;
using System.Collections.Generic;
using System.Threading;

using Moq;

using PMDEvers.CQRS.Commands;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Factories;
using PMDEvers.CQRS.Interfaces;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.TestTools
{
    public abstract class SpecificationWithResult<TAggregate, TCommand, TResult>
        where TCommand : CommandBase<TResult>
        where TAggregate : AggregateRoot
    {
        protected TAggregate Aggregate;
        protected TResult Result;
        protected Mock<IEventHandler<ErrorOccourd>> MockEventHandler;
        protected readonly Mock<IRepository<TAggregate>> MockRepository = new Mock<IRepository<TAggregate>>();
        protected readonly Mock<IServiceBus> MockServiceBus = new Mock<IServiceBus>();
        private readonly List<EventBase> _publishedEvents = new List<EventBase>();
        protected Exception CaugthException { get; }

        protected SpecificationWithResult()
        {
            Aggregate = (TAggregate)InstanceFactory().Invoke(typeof(TAggregate));
            Aggregate.LoadFromHistory(Given());

            MockRepository.Setup(x => x.GetStateAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(Aggregate);

            MockRepository.Setup(x => x.SaveAsync(It.IsAny<TAggregate>(), It.IsAny<CancellationToken>()))
                          .Callback<TAggregate, CancellationToken>((a, t) => Aggregate = a);

            MockServiceBus.Setup(x => x.PublishAsync(It.IsAny<EventBase>(), It.IsAny<CancellationToken>()))
                          .Callback<EventBase, CancellationToken>((e, t) => _publishedEvents.Add(e));


            try
            {
                Result = CommandHandler().Handle(When());
            }
            catch (Exception exception)
            {
                CaugthException = exception;
            }
        }

        protected virtual IEnumerable<EventBase> Given()
        {
            return new List<EventBase>();
        }
        protected abstract AggregateInstanceFactory InstanceFactory();
        protected abstract TCommand When();
        protected abstract ICommandHandler<TCommand,TResult> CommandHandler();
    }
}