using FileCurator;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace TaskMaster.Tests.BaseClasses
{
    [Collection("DirectoryCollection")]
    public class TestingDirectoryFixture : IDisposable
    {
        public TestingDirectoryFixture()
        {
            if (Canister.Builder.Bootstrapper == null)
            {
                new ServiceCollection().AddCanisterModules(x => x.AddAssembly(typeof(TestingDirectoryFixture).Assembly)
                   .RegisterTaskMaster());
            }
        }

        public void Dispose()
        {
            new DirectoryInfo("./TaskMaster").Delete();
        }
    }
}