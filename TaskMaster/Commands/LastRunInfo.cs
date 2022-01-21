using System;

namespace TaskMaster.Commands
{
    /// <summary>
    /// Last run info
    /// </summary>
    public struct LastRunInfo
    {
        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public static LastRunInfo MinValue => new LastRunInfo { LastRunEnd = DateTime.MinValue, LastRunStart = DateTime.MinValue };

        /// <summary>
        /// Gets or sets the last run end.
        /// </summary>
        /// <value>The last run end.</value>
        public DateTime LastRunEnd { get; set; }

        /// <summary>
        /// Gets or sets the last run start.
        /// </summary>
        /// <value>The last run start.</value>
        public DateTime LastRunStart { get; set; }
    }
}