using FileCurator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using TaskMaster.Registration;
using Xunit;

namespace TaskMaster.Tests.BaseClasses
{
    [Collection("DirectoryCollection")]
    public class TestingDirectoryFixture : IDisposable
    {
        public TestingDirectoryFixture()
        {
            if (Canister.Builder.Bootstrapper == null)
                Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
                    .AddAssembly(typeof(TestingDirectoryFixture).GetTypeInfo().Assembly)
                    .RegisterTaskMaster()
                    .Build();
        }

        public void Dispose()
        {
            new DirectoryInfo("./TaskMaster").Delete();
        }
    }
}