﻿using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.API.Client;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

namespace Coditech.Admin
{
    public static class DependencyRegistration
    {
        public static void RegisterDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<CoditechTranslator>();

            #region Agent
            builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            builder.Services.AddScoped<ICoditechLogging, CoditechLogging>();
            builder.Services.AddScoped<IUserAgent, UserAgent>();
            builder.Services.AddScoped<IGeneralDepartmentAgent, GeneralDepartmentAgent>();
            builder.Services.AddScoped<IGeneralCountryAgent, GeneralCountryAgent>();
            builder.Services.AddScoped<IGeneralNationalityAgent, GeneralNationalityAgent>();
            builder.Services.AddScoped<IGeneralDesignationAgent, GeneralDesignationAgent>();
            builder.Services.AddScoped<IGeneralCityAgent, GeneralCityAgent>();
            #endregion

            #region Client
            builder.Services.AddScoped<IUserClient, UserClient>();
            builder.Services.AddScoped<IGeneralDepartmentClient, GeneralDepartmentClient>();
            builder.Services.AddScoped<IGeneralCountryClient, GeneralCountryClient>();
            builder.Services.AddScoped<IGeneralNationalityClient, GeneralNationalityClient>();
            builder.Services.AddScoped<IGeneralDesignationClient, GeneralDesignationClient>();
            builder.Services.AddScoped<IGeneralCityClient, GeneralCityClient>();
            #endregion
        }
    }
}
