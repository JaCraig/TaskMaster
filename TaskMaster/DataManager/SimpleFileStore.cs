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
using FileCurator;
using System;
using TaskMaster.Interfaces;

namespace TaskMaster.DataManager
{
    /// <summary>
    /// Simple file store.
    /// </summary>
    /// <seealso cref="IDataManager"/>
    public abstract class SimpleFileStore : IDataManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleFileStore"/> class.
        /// </summary>
        /// <param name="location">The location to save to.</param>
        /// <param name="serialBox">The serial box.</param>
        protected SimpleFileStore(string location, SerialBox.SerialBox serialBox)
        {
            SerialBox = serialBox;
            Location = location;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; }

        /// <summary>
        /// Gets the serial box.
        /// </summary>
        /// <value>The serial box.</value>
        public SerialBox.SerialBox SerialBox { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The data associated with the task</returns>
        public dynamic GetData(ITask task)
        {
            new DirectoryInfo(Location + "Data/").Create();
            var File = new FileInfo(Location + "Data/" + task.Name + ".txt");
            if (!File.Exists)
                return new Dynamo();
            return SerialBox.Deserialize<string, Dynamo>(new FileInfo(Location + "Data/" + task.Name + ".txt").Read());
        }

        /// <summary>
        /// Gets the last run date/time.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The last run date/time.</returns>
        public DateTime GetLastRun(ITask task)
        {
            new DirectoryInfo(Location + "LastRun/").Create();
            var File = new FileInfo(Location + "LastRun/" + task.Name + ".txt");
            if (!File.Exists)
                return DateTime.MinValue;
            return SerialBox.Deserialize<string, DateTime>(File.Read());
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="data">The data.</param>
        /// <returns>True if it is saved properly, false otherwise.</returns>
        public bool SetData(ITask task, dynamic data)
        {
            new DirectoryInfo(Location + "Data/").Create();
            new FileInfo(Location + "Data/" + task.Name + ".txt").Write(SerialBox.Serialize<Dynamo, string>(data));
            return true;
        }

        /// <summary>
        /// Sets the last run date/time.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="time">The last run date/time.</param>
        /// <returns>True if it succeeds, false otherwise.</returns>
        public bool SetLastRun(ITask task, DateTime time)
        {
            new DirectoryInfo(Location + "LastRun/").Create();
            new FileInfo(Location + "LastRun/" + task.Name + ".txt").Write(SerialBox.Serialize<DateTime, string>(time));
            return true;
        }
    }
}