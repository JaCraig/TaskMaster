/*
Copyright 2017 James Craig
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using BigBook;
using Microsoft.Extensions.DependencyInjection;
using Monarch;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskMaster.DataManager;
using TaskMaster.Interfaces;
using TaskMaster.Triggers;

namespace TaskMaster
{
    /// <summary>
    /// Task master
    /// </summary>
    public class TaskMaster
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskMaster"/> class.
        /// </summary>
        public TaskMaster()
            : this(
                Log.Logger,
                Services.ServiceProvider.GetServices<ITask>() ?? Array.Empty<ITask>(),
                Services.ServiceProvider.GetServices<IDataManager>() ?? Array.Empty<IDataManager>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskMaster"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="tasks">The tasks.</param>
        /// <param name="dataManagers">The data managers.</param>
        public TaskMaster(ILogger logger, IEnumerable<ITask> tasks, IEnumerable<IDataManager> dataManagers)
        {
            DataManager = dataManagers.FirstOrDefault(x => !(x is DefaultDataManager))
                          ?? dataManagers.FirstOrDefault(x => x is DefaultDataManager);
            Logger = logger ?? Log.Logger ?? new LoggerConfiguration().CreateLogger();
            Triggers = new ListMapping<int, Trigger>();
            foreach (var Task in tasks)
            {
                Triggers.Add(Task.Priority, new Trigger(Task, Logger, DataManager));
            }
            Instance = this;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        internal static TaskMaster? Instance { get; private set; }

        /// <summary>
        /// Gets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        private IDataManager DataManager { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger Logger { get; }

        /// <summary>
        /// Gets the triggers.
        /// </summary>
        /// <value>The triggers.</value>
        private ListMapping<int, Trigger> Triggers { get; }

        /// <summary>
        /// Runs the tasks.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>True if it runs successfully, false otherwise.</returns>
        public bool Run(params string[] args)
        {
            try
            {
                var InternalTimer = new Stopwatch();
                InternalTimer.Start();
                if (args.Length > 0)
                    return Services.ServiceProvider.GetService<CommandRunner>()?.Run(args).GetAwaiter().GetResult() == 0;
                var Result = true;
                foreach (int Priority in Triggers.Keys.OrderBy(x => x))
                {
                    Result &= Triggers[Priority].ForEachParallel(x => x.RunAsync().GetAwaiter().GetResult()).All(x => x);
                }
                InternalTimer.Stop();
                Logger.Information("All tasks ended in {Time:l}", InternalTimer.Elapsed.ToString("g"));
                return Result;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Runs the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        internal void Run(string name)
        {
            try
            {
                var TaskToRun = Triggers.SelectMany(x => x.Value).FirstOrDefault(x => string.Equals(x.Task.Name, name, StringComparison.OrdinalIgnoreCase));
                if (TaskToRun == null)
                {
                    Console.WriteLine($"Task {TaskToRun} not found.");
                    throw new ArgumentException($"Task {TaskToRun} not found.");
                }
                TaskToRun.RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }
}