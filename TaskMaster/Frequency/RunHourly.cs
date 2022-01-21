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

using System;
using TaskMaster.Commands;
using TaskMaster.Interfaces;

namespace TaskMaster.Frequency
{
    /// <summary>
    /// Run hourly
    /// </summary>
    /// <seealso cref="IFrequency"/>
    public class RunHourly : IFrequency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunHourly"/> class.
        /// </summary>
        /// <param name="minuteToRun">The minute to run.</param>
        public RunHourly(int minuteToRun = 0)
        {
            MinuteToRun = minuteToRun;
        }

        /// <summary>
        /// Gets or sets the minute to run.
        /// </summary>
        /// <value>The minute to run.</value>
        public int MinuteToRun { get; set; }

        /// <summary>
        /// Determines whether this instance can run based on the specified last run.
        /// </summary>
        /// <param name="lastRun">The last run.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>True if it can, false otherwise</returns>
        public bool CanRun(LastRunInfo lastRun, DateTime currentTime)
        {
            var RunAfterDate = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, MinuteToRun, 0);
            return lastRun.LastRunStart < RunAfterDate && currentTime >= RunAfterDate;
        }
    }
}