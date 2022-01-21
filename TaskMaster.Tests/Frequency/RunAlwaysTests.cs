using System;
using TaskMaster.Commands;
using TaskMaster.Frequency;
using Xunit;

namespace TaskMaster.Tests.Frequency
{
    public class RunAlwaysTests
    {
        [Fact]
        public void CanRun()
        {
            var TestObject = new RunAlways();
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(1999, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2001, 1, 1) }, new DateTime(2000, 1, 1)));
            Assert.True(TestObject.CanRun(new LastRunInfo { LastRunStart = new DateTime(2000, 1, 1) }, new DateTime(2000, 1, 1)));
        }
    }
}