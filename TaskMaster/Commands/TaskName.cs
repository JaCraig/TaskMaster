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

using Monarch.Commands.Attributes;

namespace TaskMaster.Commands
{
    /// <summary>
    /// Task name input
    /// </summary>
    public class TaskName
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DynamicDisplay(typeof(TaskListing))]
        public string? Name { get; set; }
    }
}