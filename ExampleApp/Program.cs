using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using TaskMaster.Registration;

namespace ExampleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
                    .AddAssembly(typeof(Program).GetTypeInfo().Assembly)
                    .RegisterTaskMaster()
                    .Build();
            Canister.Builder.Bootstrapper.Resolve<TaskMaster.TaskMaster>().Run(args);
            Console.ReadKey();
        }
    }
}