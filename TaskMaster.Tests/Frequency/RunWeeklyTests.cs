using System;
using TaskMaster.Commands;
using TaskMaster.Frequency;
using Xunit;

namespace TaskMaster.Tests.Frequency
{
    public class RunWeeklyTests
    {
        [Fact]
        public void CanRun()
        {
            var TestObject = new RunWeekly(DayOfWeek.Monday);
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1999, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2001, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2000, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2000, 1, 1, 1, 2, 0) }, new DateTime(2000, 1, 1, 1, 2, 0)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1999, 1, 1, 1, 1, 0) }, new DateTime(2000, 1, 2)));
        }

        [Fact]
        public void RunAfterNoon()
        {
            var TestObject = new RunWeekly(DayOfWeek.Tuesday);
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 12, 0, 0) }, new DateTime(2016, 2, 16)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 13, 0, 0) }, new DateTime(2016, 2, 17)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 12, 30, 0) }, new DateTime(2016, 2, 18)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 11, 0, 0) }, new DateTime(2016, 2, 15)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 1, 0, 0) }, new DateTime(2016, 2, 14)));
            Assert.False(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1900, 1, 1, 11, 59, 59) }, new DateTime(2016, 2, 15, 23, 59, 59)));
        }
    }
}