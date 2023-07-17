using BigBook;
using Microsoft.Extensions.DependencyInjection;

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

using TaskMaster.Interfaces;

namespace TaskMaster.Commands
{
    /// <summary>
    /// Task listing
    /// </summary>
    public class TaskListing
    {
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[Values: ({Services.ServiceProvider?.GetServices<ITask>().ToString(x => x.Name)})]";
        }
    }
}