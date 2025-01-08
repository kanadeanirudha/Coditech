using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralPersonFollowUpAgent
    {
        /// <summary>
        /// Get list of General PersonFollowUp.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralPersonFollowUpListViewModel</returns>
        GeneralPersonFollowUpListViewModel GetPersonFollowUpList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create PersonFollowUp.
        /// </summary>
        /// <param name="GeneralPersonFollowUpViewModel">General PersonFollowUp View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralPersonFollowUpViewModel CreatePersonFollowUp(GeneralPersonFollowUpViewModel GeneralPersonFollowUpViewModel);

        /// <summary>
        /// Get PersonFollowUp by GeneralPersonFollowUpId.
        /// </summary>
        /// <param name="GeneralPersonFollowUpId">GeneralPersonFollowUpId</param>
        /// <returns>Returns GeneralPersonFollowUpViewModel.</returns>
        GeneralPersonFollowUpViewModel GetPersonFollowUp(long GeneralPersonFollowUpId);

        /// <summary>
        /// Update PersonFollowUp.
        /// </summary>
        /// <param name="GeneralPersonFollowUpViewModel">GeneralPersonFollowUpViewModel.</param>
        /// <returns>Returns updated GeneralPersonFollowUpViewModel</returns>
        GeneralPersonFollowUpViewModel UpdatePersonFollowUp(GeneralPersonFollowUpViewModel GeneralPersonFollowUpViewModel);

        /// <summary>
        /// Delete PersonFollowUp.
        /// </summary>
        /// <param name="GeneralPersonFollowUpId">GeneralPersonFollowUpId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeletePersonFollowUp(string GeneralPersonFollowUpId, out string errorMessage);
        GeneralPersonFollowUpListResponse GetPersonFollowUpList();
    }
}
