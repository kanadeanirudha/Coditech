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
        EmployeeServiceListViewModel GetEmployeeServiceList(long employeeId, long personId, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Employee.
        /// </summary>
        /// <param name="EmployeeServiceViewModel">employee Create View Model.</param>
        /// <returns>Returns created model.</returns>
        EmployeeServiceViewModel CreateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeServiceViewModel.</returns>
        EmployeeServiceViewModel GetEmployeeService(long employeeId, long personId,long employeeServiceId);

        /// <summary>
        /// Update Employee Service.
        /// </summary>
        /// <param name="EmployeeServiceViewModel">EmployeeServiceViewModel.</param>
        /// <returns>Returns updated EmployeeServiceViewModel</returns>
        EmployeeServiceViewModel UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel);

        /// <summary>
        /// Delete employee.
        /// </summary>
        /// <param name="employeeServiceIds">employeeServiceIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEmployeeService(string employeeServiceIds, out string errorMessage);
    }
}
