# TaskMaster
TaskMaster is a basic library to help with running tasks only after certain criteria are met.

[![Build status](https://ci.appveyor.com/api/projects/status/vi7fu8ahwmd823vo?svg=true)](https://ci.appveyor.com/project/JaCraig/taskmaster)

## Basic Usage

The system relies on an IoC wrapper called [Canister](https://github.com/JaCraig/Canister). While Canister has a built in IoC container, it's purpose is to actually wrap your container of choice in a way that simplifies setup and usage for other libraries that don't want to be tied to a specific IoC container. TaskMaster uses it to detect and pull in various info. As such you must set up Canister in order to use TaskMaster:

    Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
                    .RegisterTaskMaster()
                    .Build();
					
You must also register any assemblies that will contain your tasks with Canister in order for the system to find them:

	Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
					.AddAssembly(typeof(MyTask).GetTypeInfo().Assembly)
                    .RegisterTaskMaster()
                    .Build();

This is required prior to using the TaskMaster class for the first time. Once Canister is set up, you can use the TaskMaster class:

    var Manager = new TaskMaster.TaskMaster();
    Manager.Run();

The TaskMaster class will handle discovery of tasks, prioritization, and running of tasks. It will also log any errors that it finds to Serilog. If Serilog is not registered to Canister, it will default to an empty logger that does nothing. However if one is specified it will log events as they occur to the ILogger class.

## Creating a task

Creating a task is rather simple, you just need to inherit a class from ITask:

    /// <summary>
    /// Basic hello world task
    /// </summary>
    public class HelloWorldTask : ITask
    {
        /// <summary>
        /// Gets the frequencies.
        /// </summary>
        /// <value>The frequencies.</value>
        public IFrequency[] Frequencies => new IFrequency[] { new RunAlways() };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => "Hello World";

		/// <summary>
        /// Order to run it in (items with the same Priority value will be run in parallel)
        /// </summary>
        public int Priority => 1;

        /// <summary>
        /// Executes the specified last run.
        /// </summary>
        /// <param name="lastRun">The last run.</param>
        /// <returns></returns>
        public bool Execute(DateTime lastRun)
        {
            Console.WriteLine("Hello World");
            return true;
        }

        /// <summary>
        /// Initializes the specified data manager.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <returns></returns>
        public bool Initialize(IDataManager dataManager)
        {
            return true;
        }
    }
	
The above task runs every time the TaskManager's Run method is called. However you can specify another or even multiple frequencies at which to run. The name of the task is Hello World. As such all logged events will use that name. The priority is set to 1, which determines the batch they are run in. Lower numbered items are run first and if more than one task has the same priority, they will be run at the same time in parallel.

The Initialization function is called when the item is created. The function is passed in an IDataManager class that will handle saving/getting configuration data for the task. By default the data manager saves configuration data in json serialized strings but this can be changed by creating your own data manager.

The Execute function is called when the task is actually triggered. As such that is where your task's actual work should go.

## Installation

The library is available via Nuget with the package name "TaskMaster". To install it run the following command in the Package Manager Console:

Install-Package TaskMaster

## Build Process

In order to build the library you will require the following as a minimum:

1. Visual Studio 2017

Other than that, just clone the project and you should be able to load the solution and build without too much effort.