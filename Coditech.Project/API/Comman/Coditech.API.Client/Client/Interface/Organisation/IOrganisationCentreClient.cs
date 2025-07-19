using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
namespace Coditech.API.Client
{
    public interface IOrganisationCentreClient : IBaseClient
    {
        /// <summary>
        /// Get list of OrganisationCentre.
        /// </summary>
        /// <returns>OrganisationCentreListResponse</returns>
        OrganisationCentreListResponse List(int adminRoleMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create OrganisationCentre.
        /// </summary>
        /// <param name="OrganisationCentreModel">OrganisationCentreModel.</param>
        /// <returns>Returns OrganisationCentreResponse.</returns>
        OrganisationCentreResponse CreateOrganisationCentre(OrganisationCentreModel body);

        /// <summary>
        /// Get OrganisationCentre by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentreResponse.</returns>
        OrganisationCentreResponse GetOrganisationCentre(int organisationCentreId);

        /// <summary>
        /// Update OrganisationCentre.
        /// </summary>
        /// <param name="OrganisationCentreModel">OrganisationCentreModel.</param>
        /// <returns>Returns updated OrganisationCentreResponse</returns>
        OrganisationCentreResponse UpdateOrganisationCentre(OrganisationCentreModel body);

        /// <summary>
        /// Delete OrganisationCentre.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteOrganisationCentre(ParameterModel body);

        /// <summary>
        /// Get OrganisationCentrePrintingFormat by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrePrintingFormatResponse.</returns>
        OrganisationCentrePrintingFormatResponse GetPrintingFormat(int organisationCentreId);

        /// <summary>
        /// Update OrganisationCentrePrintingFormat.
        /// </summary>
        /// <param name="OrganisationCentrePrintingFormatModel">OrganisationCentrePrintingFormatModel.</param>
        /// <returns>Returns updated OrganisationCentrePrintingFormatResponse</returns>
        OrganisationCentrePrintingFormatResponse UpdatePrintingFormat(OrganisationCentrePrintingFormatModel body);

        /// <summary>
        /// Get OrganisationCentrewiseGSTCredential by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseGSTCredentialResponse.</returns>
        OrganisationCentrewiseGSTCredentialResponse GetCentrewiseGSTSetup(int organisationCentreId);

        /// <summary>
        /// Update OrganisationCentrewiseGSTCredential.
        /// </summary>
        /// <param name="OrganisationCentrewiseGSTCredentialModel">OrganisationCentrewiseGSTCredentialModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseGSTCredentialResponse</returns>
        OrganisationCentrewiseGSTCredentialResponse UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialModel body);

        /// <summary>
        /// Get OrganisationCentrewiseSmtpSetting by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseSmtpSettingResponse.</returns>
        OrganisationCentrewiseSmtpSettingResponse GetCentrewiseSmtpSetup(int organisationCentreId);

        /// <summary>
        /// Update OrganisationCentrewiseSmtpSetting.
        /// </summary>
        /// <param name="OrganisationCentrewiseSmtpSettingModel">OrganisationCentrewiseSmtpSettingModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseSmtpSettingResponse</returns>
        OrganisationCentrewiseSmtpSettingResponse UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingModel body);

        /// <summary>
        /// Get OrganisationCentrewiseSmsSetting by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseSmsSettingResponse.</returns>
        OrganisationCentrewiseSmsSettingResponse GetCentrewiseSmsSetup(int organisationCentreId, byte generalSmsProviderId);

        /// <summary>
        /// Update OrganisationCentrewiseSmsSetting.
        /// </summary>
        /// <param name="OrganisationCentrewiseSmsSettingModel">OrganisationCentrewiseSmsSettingModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseSmsSettingResponse</returns>
        OrganisationCentrewiseSmsSettingResponse UpdateCentrewiseSmsSetup(OrganisationCentrewiseSmsSettingModel body);

