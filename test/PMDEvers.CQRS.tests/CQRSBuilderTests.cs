using System;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using PMDEvers.CQRS.Builder;
using PMDEvers.CQRS.Factories;
using PMDEvers.CQRS.Interfaces;

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
            Assert.NotNull(provider.GetService<IEventSerializer>());
            Assert.IsType<BinaryEventSerializer>(provider.GetService<IEventSerializer>());
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
        public void AddCQRS_Override_Serializer()
        {
            var serializer = new JsonEventSerializer();
            var container = new ServiceCollection();
            container.AddCQRS(options => { options.EventSerializer = serializer; });

            var provider = container.BuildServiceProvider();

            Assert.Equal(serializer,  provider.GetService<IEventSerializer>());
        }

        private object InstanceFactory(Type serviceType)
        {
            return new object();
        }
    }
}
