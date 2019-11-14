using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace PMDEvers.CQRS.tests.Snapshots
{
    
    public class SnapshotTest
    {
        [Fact]
        public void Snapshotter_test()
        {
            var snap = new SnapShotter<TestAggregate>();
            var aggregate = TestAggregate.Create();

            var events = aggregate.FlushUncommittedChanges();

            snap.TakeSnapshot(aggregate);

            var ag = snap.GetSnapshot(aggregate.Id);

            Assert.NotNull(ag);
            Assert.Equal(aggregate.Title, ag.Title);
        }
    }
}
