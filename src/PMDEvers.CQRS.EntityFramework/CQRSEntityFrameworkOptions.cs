using System;

using Microsoft.EntityFrameworkCore;

using PMDEvers.CQRS.EntityFramework.Serializers;

namespace PMDEvers.CQRS.EntityFramework
{
    public class CQRSEntityFrameworkOptions
    {
        public IEventSerializer EventSerializer { get; set; } = new BinaryEventSerializer();
        public Action<DbContextOptionsBuilder> ContextOptions { get; set; }
    }
}
