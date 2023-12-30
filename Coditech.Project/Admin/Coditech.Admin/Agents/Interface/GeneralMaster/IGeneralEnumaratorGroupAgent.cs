using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralEnumaratorGroupAgent
    {
        /// <summary>
        /// Get list of General GeneralEnumaratorGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralEnumaratorGroupListViewModel</returns>
        GeneralEnumaratorGroupListViewModel GetGeneralEnumaratorGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create GeneralEnumaratorGroup.
        /// </summary>
        /// <param name="generalEnumaratorGroupViewModel">General GeneralEnumaratorGroup View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralEnumaratorGroupViewModel CreateGeneralEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel);

        /// <summary>
        /// Get GeneralEnumaratorGroup by generalEnumaratorGroupId.
        /// </summary>
        /// <param name="generalEnumaratorGroupId">generalEnumaratorGroupId</param>
        /// <returns>Returns GeneralEnumaratorGroupViewModel.</returns>
        GeneralEnumaratorGroupViewModel GetGeneralEnumaratorGroup(int generalEnumaratorGroupId);

        /// <summary>
        /// Update GeneralEnumaratorGroup.
        /// </summary>
        /// <param name="generalEnumaratorGroupViewModel">generalEnumaratorGroupViewModel.</param>
        /// <returns>Returns updated GeneralEnumaratorGroupViewModel</returns>
        GeneralEnumaratorGroupViewModel UpdateGeneralEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel);

        /// <summary>
        /// Delete GeneralEnumaratorGroup.
        /// </summary>
        /// <param name="generalEnumaratorGroupId">generalEnumaratorGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGeneralEnumaratorGroup(string generalEnumaratorGroupId, out string errorMessage);
        GeneralEnumaratorGroupListResponse GetGeneralEnumaratorGroupList();
    }
}
