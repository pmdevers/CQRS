using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS
{
    public class BinaryEventSerializer : IEventSerializer
    {
        public string Serializer(EventBase @event)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                IFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, @event);
                bytes = memoryStream.ToArray();
            }

            return Encoding.ASCII.GetString(bytes);
        }

        public EventBase Deserializer(string data)
        {
            var array = Encoding.ASCII.GetBytes(data);
            EventBase returnValue;
            using (var memoryStream = new MemoryStream(array))
            {
                IFormatter binaryFormatter = new BinaryFormatter();
                returnValue = (EventBase)binaryFormatter.Deserialize(memoryStream);
            }

            return returnValue;
        }
    }
}
