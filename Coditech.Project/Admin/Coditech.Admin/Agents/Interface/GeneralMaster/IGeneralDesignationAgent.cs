using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralDesignationAgent
    {
        /// <summary>
        /// Get list of General Designation.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralDesignationListViewModel</returns>
        GeneralDesignationListViewModel GetDesignationList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="generalDesignationViewModel">General Designation View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralDesignationViewModel CreateDesignation(GeneralDesignationViewModel generalDesignationViewModel);

        /// <summary>
        /// Get Designation by designationId.
        /// </summary>
        /// <param name="designationId">designationId</param>
        /// <returns>Returns GeneralDesignationViewModel.</returns>
        GeneralDesignationViewModel GetDesignation(short designationId);

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="generalDesignationViewModel">generalDesignationViewModel.</param>
        /// <returns>Returns updated GeneralDesignationViewModel</returns>
        GeneralDesignationViewModel UpdateDesignation(GeneralDesignationViewModel generalDesignationViewModel);

        /// <summary>
        /// Delete Designation.
        /// </summary>
        /// <param name="designationId">designationId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteDesignation(string designationId, out string errorMessage);
    }
}
