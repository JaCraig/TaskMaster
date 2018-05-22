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
            var Bootstrapper = Canister.Builder.Bootstrapper;
            var Manager = new TaskMaster.TaskMaster();
            Manager.Run();
            Console.ReadKey();
        }
    }
}