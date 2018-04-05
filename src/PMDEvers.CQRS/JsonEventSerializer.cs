using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS
{
    public class JsonEventSerializer : IEventSerializer
    {
        public string Serializer(EventBase @event)
        {
            return JsonConvert.SerializeObject(@event);
        }

        public EventBase Deserializer(string data)
        {
            return JsonConvert.DeserializeObject<EventBase>(data);
        }
    }
}
