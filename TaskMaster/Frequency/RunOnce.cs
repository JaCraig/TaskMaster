using System;
using TaskMaster.Interfaces;

namespace TaskMaster.Frequency
{
    /// <summary>
    /// Runs the task one time
    /// </summary>
    /// <seealso cref="IFrequency"/>
    public class RunOnce : IFrequency
    {
        /// <summary>
        /// Determines whether this instance can run based on the specified last run.
        /// </summary>
        /// <param name="lastRun">The last run.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>True if it can, false otherwise</returns>
        public bool CanRun(DateTime lastRun, DateTime currentTime)
        {
            return lastRun == DateTime.MinValue;
        }
    }
}