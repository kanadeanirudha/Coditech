using Coditech.Common.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.API.Service
{
    public static class DependencyRegistration
    {
        public static void RegisterDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICoditechLogging, CoditechLogging>();
            builder.Services.AddScoped<IMediaManagerService, MediaManagerService>();
            builder.Services.AddScoped<IMediaSettingMasterService, MediaSettingMasterService>();
        }
    }
}
