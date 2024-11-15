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
        OrganisationCentreModel GetOrganisationCentre(int organisationCentreMasterId);
        bool UpdateOrganisationCentre(OrganisationCentreModel model);
        bool DeleteOrganisationCentre(ParameterModel parameterModel);
        OrganisationCentrePrintingFormatModel GetPrintingFormat(int organisationCentreMasterId);
        bool UpdatePrintingFormat(OrganisationCentrePrintingFormatModel model);
        OrganisationCentrewiseGSTCredentialModel GetCentrewiseGSTSetup(int organisationCentreMasterId);
        bool UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialModel model);
        OrganisationCentrewiseSmtpSettingModel GetCentrewiseSmtpSetup(int organisationCentreMasterId);
        bool UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingModel model);
        OrganisationCentrewiseSmsSettingModel GetCentrewiseSmsSetup(int organisationCentreMasterId, byte generalSmsProviderId);
        bool UpdateCentrewiseSmsSetup(OrganisationCentrewiseSmsSettingModel model);
        OrganisationCentrewiseWhatsAppSettingModel GetCentrewiseWhatsAppSetup(int organisationCentreMasterId, byte generalWhatsAppProviderId);
        bool UpdateCentrewiseWhatsAppSetup(OrganisationCentrewiseWhatsAppSettingModel model);
        OrganisationCentrewiseEmailTemplateModel GetCentrewiseEmailTemplateSetup(int organisationCentreMasterId, string emailTemplateCode, string templateType);
        bool UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateModel model);
        OrganisationCentrewiseUserNameRegistrationModel GetCentrewiseUserName(int organisationCentreMasterId,int organisationCentrewiseUserNameRegistrationId=0);
        bool UpdateCentrewiseUserName(OrganisationCentrewiseUserNameRegistrationModel model);
        OrganisationCentrewiseEmailTemplateModel GetCentrewiseSMSTemplateSetup(int organisationCentreMasterId, string emailTemplateCode);
        bool UpdateCentrewiseSMSTemplateSetup(OrganisationCentrewiseEmailTemplateModel model);
        OrganisationCentrewiseEmailTemplateModel GetCentrewiseWhatsAppTemplateSetup(int organisationCentreMasterId, string emailTemplateCode);
        bool UpdateCentrewiseWhatsAppTemplateSetup(OrganisationCentrewiseEmailTemplateModel model);
        bool IsCentreNameAlreadyExist(string centreName);
    }
}

