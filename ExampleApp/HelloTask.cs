using System;
using System.Threading.Tasks;
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
        /// Gets a value indicating whether this <see cref="T:TaskMaster.Interfaces.ITask"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active => true;

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
        /// Executes the specified last run.
        /// </summary>
        /// <param name="lastRun">The last run.</param>
        /// <returns></returns>
        public Task<bool> ExecuteAsync(DateTime lastRun)
        {
            Console.Write("Hello");
            return Task.FromResult(true);
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