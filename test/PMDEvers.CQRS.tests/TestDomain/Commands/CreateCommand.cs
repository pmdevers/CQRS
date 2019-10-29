using System;

using PMDEvers.CQRS.Commands;

namespace PMDEvers.CQRS.tests.TestDomain.Commands
{
    public class CreateCommand : CommandBase<Guid>
    {
        public CreateCommand() : base(Guid.NewGuid())
        {
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
