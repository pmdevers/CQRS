using System;
using System.Collections.Generic;
using System.Text;

namespace PMDEvers.CQRS.EntityFramework
{
    public class Event
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string Data { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
