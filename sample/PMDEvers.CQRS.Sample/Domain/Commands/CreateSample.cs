using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PMDEvers.CQRS.Commands;

namespace PMDEvers.CQRS.Sample.Domain.Commands
{
    public class CreateSample : CommandBase<Guid>
    {
        public CreateSample() : base(Guid.NewGuid())
        {
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
