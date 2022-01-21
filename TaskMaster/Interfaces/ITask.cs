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

using System.Threading.Tasks;
using TaskMaster.Commands;

namespace TaskMaster.Interfaces
{
    /// <summary>
    /// Task interface
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITask"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        bool Active { get; }

        /// <summary>
        /// Gets the frequencies.
        /// </summary>
        /// <value>The frequencies.</value>
        IFrequency[] Frequencies { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Order to run it in (items with the same Priority value will be run in parallel)
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Executes this task
        /// </summary>
        /// <param name="lastRun">The last run date.</param>
        /// <returns>Returns true if it runs successfully, false otherwise</returns>
        Task<bool> ExecuteAsync(LastRunInfo lastRun);

        /// <summary>
        /// Initializes the task using the specified data manager.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <returns>True if it initializes correctly, false otherwise.</returns>
        bool Initialize(IDataManager dataManager);
    }
}