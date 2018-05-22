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
using System;
using TaskMaster.Interfaces;

namespace TaskMaster.Frequency
{
    /// <summary>
    /// Runs a trigger weekly
    /// </summary>
    /// <seealso cref="IFrequency"/>
    public class RunWeekly : IFrequency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunWeekly"/> class.
        /// </summary>
        /// <param name="dayToRun">The day to run.</param>
        public RunWeekly(DayOfWeek dayToRun)
        {
            DayToRun = dayToRun;
        }

        /// <summary>
        /// Gets the day to run.
        /// </summary>
        /// <value>The day to run.</value>
        public DayOfWeek DayToRun { get; }

        /// <summary>
        /// Determines whether this instance can run based on the specified last run.
        /// </summary>
        /// <param name="lastRun">The last run.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>True if it can, false otherwise</returns>
        public bool CanRun(DateTime lastRun, DateTime currentTime)
        {
            var RunAfterDate = currentTime.BeginningOf(TimeFrame.Week).AddDays((int)DayToRun);
            return lastRun < RunAfterDate && currentTime >= RunAfterDate;
        }
    }
}