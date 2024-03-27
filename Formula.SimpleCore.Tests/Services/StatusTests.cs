using Xunit;
using Formula.SimpleCore;
using System.Collections.Generic;

namespace Formula.SimpleCore.Tests
{
    public class StatusTests
    {
        public class ExampleModel
        {
            public string Name { get; set; } = "John Doe";
            public int Age { get; set; }
        }

        public class AnotherModel
        {
            public int Id { get; set; }
            public float Value { get; set; }
        }

        [Fact]
        public void TestReset()
        {
            var status = new Status<ExampleModel>();
            status.Reset();

            Assert.True(status.IsSuccessful);
            Assert.Null(status.Message);
            Assert.Equal(default(ExampleModel), status.Data);
            Assert.Null(status.Details);
        }

        [Fact]
        public void TestSucceed()
        {
            var status = new Status<ExampleModel>();
            status.Succeed();

            Assert.True(status.IsSuccessful);
        }

        [Fact]
        public void TestFail()
        {
            var status = new Status<ExampleModel>();
            status.Fail();

            Assert.False(status.IsSuccessful);
        }

        [Fact]
        public void TestSetMessage()
        {
            var status = new Status<ExampleModel>();
            status.SetMessage("Test message");

            Assert.Equal("Test message", status.Message);
        }

        [Fact]
        public void TestSetData()
        {
            var status = new Status<ExampleModel>();
            status.SetData(new ExampleModel { Age = 30 });

            Assert.Equal(30, status.Data.Age);
        }

        [Fact]
        public void TestSetDetails()
        {
            var status = new Status<ExampleModel>();
            var details = new Dictionary<string, string> { { "key", "value" } };
            status.SetDetails(details);

            Assert.Equal(details, status.Details);
        }

        [Fact]
        public void TestRecordFailure()
        {
            var status = new Status<ExampleModel>();
            status.RecordFailure("Test failure", "Test subject");

            Assert.False(status.IsSuccessful);
            Assert.Equal("Test failure", status.Message);
            Assert.True(status.Details.ContainsKey("Test subject"));
            Assert.Equal("Test failure", status.Details["Test subject"]);
        }

        [Fact]
        public void TestConvertWithDataAs()
        {
            var status = new Status<ExampleModel>();
            status.SetData(new ExampleModel { Age = 30 });
            status.RecordFailure("Test failure", "Test subject");
            var newStatus = status.ConvertWithDataAs<AnotherModel>(new AnotherModel { Id = 123, Value = 3.14f });

            Assert.Equal(status.IsSuccessful, newStatus.IsSuccessful);
            Assert.Equal("Test failure", newStatus.Message);
            Assert.Equal(123, newStatus.Data.Id);
            Assert.True(status.Details.ContainsKey("Test subject"));
        }

        [Fact]
        public void TestConvertTo()
        {
            var status = new Status<ExampleModel>();
            status.SetData(new ExampleModel { Age = 30 });
            var newStatus = status.ConvertTo<ExampleModel>();

            Assert.Equal(status.IsSuccessful, newStatus.IsSuccessful);
            Assert.Equal(status.Message, newStatus.Message);
            Assert.Equal(30, newStatus.Data.Age);
            Assert.Equal(status.Details, newStatus.Details);
        }
    }
}