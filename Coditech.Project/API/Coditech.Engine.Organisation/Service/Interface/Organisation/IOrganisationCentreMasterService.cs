using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
using System.Collections.Specialized;

namespace Coditech.API.Organisation.Service.Interface.Organisation
{
    public interface IOrganisationCentreMasterService
    {
        OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentreModel CreateOrganisationCentre(OrganisationCentreModel model);
        OrganisationCentreModel GetOrganisationCentre(short organisationCentreMasterId);
        bool UpdateOrganisationCentre(OrganisationCentreModel model);
        bool DeleteOrganisationCentre(ParameterModel parameterModel);
        OrganisationCentrePrintingFormatModel GetPrintingFormat(short organisationCentreMasterId);
        bool UpdatePrintingFormat(OrganisationCentrePrintingFormatModel model);
        OrganisationCentrewiseGSTCredentialModel GetCentrewiseGSTSetup(short organisationCentreMasterId);
        bool UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialModel model);
        OrganisationCentrewiseSmtpSettingModel GetCentrewiseSmtpSetup(short organisationCentreMasterId);
        bool UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingModel model);
        OrganisationCentrewiseSmsSettingModel GetCentrewiseSmsSetup(short organisationCentreMasterId, byte generalSmsProviderId);
        bool UpdateCentrewiseSmsSetup(OrganisationCentrewiseSmsSettingModel model);
        OrganisationCentrewiseEmailTemplateModel GetCentrewiseEmailTemplateSetup(short organisationCentreMasterId, string emailTemplateCode);
        bool UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateModel model);
        OrganisationCentrewiseUserNameRegistrationModel GetCentrewiseUserName(short organisationCentreMasterId,short organisationCentrewiseUserNameRegistrationId=0);
        bool UpdateCentrewiseUserName(OrganisationCentrewiseUserNameRegistrationModel model);
    }
}

