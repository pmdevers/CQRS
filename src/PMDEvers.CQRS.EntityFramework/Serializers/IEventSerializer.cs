using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.EntityFramework.Serializers
{
    public interface IEventSerializer
    {
        string Serializer(EventBase @event);

        EventBase Deserializer(string data);
    }
}
