using Coditech.Admin.ViewModel;

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
        /// <param name="generalEnumaratorGroupViewModel">General EnumaratorGroup View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralEnumaratorGroupViewModel CreateEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel);

        /// <summary>
        /// Get EnumaratorGroup by generalEnumaratorGroupId.
        /// </summary>
        /// <param name="generalEnumaratorGroupId">generalEnumaratorGroupId</param>
        /// <returns>Returns GeneralEnumaratorGroupViewModel.</returns>
        GeneralEnumaratorGroupViewModel GetEnumaratorGroup(int generalEnumaratorGroupId);

        /// <summary>
        /// Update EnumaratorGroup.
        /// </summary>
        /// <param name="generalEnumaratorGroupViewModel">generalEnumaratorGroupViewModel.</param>
        /// <returns>Returns updated GeneralEnumaratorGroupViewModel</returns>
        GeneralEnumaratorGroupViewModel UpdateEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel);

        /// <summary>
        /// Delete EnumaratorGroup.
        /// </summary>
        /// <param name="generalEnumaratorGroupId">generalEnumaratorGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEnumaratorGroup(string generalEnumaratorGroupId, out string errorMessage);

        /// <summary>
        /// Get Enumarator by generalEnumaratorId.
        /// </summary>
        /// <param name="generalEnumaratorId">generalEnumaratorId</param>
        /// <returns>Returns GeneralEnumaratorViewModel.</returns>
        GeneralEnumaratorViewModel GetEnumarator(int generalEnumaratorId);

        /// <summary>
        /// Update Enumarator.
        /// </summary>
        /// <param name="generalEnumaratorViewModel">generalEnumaratorViewModel.</param>
        /// <returns>Returns updated GeneralEnumaratorViewModel</returns>
        GeneralEnumaratorViewModel InsertUpdateEnumarator(GeneralEnumaratorViewModel generalEnumaratorViewModel);

        /// <summary>
        /// Delete Enumarator.
        /// </summary>
        /// <param name="generalEnumaratorId">generalEnumaratorId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEnumarator(string generalEnumaratorId, out string errorMessage);
    }
}
