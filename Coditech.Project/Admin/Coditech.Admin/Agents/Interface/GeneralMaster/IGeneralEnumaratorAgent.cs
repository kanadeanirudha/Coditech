using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralEnumaratorAgent
    {
        /// <summary>
        /// Get list of General Enumarator.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralEnumaratorListViewModel</returns>
        GeneralEnumaratorListViewModel GetEnumaratorList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Enumarator.
        /// </summary>
        /// <param name="GeneralEnumaratorViewModel">General Enumarator View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralEnumaratorViewModel CreateEnumarator(GeneralEnumaratorViewModel generalEnumaratorViewModel);

        /// <summary>
        /// Get Enumarator by GeneralEnumaratorMasterId.
        /// </summary>
        /// <param name="GeneralEnumaratorMasterId">GeneralEnumaratorMasterId</param>
        /// <returns>Returns GeneralEnumaratorViewModel.</returns>
        GeneralEnumaratorViewModel GetEnumarator(int generalEnumaratorMasterId);

        /// <summary>
        /// Update Enumarator.
        /// </summary>
        /// <param name="GeneralEnumaratorViewModel">GeneralEnumaratorViewModel.</param>
        /// <returns>Returns updated GeneralEnumaratorViewModel</returns>
        GeneralEnumaratorViewModel UpdateEnumarator(GeneralEnumaratorViewModel generalEnumaratorViewModel);

        /// <summary>
        /// Delete Enumarator.
        /// </summary>
        /// <param name="GeneralEnumaratorMasterId">GeneralEnumaratorMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEnumarator(string generalEnumaratorMasterId, out string errorMessage);
        GeneralEnumaratorListResponse GetEnumaratorList();
    }
}
