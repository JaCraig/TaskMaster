using System;
using TaskMaster.Frequency;
using TaskMaster.Interfaces;

namespace ExampleApp
{
    /// <summary>
    /// Basic hello world task
    /// </summary>
    public class HelloTask : ITask
    {
        /// <summary>
        /// Gets the frequencies.
        /// </summary>
        /// <value>The frequencies.</value>
        public IFrequency[] Frequencies => new IFrequency[] { new RunAlways() };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => "Hello";

        /// <summary>
        /// Order to run it in (items with the same Priority value will be run in parallel)
        /// </summary>
        public int Priority => 1;

        /// <summary>
        /// Gets or sets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        private IDataManager DataManager { get; set; }

        /// <summary>
        /// Executes the specified last run.
        /// </summary>
        /// <param name="lastRun">The last run.</param>
        /// <returns></returns>
        public bool Execute(DateTime lastRun)
        {
            Console.Write("Hello");
            return true;
        }

        /// <summary>
        /// Initializes the specified data manager.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <returns></returns>
        public bool Initialize(IDataManager dataManager)
        {
            return true;
        }
    }
}