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

using TaskMaster.Commands;

namespace TaskMaster.Interfaces
{
    /// <summary>
    /// Data manager
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The data associated with the task</returns>
        dynamic? GetData(ITask task);

        /// <summary>
        /// Gets the last run date/time.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The last run date/time.</returns>
        LastRunInfo GetLastRun(ITask task);

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="data">The data.</param>
        /// <returns>True if it is saved properly, false otherwise.</returns>
        bool SetData(ITask task, dynamic data);

        /// <summary>
        /// Sets the last run date/time.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="runInfo">The run information.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        bool SetLastRun(ITask task, LastRunInfo runInfo);
    }
}