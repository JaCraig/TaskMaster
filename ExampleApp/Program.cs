using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExampleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            new ServiceCollection().AddCanisterModules();
            Canister.Builder.Bootstrapper.Resolve<TaskMaster.TaskMaster>().Run(args);
            Console.ReadKey();
        }
    }
}