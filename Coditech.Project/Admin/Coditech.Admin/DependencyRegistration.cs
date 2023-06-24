using Coditech.Admin.Agents;
using Coditech.API.Client;
using Coditech.Common.Logger;

namespace Coditech.Admin
{
    public static class DependencyRegistration
    {
        public static void RegisterDI(this WebApplicationBuilder builder)
        {
            #region Agent
            builder.Services.AddScoped<ICoditechLogging, CoditechLogging>();
            builder.Services.AddScoped<IUserAgent, UserAgent>();
            #endregion

            #region Client
            builder.Services.AddScoped<IUserClient, UserClient>();
            #endregion
        }
    }
}
