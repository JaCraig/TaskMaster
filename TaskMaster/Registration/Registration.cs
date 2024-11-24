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

using BigBook.Registration;
using Canister.Interfaces;
using SerialBox.Registration;
using System.Reflection;
using TaskMaster.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Registration extension methods
    /// </summary>
    public static class RegistrationTaskMaster
    {
        /// <summary>
        /// Registers the library with the bootstrapper.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns>The bootstrapper</returns>
        public static ICanisterConfiguration? RegisterTaskMaster(this ICanisterConfiguration? bootstrapper)
        {
            return bootstrapper?.AddAssembly(typeof(RegistrationTaskMaster).GetTypeInfo().Assembly)
                               ?.RegisterFileCurator()
                               ?.RegisterSerialBox()
                               ?.RegisterBigBookOfDataTypes()
                               ?.RegisterMonarch();
        }

        /// <summary>
        /// Registers the TaskMaster services with the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with the TaskMaster services added.</returns>
        public static IServiceCollection? RegisterTaskMaster(this IServiceCollection? services)
        {
            if (services.Exists<TaskMaster.TaskMaster>())
                return services;
            return services?.AddAllTransient<ITask>()
                ?.AddAllTransient<IDataManager>()
                ?.AddTransient<TaskMaster.TaskMaster>()
                ?.RegisterFileCurator()
                ?.RegisterSerialBox()
                ?.RegisterBigBookOfDataTypes()
                ?.RegisterMonarch();
        }
    }
}