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
            #region Admin
            builder.Services.AddScoped<IAdminSanctionPostAgent, AdminSanctionPostAgent>();
            #endregion
            builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            builder.Services.AddScoped<ICoditechLogging, CoditechLogging>();
            builder.Services.AddScoped<IUserAgent, UserAgent>();
            builder.Services.AddScoped<IGeneralDepartmentAgent, GeneralDepartmentAgent>();
            builder.Services.AddScoped<IGeneralCountryAgent, GeneralCountryAgent>();
            builder.Services.AddScoped<IGeneralNationalityAgent, GeneralNationalityAgent>();
            builder.Services.AddScoped<IGeneralDesignationAgent, GeneralDesignationAgent>();
            builder.Services.AddScoped<IGeneralCityAgent, GeneralCityAgent>();
            builder.Services.AddScoped<IGeneralTaxMasterAgent, GeneralTaxMasterAgent>();
            builder.Services.AddScoped<IGeneralTaxGroupAgent, GeneralTaxGroupAgent>();
            builder.Services.AddScoped<IOrganisationAgent, OrganisationAgent>();
            builder.Services.AddScoped<IOrganisationCentreAgent, OrganisationCentreAgent>();
            #endregion

            #region Client
            #region Admin
            builder.Services.AddScoped<IAdminSanctionPostClient, AdminSanctionPostClient>();
            #endregion
            builder.Services.AddScoped<IUserClient, UserClient>();
            builder.Services.AddScoped<IGeneralDepartmentClient, GeneralDepartmentClient>();
            builder.Services.AddScoped<IGeneralCountryClient, GeneralCountryClient>();
            builder.Services.AddScoped<IGeneralNationalityClient, GeneralNationalityClient>();
            builder.Services.AddScoped<IGeneralDesignationClient, GeneralDesignationClient>();
            builder.Services.AddScoped<IGeneralCityClient, GeneralCityClient>();
            builder.Services.AddScoped<IGeneralTaxMasterClient, GeneralTaxMasterClient>();
            builder.Services.AddScoped<IGeneralTaxGroupClient, GeneralTaxGroupClient>();
            builder.Services.AddScoped<IOrganisationClient, OrganisationClient>();
            builder.Services.AddScoped<IOrganisationCentreClient, OrganisationCentreClient>();
            #endregion
        }
    }
}
