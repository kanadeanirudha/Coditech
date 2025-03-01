using Coditech.Admin.ViewModel;
namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewiseJoiningCodeAgent
    {
        /// <summary>
        /// Get list of Organisation Centrewise Joining Code.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentrewiseJoiningCodeListViewModel</returns>
        OrganisationCentrewiseJoiningCodeListViewModel GetOrganisationCentrewiseJoiningCodeList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Organisation Centrewise Joining Code.
        /// </summary>
        /// <param name="OrganisationCentrewiseJoiningCodeViewModel">Organisation Centrewise Joining Code View Model.</param>
        /// <returns>Returns created model.</returns>
        OrganisationCentrewiseJoiningCodeViewModel CreateOrganisationCentrewiseJoiningCode(OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel);

        /// <summary>
        /// Organisation Centrewise Joining Code Send.
        /// </summary>
        /// <param name="joiningCode">joiningCode</param>
        /// <returns>Returns the Organisation Centrewise Joining Code Send view model.</returns>
        OrganisationCentrewiseJoiningCodeViewModel OrganisationCentrewiseJoiningCodeSend(string joiningCode, string emailId, string mobileNumber);
    }
}
