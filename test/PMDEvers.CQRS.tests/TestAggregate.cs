using System;
using System.Collections.Generic;
using System.Text;

using PMDEvers.CQRS.Commands;
using PMDEvers.CQRS.Events;

namespace PMDEvers.CQRS.tests
{
    public class CreateTestCommand : CommandBase
    {
        public CreateTestCommand()
            : base(Guid.NewGuid())
        {
        }

        public override bool IsValid()
        {
            return true;
        }
    }

    [Serializable]
    public class TestCreated : EventBase
    {
        public TestCreated(Guid id, string title)
            : base(id)
        {
            Title = title;
        }

        public string Title { get; private set; }

        public override string ToString()
        {
            return $"Test created {AggregateId}";
        }
    }

    public class TestAggregate : AggregateRoot
    {
        public static TestAggregate Create()
        {
            var t = new TestAggregate();
            t.ApplyChange(new TestCreated(t.Id, "Test"));
            return t;
        }

        public string Title { get; private set; }

        protected override void RegisterAppliers()
        {
            RegisterApplier<TestCreated>(CreateTest);
        }

        private void CreateTest(TestCreated obj)
        {
            Title = obj.Title;
        }
    }
}
