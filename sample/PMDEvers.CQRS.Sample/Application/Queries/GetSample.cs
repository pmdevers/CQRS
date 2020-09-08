using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDEvers.CQRS.Sample.Application.Responses;
using PMDEvers.Servicebus.Interfaces;

namespace PMDEvers.CQRS.Sample.Application.Queries
{
    public class GetSample : IQuery<SampleResponse>
    {
        public Guid SampleId { get; set; }
    }
}
