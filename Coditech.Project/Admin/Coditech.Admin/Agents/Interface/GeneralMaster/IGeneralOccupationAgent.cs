using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralOccupationAgent
    {
        /// <summary>
        /// Get list of General Occupation.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralOccupationListViewModel</returns>
        GeneralOccupationListViewModel GetOccupationList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Occupation.
        /// </summary>
        /// <param name="generalOccupationViewModel">General Occupation View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralOccupationViewModel CreateOccupation(GeneralOccupationViewModel generalOccupationViewModel);

        /// <summary>
        /// Get Occupation by generalOccupationId.
        /// </summary>
        /// <param name="generalOccupationId">generalOccupationId</param>
        /// <returns>Returns GeneralOccupationViewModel.</returns>
        GeneralOccupationViewModel GetOccupation(short generalOccupationId);

        /// <summary>
        /// Update Occupation.
        /// </summary>
        /// <param name="generalOccupationViewModel">generalOccupationViewModel.</param>
        /// <returns>Returns updated GeneralOccupationViewModel</returns>
        GeneralOccupationViewModel UpdateOccupation(GeneralOccupationViewModel generalOccupationViewModel);

        /// <summary>
        /// Delete Occupation.
        /// </summary>
        /// <param name="generalOccupationId">generalOccupationId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteOccupation(string generalOccupationId, out string errorMessage);
        GeneralOccupationListResponse GetOccupationList();
    }
}
