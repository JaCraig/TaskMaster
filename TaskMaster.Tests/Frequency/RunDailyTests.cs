using System;
using TaskMaster.Commands;
using TaskMaster.Frequency;
using Xunit;

namespace TaskMaster.Tests.Frequency
{
    public class RunDailyTests
    {
        [Fact]
        public void CanRun()
        {
            var TestObject = new RunDaily(1, 2);
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1999, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2001, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2000, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2000, 1, 1, 1, 2, 0) }, new DateTime(2000, 1, 1, 1, 2, 0)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1999, 1, 1, 1, 1, 0) }, new DateTime(2000, 1, 1, 1, 2, 0)));
        }

        [Fact]
        public void RunAfterNoon()
        {
            var TestObject = new RunDaily(12);
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 12, 0, 0) }, DateTime.Today.AddHours(12)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 13, 0, 0) }, DateTime.Today.AddHours(13)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 12, 30, 0) }, DateTime.Today.AddHours(12).AddMinutes(30)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 11, 0, 0) }, DateTime.Today.AddHours(11)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 1, 0, 0) }, DateTime.Today.AddHours(1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 11, 59, 59) }, DateTime.Today.AddHours(11).AddMinutes(59).AddSeconds(59)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = DateTime.Today }, DateTime.Today));
        }
    }
}