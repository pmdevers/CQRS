using System;
using System.Collections.Generic;
using System.Text;

namespace PMDEvers.CQRS.Interfaces
{
    public interface ISnapshotStore<T>
        where T : AggregateRoot
    {
        void TakeSnapshot(T instance);

        T GetSnapshot(Guid id);
    }
}
