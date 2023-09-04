using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralDepartmentAgent
    {
        /// <summary>
        /// Get list of General Department.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralDepartmentListViewModel</returns>
        GeneralDepartmentListViewModel GetDepartmentList(DataTableViewModel dataTableModel);
        
        /// <summary>
        /// Create Department.
        /// </summary>
        /// <param name="generalDepartmentViewModel">General Department View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralDepartmentViewModel CreateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel);

        /// <summary>
        /// Get Department by generalDepartmentId.
        /// </summary>
        /// <param name="generalDepartmentId">generalDepartmentId</param>
        /// <returns>Returns GeneralDepartmentViewModel.</returns>
        GeneralDepartmentViewModel GetDepartment(int generalDepartmentId);

        /// <summary>
        /// Update Department.
        /// </summary>
        /// <param name="generalDepartmentViewModel">generalDepartmentViewModel.</param>
        /// <returns>Returns updated GeneralDepartmentViewModel</returns>
        GeneralDepartmentViewModel UpdateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel);

        /// <summary>
        /// Delete Department.
        /// </summary>
        /// <param name="generalDepartmentId">generalDepartmentId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteDepartment(string generalDepartmentId, out string errorMessage);
    }
}
