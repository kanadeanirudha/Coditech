using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralEnumaratorGroupAgent
    {
        /// <summary>
        /// Get list of General EnumaratorGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralEnumaratorGroupListViewModel</returns>
        GeneralEnumaratorGroupListViewModel GetEnumaratorGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create EnumaratorGroup.
        /// </summary>
        /// <param name="GeneralEnumaratorGroupViewModel">General EnumaratorGroup View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralEnumaratorGroupViewModel CreateEnumaratorGroup(GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel);

        /// <summary>
        /// Get EnumaratorGroup by GeneralEnumaratorGroupId.
        /// </summary>
        /// <param name="GeneralEnumaratorGroupId">GeneralEnumaratorGroupId</param>
        /// <returns>Returns GeneralEnumaratorGroupViewModel.</returns>
        GeneralEnumaratorGroupViewModel GetEnumaratorGroup(short GeneralEnumaratorGroupId);

        /// <summary>
        /// Update EnumaratorGroup.
        /// </summary>
        /// <param name="GeneralEnumaratorGroupViewModel">GeneralEnumaratorGroupViewModel.</param>
        /// <returns>Returns updated GeneralEnumaratorGroupViewModel</returns>
        GeneralEnumaratorGroupViewModel UpdateEnumaratorGroup(GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel);

        /// <summary>
        /// Delete EnumaratorGroup.
        /// </summary>
        /// <param name="GeneralEnumaratorGroupId">GeneralEnumaratorGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEnumaratorGroup(string GeneralEnumaratorGroupId, out string errorMessage);
        GeneralEnumaratorGroupListResponse GetEnumaratorGroupList();
    }
}
