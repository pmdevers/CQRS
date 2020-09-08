using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMDEvers.CQRS.Interfaces;
using PMDEvers.CQRS.Sample.Application.Queries;
using PMDEvers.CQRS.Sample.Application.Responses;
using PMDEvers.CQRS.Sample.Domain;
using PMDEvers.Servicebus.Interfaces;

namespace PMDEvers.CQRS.Sample.Application.Handlers
{
    public class GetSampleHandler : IAsyncQueryHandler<GetSample, SampleResponse>
    {
        private readonly IRepository<SampleAggregate> _repository;

        public GetSampleHandler(IRepository<SampleAggregate> repository)
        {
            _repository = repository;
        }

        public async Task<SampleResponse> HandleAsync(GetSample query)
        {
            if (query.SampleId == Guid.Empty)
            {
                return new SampleResponse()
                {
                    Id = query.SampleId,
                    Username = ""
                };
            }

            var sample = await _repository.GetCurrentStateAsync(query.SampleId);

            return new SampleResponse()
            {
                Id = sample.Id,
                Username = sample.CreatedBy,
            };
        }
    }
}