        /// <summary>
        /// Get OrganisationCentrewiseWhatsAppSetting by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseWhatsAppSettingResponse.</returns>
        OrganisationCentrewiseWhatsAppSettingResponse GetCentrewiseWhatsAppSetup(int organisationCentreId, byte generalWhatsAppProviderId);

        /// <summary>
        /// Update OrganisationCentrewiseWhatsAppSetting.
        /// </summary>
        /// <param name="OrganisationCentrewiseWhatsAppSettingModel">OrganisationCentrewiseWhatsAppSettingModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseWhatsAppSettingResponse</returns>
        OrganisationCentrewiseWhatsAppSettingResponse UpdateCentrewiseWhatsAppSetup(OrganisationCentrewiseWhatsAppSettingModel body);

        /// <summary>
        /// Get OrganisationCentrewiseEmailTemplate by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <param name="emailTemplateCode">emailTemplateCode</param>
        /// <returns>Returns OrganisationCentrewiseEmailTemplateResponse.</returns>
        OrganisationCentrewiseEmailTemplateResponse GetCentrewiseEmailTemplateSetup(int organisationCentreId, string emailTemplateCode, string templateType);

        /// <summary>
        /// Update OrganisationCentrewiseEmailTemplate.
        /// </summary>
        /// <param name="OrganisationCentrewiseEmailTemplateModel">OrganisationCentrewiseEmailTemplateModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseEmailTemplateResponse</returns>
        OrganisationCentrewiseEmailTemplateResponse UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateModel body);

        /// <summary>
        /// Get OrganisationCentrewiseUserName by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseUserNameRegistrationResponse.</returns>
        OrganisationCentrewiseUserNameRegistrationResponse GetCentrewiseUserName(int organisationCentreId, int organisationCentrewiseUserNameRegistrationId);

        /// <summary>
        /// Update OrganisationCentrewiseUserName.
        /// </summary>
        /// <param name="OrganisationCentrewiseUserNameRegistrationModel">OrganisationCentrewiseUserNameRegistrationModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseUserNameRegistrationResponse</returns>
        OrganisationCentrewiseUserNameRegistrationResponse UpdateCentrewiseUserName(OrganisationCentrewiseUserNameRegistrationModel body);

        /// <summary>
        /// Get OrganisationCentrewiseSMSTemplate by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <param name="emailTemplateCode">emailTemplateCode</param>
        /// <returns>Returns OrganisationCentrewiseEmailTemplateResponse.</returns>
        OrganisationCentrewiseEmailTemplateResponse GetCentrewiseSMSTemplateSetup(int organisationCentreId, string emailTemplateCode);

        /// <summary>
        /// Update OrganisationCentrewiseSMSTemplate.
        /// </summary>
        /// <param name="OrganisationCentrewiseEmailTemplateModel">OrganisationCentrewiseEmailTemplateModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseEmailTemplateResponse</returns>
        OrganisationCentrewiseEmailTemplateResponse UpdateCentrewiseSMSTemplateSetup(OrganisationCentrewiseEmailTemplateModel body);

        /// <summary>
        /// Get OrganisationCentrewiseWhatsAppTemplate by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <param name="emailTemplateCode">emailTemplateCode</param>
        /// <returns>Returns OrganisationCentrewiseEmailTemplateResponse.</returns>
        OrganisationCentrewiseEmailTemplateResponse GetCentrewiseWhatsAppTemplateSetup(int organisationCentreId, string emailTemplateCode);

        /// <summary>
        /// Update OrganisationCentrewiseWhatsAppTemplate.
        /// </summary>
        /// <param name="OrganisationCentrewiseEmailTemplateModel">OrganisationCentrewiseEmailTemplateModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseEmailTemplateResponse</returns>
        OrganisationCentrewiseEmailTemplateResponse UpdateCentrewiseWhatsAppTemplateSetup(OrganisationCentrewiseEmailTemplateModel body);
        OrganisationCentrewiseSmtpSettingSendTestEmailResponse SendTestModal(OrganisationCentrewiseSmtpSettingSendTestEmailModel body);
    }
}
