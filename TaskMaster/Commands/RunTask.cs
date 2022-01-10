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
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public override string[] Aliases => new string[] { "run" };

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
        protected override Task<int> Run(TaskName input)
        {
            TaskMaster.Instance?.Run(input.Name ?? "");
            return Task.FromResult(0);
        }
    }
}