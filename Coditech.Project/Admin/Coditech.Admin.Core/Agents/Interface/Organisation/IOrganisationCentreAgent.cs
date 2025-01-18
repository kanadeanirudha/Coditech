using Coditech.Admin.ViewModel;
namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentreAgent
    {
        /// <summary>
        /// Get list of Organisation Centre.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentreListViewModel</returns>
        OrganisationCentreListViewModel GetOrganisationCentreList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Organisation Centre.
        /// </summary>
        /// <param name="organisationCentreViewModel">Organisation Centre View Model.</param>
        /// <returns>Returns created model.</returns>
        OrganisationCentreViewModel CreateOrganisationCentre(OrganisationCentreViewModel organisationCentreViewModel);

        /// <summary>
        /// Get Organisation Centre by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentreViewModel.</returns>
        OrganisationCentreViewModel GetOrganisationCentre(int organisationCentreId);

        /// <summary>
        /// Update Organisation Centre.
        /// </summary>
        /// <param name="organisationCentreViewModel">organisationCentreViewModel.</param>
        /// <returns>Returns updated OrganisationCentreViewModel</returns>
        OrganisationCentreViewModel UpdateOrganisationCentre(OrganisationCentreViewModel organisationCentreViewModel);

        /// <summary>
        /// Delete Organisation Centre.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteOrganisationCentre(string organisationCentreId, out string errorMessage);

        /// <summary>
        /// Get Organisation Centre Printing Format by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrePrintingFormatViewModel.</returns>
        OrganisationCentrePrintingFormatViewModel GetPrintingFormat(int organisationCentreId);

        /// <summary>
        /// Update Organisation Centre Printing Format.
        /// </summary>
        /// <param name="organisationCentrePrintingFormatViewModel">organisationCentrePrintingFormatViewModel.</param>
        /// <returns>Returns updated OrganisationCentrePrintingFormatViewModel</returns>
        OrganisationCentrePrintingFormatViewModel UpdatePrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel);

        /// <summary>
        /// Get Organisation Centrewise GST Credential by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseGSTCredentialViewModel.</returns>
        OrganisationCentrewiseGSTCredentialViewModel GetCentrewiseGSTSetup(int organisationCentreId);

        /// <summary>
        /// Update Organisation Centrewise GST Credential.
        /// </summary>
        /// <param name="organisationCentrewiseGSTCredentialViewModel">organisationCentrePrintingFormatViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseGSTCredentialViewModel</returns>
        OrganisationCentrewiseGSTCredentialViewModel UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialViewModel organisationCentrewiseGSTCredentialViewModel);

        /// <summary>
        /// Get Organisation Centrewise Smtp Setting by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseSmtpSettingViewModel.</returns>
        OrganisationCentrewiseSmtpSettingViewModel GetCentrewiseSmtpSetup(int organisationCentreId);

        /// <summary>
        /// Update Organisation Centrewise Smtp Setting.
        /// </summary>
        /// <param name="organisationCentrewiseSmtpSettingViewModel">organisationCentrewiseSmtpSettingViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseSmtpSettingViewModel</returns>
        OrganisationCentrewiseSmtpSettingViewModel UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingViewModel organisationCentrewiseSmtpSettingViewModel);

        /// <summary>
        /// Get Organisation Centrewise Sms Setting by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseSmsSettingViewModel.</returns>
        OrganisationCentrewiseSmsSettingViewModel GetCentrewiseSmsSetup(int organisationCentreId, byte generalSmsProviderId);

        /// <summary>
        /// Update Organisation Centrewise Sms Setting.
        /// </summary>
        /// <param name="organisationCentrewiseSmsSettingViewModel">organisationCentrewiseSmsSettingViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseSmsSettingViewModel</returns>
        OrganisationCentrewiseSmsSettingViewModel UpdateCentrewiseSmsSetup(OrganisationCentrewiseSmsSettingViewModel organisationCentrewiseSmsSettingViewModel);

        /// <summary>
        /// Get Organisation Centrewise WhatsApp Setting by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseWhatsAppSettingViewModel.</returns>
        OrganisationCentrewiseWhatsAppSettingViewModel GetCentrewiseWhatsAppSetup(int organisationCentreId, byte generalWhatsAppProviderId);

        /// <summary>
        /// Update Organisation Centrewise WhatsApp Setting.
        /// </summary>
        /// <param name="organisationCentrewiseWhatsAppSettingViewModel">organisationCentrewiseWhatsAppSettingViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseWhatsAppSettingViewModel</returns>
        OrganisationCentrewiseWhatsAppSettingViewModel UpdateCentrewiseWhatsAppSetup(OrganisationCentrewiseWhatsAppSettingViewModel organisationCentrewiseWhatsAppSettingViewModel);

        /// <summary>
        /// Get Organisation Centrewise Email Template by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <param name="emailTemplateCode">emailTemplateCode</param>
        /// <returns>Returns OrganisationCentrewiseEmailTemplateViewModel.</returns>
        OrganisationCentrewiseEmailTemplateViewModel GetCentrewiseEmailTemplateSetup(int organisationCentreId, string emailTemplateCode, string templateType);

        /// <summary>
        /// Update Organisation Centrewise Email Template.
        /// </summary>
        /// <param name="organisationCentrewiseEmailTemplateViewModel">organisationCentrewiseEmailTemplateViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseEmailTemplateViewModel</returns>
        OrganisationCentrewiseEmailTemplateViewModel UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel);

        /// <summary>
        /// Get Organisation Centrewise UserName by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <returns>Returns OrganisationCentrewiseUserNameRegistrationViewModel.</returns>
        OrganisationCentrewiseUserNameRegistrationViewModel GetCentrewiseUserName(int organisationCentreId, int organisationCentrewiseUserNameRegistrationId = 0);

        /// <summary>
        /// Update Organisation Centrewise UserName.
        /// </summary>
        /// <param name="organisationCentrewiseUserNameRegistrationViewModel">organisationCentrewiseUserNameRegistrationViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseUserNameRegistrationViewModel</returns>
        OrganisationCentrewiseUserNameRegistrationViewModel UpdateCentrewiseUserName(OrganisationCentrewiseUserNameRegistrationViewModel organisationCentrewiseUserNameRegistrationViewModel);

        /// <summary>
        /// Get Organisation Centrewise SMS Template by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <param name="emailTemplateCode">emailTemplateCode</param>
        /// <returns>Returns OrganisationCentrewiseEmailTemplateViewModel.</returns>
        OrganisationCentrewiseEmailTemplateViewModel GetCentrewiseSMSTemplateSetup(int organisationCentreId, string emailTemplateCode);

        /// <summary>
        /// Update Organisation Centrewise SMS Template.
        /// </summary>
        /// <param name="organisationCentrewiseEmailTemplateViewModel">organisationCentrewiseEmailTemplateViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseEmailTemplateViewModel</returns>
        OrganisationCentrewiseEmailTemplateViewModel UpdateCentrewiseSMSTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel);

        OrganisationCentrewiseEmailTemplateViewModel GetCentrewiseWhatsAppTemplateSetup(int organisationCentreId, string emailTemplateCode);

        OrganisationCentrewiseEmailTemplateViewModel UpdateCentrewiseWhatsAppTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel);
    }
}

