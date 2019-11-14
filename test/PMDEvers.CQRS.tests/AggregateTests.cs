
using System;
using System.Threading.Tasks;

using PMDEvers.CQRS.tests.TestDomain;
using PMDEvers.CQRS.tests.TestDomain.Commands;
using PMDEvers.CQRS.tests.TestDomain.Handlers;
using PMDEvers.CQRS.TestTools;
using PMDEvers.Servicebus;

using Xunit;

namespace PMDEvers.CQRS.tests
{
    public class AggregateCreate : SpecificationWithResult<Aggregate, CreateCommand, Guid>
    {
        protected override CreateCommand When()
        {
            return new CreateCommand();
        }

        protected override ICommandHandler<CreateCommand, Guid> CommandHandler()
        {
            return new CreateCommandHandler(MockRepository.Object);
        }

        [Fact]
        public void Then()
        {
            Assert.NotEqual(Guid.Empty, Result);
        }
    }


    public class AsyncAggregateCreate : AsyncSpecificationWithResult<Aggregate, CreateCommand, Guid>
    {
        protected override CreateCommand When()
        {
            return new CreateCommand();
        }

        protected override IAsyncCommandHandler<CreateCommand, Guid> CommandHandler()
        {
            return new CreateCommandHandler(MockRepository.Object);
        }

        [Fact]
        public void Then()
        {
            var res = Result;
            Assert.NotEqual(Guid.Empty, res);
        }
    }

    public class CancellableAsyncAggregateCreate : CancellableAsyncSpecificationWithResult<Aggregate, CreateCommand, Guid>
    {
        protected override CreateCommand When()
        {
            return new CreateCommand();
        }

        protected override ICancellableAsyncCommandHandler<CreateCommand, Guid> CommandHandler()
        {
            return new CancellableCreateCommandHandler(MockRepository.Object);
        }

        [Fact]
        public void Then()
        {
            var res = Result;
            Assert.NotEqual(Guid.Empty, res);
        }
    }
}