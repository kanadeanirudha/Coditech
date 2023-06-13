using Coditech.Common.Helper;

using Microsoft.Extensions.DependencyInjection;

namespace Coditech.Common.Logger
{
    public class CoditechLoggingHelper
    {
        /// <summary>
        /// Gets the Coditech logging instance.
        /// </summary>
        /// <returns>The Coditech logging instance.</returns>
        public static ICoditechLogging CoditechLogging => CoditechDependencyResolver._staticServiceProvider?.GetService<ICoditechLogging>();
    }
}
