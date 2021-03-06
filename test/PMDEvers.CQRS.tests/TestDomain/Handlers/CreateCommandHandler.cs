﻿using System;
using System.Threading;
using System.Threading.Tasks;

using PMDEvers.CQRS.Interfaces;
using PMDEvers.CQRS.tests.TestDomain.Commands;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.tests.TestDomain.Handlers
{
    public class CreateCommandHandler : 
        ICommandHandler<CreateCommand, Guid>,
        IAsyncCommandHandler<CreateCommand, Guid>
        
    {
        private readonly IRepository<Aggregate> _repository;

        public CreateCommandHandler(IRepository<Aggregate> repository)
        {
            _repository = repository;
        }

        public Guid Handle(CreateCommand command)
        {
            var a = _repository.Create();
            _repository.SaveAsync(a, CancellationToken.None);
            return a.Id;
        }

        public async Task<Guid> HandleAsync(CreateCommand command)
        {
            var a = _repository.Create();
            await _repository.SaveAsync(a, CancellationToken.None);
            return a.Id;
        }
    }

    public class CancellableCreateCommandHandler : ICancellableAsyncCommandHandler<CreateCommand, Guid>
    {
        private readonly IRepository<Aggregate> _repository;

        public CancellableCreateCommandHandler(IRepository<Aggregate> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> HandleAsync(CreateCommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            var a = _repository.Create();
            await _repository.SaveAsync(a, cancellationToken);
            return a.Id;
        }
    }
}
