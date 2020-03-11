using System;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.Factories;
using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS.Builder
{
    public class CQRSOptions
    {
        public CQRSOptions()
        {
            UsernameAccessor = () => UnknownUsername;
        }
        public string UnknownUsername { get; set; } = "Unknown";
        public AggregateInstanceFactory InstanceFactory { get; set; } = Activator.CreateInstance;
        public UsernameAccessor UsernameAccessor { get; set; }
    }
}
