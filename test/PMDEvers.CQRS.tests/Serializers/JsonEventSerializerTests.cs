using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Xunit;

namespace PMDEvers.CQRS.tests.Serializers
{
    public class JsonEventSerializerTests
    {
        [Fact]
        public void JsonSerializer_Serialize()
        {
            var serializer = new JsonEventSerializer();
            var id = Guid.NewGuid();
            var testEvent = new TestCreated(id, "Test") { Version = 1 };

            var result = serializer.Serializer(testEvent);

            var obj = JObject.Parse(result);

            Assert.Equal(testEvent.AggregateId, Guid.Parse(obj["AggregateId"].Value<string>()));
            Assert.Equal(testEvent.Title, obj["Title"].Value<String>());
            Assert.Equal(testEvent.Version, obj["Version"].Value<int>());
            Assert.Equal(testEvent.MessageType, obj["MessageType"].Value<string>());
            Assert.Equal(testEvent.Timestamp, obj["Timestamp"].Value<DateTime>());
            Assert.Equal(testEvent.Username, obj["Username"].Value<string>());
        }

        [Fact]
        public void JsonSerializer_Deserialize()
        {
            var serializer = new JsonEventSerializer();
            var testEvent = new TestCreated(Guid.NewGuid(), "Test") { Version = 1 };
            var eventString = serializer.Serializer(testEvent);

            var result = serializer.Deserializer(eventString);

            var eventResult = (TestCreated)result;

            Assert.Equal(testEvent.AggregateId, eventResult.AggregateId);
            Assert.Equal(testEvent.Title, eventResult.Title);
            Assert.Equal(testEvent.Version, eventResult.Version);
            Assert.Equal(testEvent.MessageType, eventResult.MessageType);
            Assert.Equal(testEvent.Timestamp, eventResult.Timestamp);
            Assert.Equal(testEvent.Username, testEvent.Username);
        }
    }
}
