using Microsoft.Extensions.DependencyInjection;
using System;

namespace TaskMaster
{
    /// <summary>
    /// Services
    /// </summary>
    internal static class Services
    {
        /// <summary>
        /// The service provider lock
        /// </summary>
        private static readonly object ServiceProviderLock = new();

        /// <summary>
        /// The service provider
        /// </summary>
        private static IServiceProvider? _ServiceProvider;

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>The service provider.</value>
        public static IServiceProvider? ServiceProvider
        {
            get
            {
                if (_ServiceProvider is not null)
                    return _ServiceProvider;
                lock (ServiceProviderLock)
                {
                    if (_ServiceProvider is not null)
                        return _ServiceProvider;
                    _ServiceProvider = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider();
                }
                return _ServiceProvider;
            }
        }
    }
}