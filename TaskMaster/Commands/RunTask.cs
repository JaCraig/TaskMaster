using Canister.Interfaces;
using Monarch.Commands.BaseClasses;
using System.Threading.Tasks;

namespace TaskMaster.Commands
{
    /// <summary>
    /// Runs a specific task
    /// </summary>
    /// <seealso cref="CommandBaseClass{TaskName}"/>
    public class RunTask : CommandBaseClass<TaskName>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunTask"/> class.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public RunTask(IBootstrapper bootstrapper)
        {
            Bootstrapper = bootstrapper;
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public override string[] Aliases => new string[] { "run" };

        /// <summary>
        /// Gets the bootstrapper.
        /// </summary>
        /// <value>The bootstrapper.</value>
        public IBootstrapper Bootstrapper { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description => "Run a specific task";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Run";

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        protected override async Task<int> Run(TaskName input)
        {
            await Task.CompletedTask;
            Bootstrapper.Resolve<TaskMaster>()?.Run(input.Name);
            return 0;
        }
    }
}