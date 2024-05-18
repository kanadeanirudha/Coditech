using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IEmployeeServiceAgent
    {
        /// <summary>
        /// Get list of employee Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>EmployeeServiceListViewModel</returns>
        EmployeeServiceListViewModel GetEmployeeServiceList(DataTableViewModel dataTableModel, int employeeId, long personId);

        /// <summary>
        /// Create Employee.
        /// </summary>
        /// <param name="EmployeeServiceViewModel">employee Create View Model.</param>
        /// <returns>Returns created model.</returns>
        EmployeeServiceViewModel CreateEmployee(EmployeeServiceViewModel employeeServiceViewModel);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeServiceViewModel.</returns>
        EmployeeServiceViewModel GetEmployeeService(long employeeId, long personId);

        /// <summary>
        /// Update Employee Service.
        /// </summary>
        /// <param name="EmployeeServiceViewModel">EmployeeServiceViewModel.</param>
        /// <returns>Returns updated EmployeeServiceViewModel</returns>
        EmployeeServiceViewModel UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel);

        /// <summary>
        /// Delete employee.
        /// </summary>
        /// <param name="employeeIds">employeeIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEmployee(string employeeIds, out string errorMessage);
    }
}
