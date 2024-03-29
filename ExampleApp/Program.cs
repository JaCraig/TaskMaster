﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExampleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetService<TaskMaster.TaskMaster>().Run(args);
            Console.ReadKey();
        }
    }
}