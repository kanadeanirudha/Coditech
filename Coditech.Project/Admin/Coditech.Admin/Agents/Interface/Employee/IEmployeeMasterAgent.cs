using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IEmployeeMasterAgent
    {
        /// <summary>
        /// Get list of employee Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>EmployeeMasterListViewModel</returns>
        EmployeeMasterListViewModel GetEmployeeMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Employee.
        /// </summary>
        /// <param name="EmployeeCreateEditViewModel">employee Create View Model.</param>
        /// <returns>Returns created model.</returns>
        EmployeeCreateEditViewModel CreateEmployee(EmployeeCreateEditViewModel employeeCreateEditViewModel);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeCreateEditViewModel.</returns>
        EmployeeCreateEditViewModel GetEmployeePersonalDetails(long personId);

        /// <summary>
        /// Update Employee Personal Details.
        /// </summary>
        /// <param name="EmployeeMasterViewModel">EmployeeCreateEditViewModel.</param>
        /// <returns>Returns updated EmployeeCreateEditViewModel</returns>
        EmployeeCreateEditViewModel UpdateEmployeePersonalDetails(EmployeeCreateEditViewModel employeeCreateEditViewModel);

        /// <summary>
        /// Delete employee.
        /// </summary>
        /// <param name="employeeIds">employeeIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEmployee(string employeeIds, out string errorMessage);
    }
}
