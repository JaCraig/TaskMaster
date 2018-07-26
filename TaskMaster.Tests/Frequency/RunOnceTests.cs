using System;
using TaskMaster.Frequency;
using Xunit;

namespace TaskMaster.Tests.Frequency
{
    public class RunOnceTests
    {
        [Fact]
        public void CanRun()
        {
            var TestObject = new RunOnce();
            Assert.False(TestObject.CanRun(new DateTime(1999, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2001, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1), new DateTime(2000, 1, 1)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 1, 1, 2, 0), new DateTime(2000, 1, 1, 1, 2, 0)));
            Assert.False(TestObject.CanRun(new DateTime(1999, 1, 1, 1, 1, 0), new DateTime(2000, 1, 2)));
            Assert.False(TestObject.CanRun(new DateTime(2000, 1, 2), new DateTime(2000, 1, 3)));
            Assert.True(TestObject.CanRun(new DateTime(), new DateTime(2000, 1, 3)));
        }
    }
}