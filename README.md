# TaskMaster

[![.NET Publish](https://github.com/JaCraig/TaskMaster/actions/workflows/dotnet-publish.yml/badge.svg)](https://github.com/JaCraig/TaskMaster/actions/workflows/dotnet-publish.yml)

TaskMaster is a lightweight C# library that provides functionality for running tasks based on specific criteria. It simplifies the process of setting up and executing tasks in your application.

## Basic Usage

In order to use TaskMaster, you must wire it up first by adding it to your ServiceCollection:

```csharp
serviceCollection.AddCanisterModules();
```
Once Canister is configured, you can create a new instance of the TaskMaster service:

```csharp
var Runner = services.GetService<TaskMaster>();
Runner.Run(args);
```

The TaskMaster class handles task discovery, prioritization, and execution. It also logs any errors it encounters using Serilog. If Serilog is not registered with Canister, a default empty logger will be used. However, if a logger is specified, events will be logged using the ILogger class.

## Creating a Task

Creating a task with TaskMaster is straightforward. Simply inherit from the `ITask` interface and implement its methods. Here's an example of a basic "Hello World" task:

```csharp
/// <summary>
/// Basic hello world task
/// </summary>
public class HelloWorldTask : ITask
{
    public IFrequency[] Frequencies => new IFrequency[] { new RunAlways() };

    public string Name => "Hello World";

    public int Priority => 1;

    public bool Execute(DateTime lastRun)
    {
        Console.WriteLine("Hello World");
        return true;
    }

    public bool Initialize(IDataManager dataManager)
    {
        return true;
    }
}
```

In this example, the task runs every time the `Run` method of the TaskMaster is called. However, you can specify different frequencies for execution. The task's name is "Hello World," which is used for logging purposes. The priority is set to 1, determining the execution order. Tasks with lower priority values are executed first, and tasks with the same priority may run in parallel.

The `Initialize` method is called when the task is created and receives an `IDataManager` instance, which handles saving and retrieving configuration data for the task. By default, the data manager saves configuration data as JSON-serialized strings, but you can implement your own data manager for customization.

The `Execute` method is where the actual work of the task should be performed.

## Installation

TaskMaster is available as a NuGet package. You can install it by running the following command in the Package Manager Console:

```shell
Install-Package TaskMaster
```

## Build Process

To build the library, ensure you have the following minimum requirements:

- Visual Studio 2017

Clone the project repository, and you should be able to load the solution in Visual Studio and build it without any issues.

For any further assistance or information, please refer to the project documentation or reach out to the project contributors.

Enjoy using TaskMaster in your applications!
