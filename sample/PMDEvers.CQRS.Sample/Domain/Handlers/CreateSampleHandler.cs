using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Interfaces;
using PMDEvers.CQRS.Sample.Domain.Commands;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.Sample.Domain.Handlers
{
    public class CreateSampleHandler : ICancellableAsyncCommandHandler<CreateSample>
    {
        private readonly IRepository<SampleAggregate> _repository;

        public CreateSampleHandler(IRepository<SampleAggregate> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CreateSample command, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!command.IsValid())
            {
                return;
            }

            var aggregate = SampleAggregate.Create();

            await _repository.SaveAsync(aggregate, cancellationToken);
        }
    }
}
