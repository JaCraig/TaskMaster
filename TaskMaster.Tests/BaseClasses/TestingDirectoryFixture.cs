using FileCurator;
using System;
using Xunit;

namespace TaskMaster.Tests.BaseClasses
{
    [Collection("DirectoryCollection")]
    public class TestingDirectoryFixture : IDisposable
    {
        public TestingDirectoryFixture()
        {
        }

        public void Dispose()
        {
            new DirectoryInfo("./TaskMaster").Delete();
        }
    }
}