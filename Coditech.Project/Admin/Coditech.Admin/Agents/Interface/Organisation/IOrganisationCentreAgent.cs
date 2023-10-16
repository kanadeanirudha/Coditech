using Coditech.Admin.ViewModel;
using Coditech.ViewModel;

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
        /// Get OrganisationCentrePrintingFormat.
        /// </summary>
        /// <param name="organisationCentrePrintingFormatViewModel">organisationCentrePrintingFormatViewModel.</param>
        /// <returns>Returns OrganisationCentrePrintingFormatViewModel.</returns>
        OrganisationCentrePrintingFormatViewModel GetPrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel);
    }
}
