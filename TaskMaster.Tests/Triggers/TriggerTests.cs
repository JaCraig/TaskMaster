using System;
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
            var TestObject = new Trigger(new TestTask(), null, null, 0);
            Assert.Equal(1, TestObject.Frequencies.Length);
            Assert.Equal(new DateTime(1, 1, 1), TestObject.LastRun);
            Assert.IsType<TestTask>(TestObject.Task);
        }

        [Fact]
        public void Run()
        {
            var TestObject = new Trigger(new TestTask(), null, null, 0);
            Assert.True(TestObject.Run());
        }

        private class TestTask : ITask
        {
            public IFrequency[] Frequencies => new IFrequency[] { new RunAlways() };

            public string Name => "Test task";

            public int Priority => 1;

            public bool Execute(DateTime lastRun)
            {
                return true;
            }

            public bool Initialize(IDataManager dataManager)
            {
                return true;
            }
        }
    }
}