using System;
using TaskMaster.Frequency;
using Xunit;

namespace TaskMaster.Tests.Frequency
{
    public class RunMonthlyTests
    {
        [Fact]
        public void CanRun()
        {
            var TestObject = new RunMonthly(1);
            Assert.True(TestObject.CanRun(new DateTime(1999, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2001, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1, 1, 2, 0), new DateTime(2000, 1, 1, 1, 2, 0)));
            Assert.True(TestObject.CanRun(new DateTime(1999, 1, 1, 1, 1, 0), new DateTime(2000, 1, 2)));
        }

        [Fact]
        public void RunAfterNoon()
        {
            var TestObject = new RunMonthly(14);
            Assert.True(TestObject.CanRun(new DateTime(1900, 1, 1, 12, 0, 0), new DateTime(2016, 2, 16)));
            Assert.True(TestObject.CanRun(new DateTime(1900, 1, 1, 13, 0, 0), new DateTime(2016, 2, 17)));
            Assert.True(TestObject.CanRun(new DateTime(1900, 1, 1, 12, 30, 0), new DateTime(2016, 2, 18)));
            Assert.False(TestObject.CanRun(new DateTime(1900, 1, 1, 11, 0, 0), new DateTime(2016, 2, 1)));
            Assert.False(TestObject.CanRun(new DateTime(1900, 1, 1, 1, 0, 0), new DateTime(2016, 2, 10)));
            Assert.False(TestObject.CanRun(new DateTime(1900, 1, 1, 11, 59, 59), new DateTime(2016, 2, 13, 23, 59, 59)));
        }
    }
}