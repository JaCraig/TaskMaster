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
using TaskMaster.Interfaces;

namespace TaskMaster.Frequency
{
    /// <summary>
    /// Runs once daily
    /// </summary>
    /// <seealso cref="IFrequency"/>
    public class RunDaily : IFrequency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunDaily"/> class.
        /// </summary>
        /// <param name="hourToRun">The minimum hour to run.</param>
        /// <param name="minuteToRun">The minute to run.</param>
        public RunDaily(int hourToRun, int minuteToRun = 0)
        {
            MinuteToRun = minuteToRun;
            HourToRun = hourToRun;
        }

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>The hour to run.</value>
        public int HourToRun { get; private set; }

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
        public bool CanRun(DateTime lastRun, DateTime currentTime)
        {
            return lastRun.Date != currentTime.Date
                && (HourToRun <= currentTime.Hour
                || (HourToRun == currentTime.Hour && MinuteToRun <= currentTime.Minute));
        }
    }
}