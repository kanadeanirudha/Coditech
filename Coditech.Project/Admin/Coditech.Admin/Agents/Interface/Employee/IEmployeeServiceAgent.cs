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
        EmployeeServiceListViewModel GetEmployeeServiceList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Get Employee Service by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeServiceViewModel.</returns>
        EmployeeServiceViewModel GetEmployeeService(long employeeId);


        /// <summary>
        /// Update Employee Service.
        /// </summary>
        /// <param name="EmployeeServiceViewModel">EmployeeMasterViewModel.</param>
        /// <returns>Returns updated EmployeeMasterViewModel</returns>
        EmployeeServiceViewModel UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel);
    }
}
