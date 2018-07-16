//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Text;

//using AutoFixture;

//using Moq;

//using PMDEvers.CQRS.Events;
//using PMDEvers.CQRS.Factories;
//using PMDEvers.CQRS.TestTools;
//using PMDEvers.Servicebus;

//using Xunit;

//namespace PMDEvers.CQRS.tests
//{

//    public class AggregateTests
//    {
//        [Fact]
//        public void AuditTests()
//        {
//            var fixture = new Fixture();
//            var test = fixture.Create<Mock<Aggregate>>();

//            var createEvent = fixture.Create<Mock<>>();

//            var events = new[] { TestCreated. };

//            test.LoadFromHistory(events);

//            Assert.Equal(events.First().Timestamp, test.CreationDate);
//            Assert.Equal(events.First().Username, test.CreatedBy);
//            Assert.Equal(events.Last().Timestamp, test.LastModifiedDate);
//            Assert.Equal(events.Last().Username, test.LastModifiedBy);
//        }
//    }
//}
