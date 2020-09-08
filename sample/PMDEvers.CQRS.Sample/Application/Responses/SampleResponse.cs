using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMDEvers.CQRS.Sample.Application.Responses
{
    public class SampleResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Value { get; set; }
    }
}
