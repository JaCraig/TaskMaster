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

using SerialBox.Interfaces;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskMaster.DataManager;
using TaskMaster.Frequency;
using TaskMaster.Interfaces;

namespace TaskMaster.Triggers
{
    /// <summary>
    /// Determines when a task is triggered
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trigger"/> class.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <exception cref="ArgumentNullException">logger</exception>
        public Trigger(ITask task, ILogger logger, IDataManager dataManager)
        {
            DataManager = dataManager ?? new DefaultDataManager(Canister.Builder.Bootstrapper?.Resolve<SerialBox.SerialBox>() ?? new SerialBox.SerialBox(Array.Empty<ISerializer>()));
            Logger = logger ?? Log.Logger ?? new LoggerConfiguration().CreateLogger() ?? throw new ArgumentNullException(nameof(logger));
            Frequencies = task.Frequencies ?? new IFrequency[] { new RunAlways() };
            Task = task;
            Priority = Task.Priority;

            Task.Initialize(DataManager);
            LastRun = DataManager.GetLastRun(Task);
        }

        /// <summary>
        /// Gets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        public IDataManager DataManager { get; }

        /// <summary>
        /// Gets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public IFrequency[] Frequencies { get; }

        /// <summary>
        /// Gets the last run.
        /// </summary>
        /// <value>The last run.</value>
        public DateTime LastRun { get; }

        /// <summary>
        /// Priority to run in.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <value>The task.</value>
        public ITask Task { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger Logger { get; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>True if it ran successfully, false otherwise</returns>
        public async Task<bool> RunAsync()
        {
            Logger.Information("Initializing task: {Name:l}", Task.Name);
            try
            {
                if (Frequencies.Any(x => x.CanRun(LastRun, DateTime.Now)))
                {
                    Logger.Information("Beginning task: {Name:l}", Task.Name);
                    var Result = await Task.ExecuteAsync(LastRun).ConfigureAwait(false);
                    DataManager.SetLastRun(Task, DateTime.Now);
                    Logger.Information("Task {Name:l} ended", Task.Name);
                    return Result;
                }
                Logger.Information("Task skipped based on schedule: {Name:l}", Task.Name);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            Logger.Information("Task {Name:l} ended", Task.Name);
            return false;
        }
    }
}