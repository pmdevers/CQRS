using System;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.Factories;
using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS.Builder
{
    public class CQRSOptions
    {
        public AggregateInstanceFactory InstanceFactory { get; set; } = Activator.CreateInstance;
        public IEventSerializer EventSerializer { get; set; } = new BinaryEventSerializer();
    }
}
