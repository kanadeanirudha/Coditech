﻿using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class OrganisationCentreEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetOrganisationCentreList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateOrganisationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/CreateOrganisationCentre";

        public string GetOrganisationAsync(short organisationCentreId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetOrganisationCentre?organisationCentreMasterId={organisationCentreId}";
       
        public string UpdateOrganisationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateOrganisationCentre";

        public string DeleteOrganisationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/DeleteOrganisationCentre";
        public string GetPrintingFormatAsync(short organisationCentreId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetPrintingFormat?organisationCentreMasterId={organisationCentreId}";

        public string UpdatePrintingFormatAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdatePrintingFormat";

        public string GetCentrewiseGSTSetupAsync(short organisationCentreId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetCentrewiseGSTSetup?organisationCentreMasterId={organisationCentreId}";

        public string UpdateCentrewiseGSTSetupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateCentrewiseGSTSetup";

        public string GetCentrewiseSmtpSetupAsync(short organisationCentreId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetCentrewiseSmtpSetup?organisationCentreMasterId={organisationCentreId}";

        public string UpdateCentrewiseSmtpSetupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateCentrewiseSmtpSetup";
        public string GetCentrewiseSmsSetupAsync(short organisationCentreId) =>
          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetCentrewiseSmsSetup?organisationCentreMasterId={organisationCentreId}";

        public string UpdateCentrewiseSmsSetupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateCentrewiseSmsSetup";

        public string GetCentrewiseEmailTemplateSetupAsync(short organisationCentreId, string emailTemplateCode) =>
          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetCentrewiseEmailTemplateSetup?organisationCentreMasterId={organisationCentreId}&emailTemplateCode={emailTemplateCode}";

        public string UpdateCentrewiseEmailTemplateSetupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateCentrewiseEmailTemplateSetup";
        public string GetCentrewiseUserNameAsync(short organisationCentreId,short organisationCentrewiseUserNameRegistrationId) =>
         $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/GetCentrewiseUserName?organisationCentreMasterId={organisationCentreId}&organisationCentrewiseUserNameRegistrationId={organisationCentrewiseUserNameRegistrationId}";
        public string UpdateCentrewiseUserNameAsync() =>
              $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentreMaster/UpdateCentrewiseUserName";
    }
}
