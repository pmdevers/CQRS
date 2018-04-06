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

            return ByteArrayToString(bytes);
        }

        public EventBase Deserializer(string data)
        {
            var array = StringToByteArray(data);
            EventBase returnValue;
            using (var memoryStream = new MemoryStream(array))
            {
                IFormatter binaryFormatter = new BinaryFormatter();
                returnValue = (EventBase)binaryFormatter.Deserialize(memoryStream);
            }

            return returnValue;
        }


        private string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            var bytes = new byte[NumberChars / 2];
            for (var i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
