using TaskMaster.Tests.BaseClasses;
using Xunit;

namespace TaskMaster.Tests
{
    public class TaskMasterTests : TestingDirectoryFixture
    {
        [Fact]
        public void CreateAndRun()
        {
            var TestObject = new TaskMaster();
            Assert.True(TestObject.Run());
        }
    }
}