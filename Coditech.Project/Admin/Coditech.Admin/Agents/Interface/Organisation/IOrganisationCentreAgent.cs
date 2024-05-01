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
        OrganisationCentreViewModel GetOrganisationCentre(short organisationCentreId);

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
        OrganisationCentrePrintingFormatViewModel GetPrintingFormat(short organisationCentreId);

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
        OrganisationCentrewiseGSTCredentialViewModel GetCentrewiseGSTSetup(short organisationCentreId);

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
        OrganisationCentrewiseSmtpSettingViewModel GetCentrewiseSmtpSetup(short organisationCentreId);

        /// <summary>
        /// Update Organisation Centrewise Smtp Setting.
        /// </summary>
        /// <param name="organisationCentrewiseSmtpSettingViewModel">organisationCentrewiseSmtpSettingViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseSmtpSettingViewModel</returns>
        OrganisationCentrewiseSmtpSettingViewModel UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingViewModel organisationCentrewiseSmtpSettingViewModel);

        /// <summary>
        /// Get Organisation Centrewise Email Template by organisationCentreId.
        /// </summary>
        /// <param name="organisationCentreId">organisationCentreId</param>
        /// <param name="emailTemplateCode">emailTemplateCode</param>
        /// <returns>Returns OrganisationCentrewiseEmailTemplateViewModel.</returns>
        OrganisationCentrewiseEmailTemplateViewModel GetCentrewiseEmailTemplateSetup(short organisationCentreId, string emailTemplateCode);

        /// <summary>
        /// Update Organisation Centrewise Email Template.
        /// </summary>
        /// <param name="organisationCentrewiseEmailTemplateViewModel">organisationCentrewiseEmailTemplateViewModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseEmailTemplateViewModel</returns>
        OrganisationCentrewiseEmailTemplateViewModel UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateViewModel organisationCentrewiseEmailTemplateViewModel);
    }
}

