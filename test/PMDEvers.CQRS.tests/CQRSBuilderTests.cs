using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using PMDEvers.CQRS.Builder;
using PMDEvers.CQRS.Factories;
using PMDEvers.CQRS.InMemory;
using PMDEvers.CQRS.Interfaces;
using PMDEvers.Servicebus;

using Xunit;

namespace PMDEvers.CQRS.tests
{
    public class CQRSBuilderTests
    {
        [Fact]
        public void AddCQRS_Adds_Defaults()
        {
            var container = new ServiceCollection();
            container.AddCQRS();

            var provider = container.BuildServiceProvider();

            Assert.NotNull(provider.GetService<AggregateInstanceFactory>());
        }

        [Fact]
        public void AddCQRS_Override_Factory()
        {
            var container = new ServiceCollection();
            container.AddCQRS(options =>
            {
                options.InstanceFactory = InstanceFactory;
            });

            var provider = container.BuildServiceProvider();

            Assert.Equal(InstanceFactory,  provider.GetService<AggregateInstanceFactory>());
        }

        [Fact]
        public void AddAggregate_Registers_Repository()
        {
            var container = new ServiceCollection();
            container.AddServiceBus();
            container.AddCQRS()
                     .AddAggregate<TestAggregate>()
                     .AddInMemoryEventStore();

            var provider = container.BuildServiceProvider();

            Assert.NotNull(provider.GetService<IRepository<TestAggregate>>());

        }
        [Fact]
        public async Task Repository_CreatesNewAggregate()
        {
            var container = new ServiceCollection();
            container.AddServiceBus();
            container.AddCQRS()
                     .AddAggregate<TestAggregate>()
                     .AddInMemoryEventStore();

            var provider = container.BuildServiceProvider();
            var store = provider.GetService<IEventStore>();
            var id = Guid.NewGuid();
            await store.SaveAsync(new TestCreated(id, "test") { Version = 1 });
            var repository = provider.GetService<IRepository<TestAggregate>>();

            var aggregate = await repository.GetCurrentStateAsync(id);

            Assert.NotNull(aggregate);
        }

        [Fact]
        public async Task Repository_CreateReturnsNewAggregate()
        {
            var container = new ServiceCollection();
            container.AddServiceBus();
            container.AddCQRS()
                .AddAggregate<ComplexAggregate>()
                .AddInMemoryEventStore();

            var provider = container.BuildServiceProvider();

            var repository = provider.GetService<IRepository<ComplexAggregate>>();

            var result = await repository.Create();

            await repository.SaveAsync(result);

            var store = provider.GetService<IEventStore>();

            var test = await repository.GetCurrentStateAsync(result.Id);

            Assert.NotNull(result);
        }

        private object InstanceFactory(Type serviceType)
        {
            return new object();
        }
    }
}
