using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.Interfaces
{
    public interface IEventSerializer
    {
        string Serializer(EventBase @event);

        EventBase Deserializer(string data);
    }
}
