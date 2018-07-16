using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using PMDEvers.CQRS.EntityFramework.Serializers;

using Xunit;

namespace PMDEvers.CQRS.tests.Serializers
{
    public class BinaryEventSerializerTests
    {
        [Fact]
        public void BinaryEventSerializer_SerializeEvent()
        {
            var serializer = new BinaryEventSerializer();
            var testEvent = new TestCreated(Guid.NewGuid(), "Test");

            var result = serializer.Serializer(testEvent);

            Assert.NotNull(result);
        }

        [Fact]
        public void BinaryEventSerializer_Deserialize()
        {
            var serializer = new BinaryEventSerializer();
            var testEvent = new TestCreated(Guid.NewGuid(), "Test");
            var eventString = serializer.Serializer(testEvent);

            var result = serializer.Deserializer(eventString);

            var eventResult = (TestCreated)result;

            Assert.Equal(testEvent.AggregateId, eventResult.AggregateId);
            Assert.Equal(testEvent.Title, eventResult.Title);
            Assert.Equal(testEvent.Version, eventResult.Version);
            Assert.Equal(testEvent.MessageType, eventResult.MessageType);
            Assert.Equal(testEvent.Username, eventResult.Username);
        }
    }
}
