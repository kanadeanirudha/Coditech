using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
namespace Coditech.Admin.Agents
{
    public interface IAccSetupMasterAgent
    {
        /// <summary>
        /// Get list of AccSetupMaster.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AccSetupMasterListViewModel</returns>
        AccSetupMasterListViewModel GetAccSetupMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create AccSetupMaster.
        /// </summary>
        /// <param name="accSetupMasterViewModel">Accout Setup Master View Model.</param>
        /// <returns>Returns created model.</returns>
        AccSetupMasterViewModel CreateAccSetupMaster(AccSetupMasterViewModel accSetupMasterViewModel);

        /// <summary>
        /// Get AccSetupMaster by accSetupMasterId.
        /// </summary>
        /// <param name="accSetupMasterId">accSetupMasterId</param>
        /// <returns>Returns AccSetupMasterViewModel.</returns>
        AccSetupMasterViewModel GetAccSetupMaster(short accSetupMasterId);

        /// <summary>
        /// Update AccSetupMaster.
        /// </summary>
        /// <param name="accSetupMasterViewModel">accSetupMasterViewModel.</param>
        /// <returns>Returns updated AccSetupMasterViewModel</returns>
        AccSetupMasterViewModel UpdateAccSetupMaster(AccSetupMasterViewModel accSetupMasterViewModel);

        /// <summary>
        /// Delete AccSetupMaster.
        /// </summary>
        /// <param name="accSetupMasterId">accSetupMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteAccSetupMaster(string accSetupMasterId, out string errorMessage);
        AccSetupMasterListResponse GetAccSetupMasterList();
    }
}
