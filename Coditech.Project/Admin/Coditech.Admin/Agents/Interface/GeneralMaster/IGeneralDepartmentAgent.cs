using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralDepartmentAgent
    {
        /// <summary>
        /// Gets list of General Department.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralDepartmentListViewModel GetDepartmentList(DataTableViewModel dataTableModel);
        
        /// <summary>
        /// Create general Department.
        /// </summary>
        /// <param name="generalDepartmentViewModel">General Department View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralDepartmentViewModel CreateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel);

        /// <summary>
        /// Get general Department list by general Department id.
        /// </summary>
        /// <param name="generalDepartmentId">GeneralDepartment list Id</param>
        /// <returns>Returns GeneralDepartmentViewModel.</returns>
        GeneralDepartmentViewModel GetDepartment(int generalDepartmentId);

        /// <summary>
        /// Update general Department.
        /// </summary>
        /// <param name="generalDepartmentViewModel">GeneralDepartment view model to update.</param>
        /// <returns>Returns updated general Department model.</returns>
        GeneralDepartmentViewModel UpdateDepartment(GeneralDepartmentViewModel generalDepartmentViewModel);

        /// <summary>
        /// Delete general Department.
        /// </summary>
        /// <param name="generalDepartmentId">General Department Id.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteDepartment(string generalDepartmentId, out string errorMessage);
    }
}
