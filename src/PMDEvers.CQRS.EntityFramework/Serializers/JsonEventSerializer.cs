using System;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.EntityFramework.Serializers
{
    public class JsonEventSerializer : IEventSerializer
    {
        public string Serializer(EventBase @event)
        {
            var setting = new JsonSerializerSettings
            {
TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            return JsonConvert.SerializeObject(@event, setting);
        }

        public EventBase Deserializer(string data)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new PrivatePropertyContractResolver(),
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            return JsonConvert.DeserializeObject<EventBase>(data, setting);
        }

        // ReSharper disable once ClassNeverInstantiated.Global
        internal class JsonEvent : EventBase
        {
            public JsonEvent(Guid aggregateId) : base(aggregateId)
            {
            }

            public override string ToString()
            {
                return $"JsonEvent for serialzation Only";
            }
        }


    }

    public class PrivatePropertyContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(
            MemberInfo member,
            MemberSerialization memberSerialization)
        {
            //TODO: Maybe cache
            var prop = base.CreateProperty(member, memberSerialization);

            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    var hasPrivateSetter = property.GetSetMethod(true) != null;
                    prop.Writable = hasPrivateSetter;
                }
            }

            return prop;
        }
    }
}
