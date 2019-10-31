using System;
using System.Collections.Generic;
using System.Text;

namespace PMDEvers.CQRS.EntityFramework
{
    public class EventCollisionException : Exception
    {
        public EventCollisionException(Guid aggregateId, int version)
            :base($"The aggregate with id '{aggregateId}' already has a event with version: '{version}'")
        {
            
        }
    }
}
