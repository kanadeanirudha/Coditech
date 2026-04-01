using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralSchoolAgent
    {
        /// <summary>
        /// Get list of General School.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralSchoolListViewModel</returns>
        GeneralSchoolListViewModel GetSchoolList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create School.
        /// </summary>
        /// <param name="generalSchoolViewModel">General School View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralSchoolViewModel CreateSchool(GeneralSchoolViewModel generalSchoolViewModel);

        /// <summary>
        /// Get School by generalSchoolId.
        /// </summary>
        /// <param name="generalSchoolId">generalSchoolId</param>
        /// <returns>Returns GeneralSchoolViewModel.</returns>
        GeneralSchoolViewModel GetSchool(short generalSchoolId);

        /// <summary>
        /// Update School.
        /// </summary>
        /// <param name="generalSchoolViewModel">generalSchoolViewModel.</param>
        /// <returns>Returns updated GeneralSchoolViewModel</returns>
        GeneralSchoolViewModel UpdateSchool(GeneralSchoolViewModel generalSchoolViewModel);

        /// <summary>
        /// Delete School.
        /// </summary>
        /// <param name="generalSchoolId">generalSchoolId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteSchool(string generalSchoolId, out string errorMessage);
        GeneralSchoolListResponse GetSchoolList();
    }
}
