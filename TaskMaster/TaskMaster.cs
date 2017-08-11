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
using Serilog;
using System;
using System.Linq;
using System.Reflection;
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
        {
            var Tasks = Canister.Builder.Bootstrapper.ResolveAll<ITask>().ToArray();
            var DataManagers = Canister.Builder.Bootstrapper.ResolveAll<IDataManager>();
            DataManager = DataManagers.FirstOrDefault(x => x.GetType().GetTypeInfo().Assembly != typeof(TaskMaster).GetTypeInfo().Assembly);
            if (DataManager == null)
                DataManager = DataManagers.FirstOrDefault(x => x.GetType().GetTypeInfo().Assembly == typeof(TaskMaster).GetTypeInfo().Assembly);
            Logger = Canister.Builder.Bootstrapper.Resolve<ILogger>() ?? Log.Logger ?? new LoggerConfiguration().CreateLogger();
            Triggers = new ListMapping<int, Trigger>();
            foreach (var Task in Tasks)
            {
                Triggers.Add(Task.Priority, new Trigger(Task, Logger, DataManager));
            }
        }

        /// <summary>
        /// Gets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        private IDataManager DataManager { get; }

        private ILogger Logger { get; }

        /// <summary>
        /// Gets the triggers.
        /// </summary>
        /// <value>The triggers.</value>
        private ListMapping<int, Trigger> Triggers { get; }

        /// <summary>
        /// Runs the tasks.
        /// </summary>
        /// <returns>True if it runs successfully, false otherwise.</returns>
        public bool Run()
        {
            try
            {
                var Result = true;
                foreach (int Priority in Triggers.Keys.OrderBy(x => x))
                {
                    Result &= Triggers[Priority].ForEachParallel(x => x.Run()).All(x => x);
                }
                return Result;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                return false;
            }
        }
    }
}