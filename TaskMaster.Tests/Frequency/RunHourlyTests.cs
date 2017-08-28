using System;
using TaskMaster.Frequency;
using Xunit;

namespace TaskMaster.Tests.Frequency
{
    public class RunHourlyTests
    {
        [Fact]
        public void CanRun()
        {
            var TestObject = new RunHourly();
            Assert.True(TestObject.CanRun(new DateTime(1999, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2001, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1, 1, 2, 0), new DateTime(2000, 1, 1, 1, 2, 0)));
            Assert.True(TestObject.CanRun(new DateTime(1999, 1, 1, 1, 1, 0), new DateTime(2000, 1, 1, 1, 2, 0)));
            Assert.True(TestObject.CanRun(new DateTime(1999, 12, 31, 23, 59, 59), new DateTime(2000, 1, 1, 0, 0, 0)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 0, 59, 0)));
            Assert.True(TestObject.CanRun(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 1, 0, 0)));
        }
    }
}