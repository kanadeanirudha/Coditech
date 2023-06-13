using Microsoft.Extensions.DependencyInjection;

namespace Coditech.Common.Helper
{
    public class CoditechDependencyResolver : ICoditechDependencyResolver
    {
        public static IServiceProvider _staticServiceProvider;
        /// <summary>
        /// Gets a service of the specified type from the static service provider.
        /// </summary>
        /// <typeparam name="T">The type of service to get.</typeparam>
        /// <returns>The service instance.</returns>
        public static T GetService<T>() where T : class
        {
            return (T)_staticServiceProvider.GetService<T>();
        }
    }
}
