using System;
using System.Threading.Tasks;
using TaskMaster.Commands;
using TaskMaster.Frequency;
using TaskMaster.Interfaces;
using TaskMaster.Tests.BaseClasses;
using TaskMaster.Triggers;
using Xunit;

namespace TaskMaster.Tests.Triggers
{
    public class TriggerTests : TestingDirectoryFixture
    {
        [Fact]
        public void Creation()
        {
            var TestObject = new Trigger(new TestTask(), null, null);
            Assert.Single(TestObject.Frequencies);
            Assert.Equal(new LastRunInfo { LastRunStart = new DateTime(1, 1, 1) }, TestObject.LastRun);
            Assert.IsType<TestTask>(TestObject.Task);
        }

        [Fact]
        public async System.Threading.Tasks.Task RunAsync()
        {
            var TestObject = new Trigger(new TestTask(), null, null);
            Assert.True(await TestObject.RunAsync());
        }

        private class TestTask : ITask
        {
            public bool Active => true;
            public IFrequency[] Frequencies => new IFrequency[] { new RunAlways() };
            public string Name => "Test task";

            public int Priority => 1;

            public Task<bool> ExecuteAsync(LastRunInfo lastRun)
            {
                return Task.FromResult(true);
            }

            public bool Initialize(IDataManager dataManager)
            {
                return true;
            }
        }
    }
}