using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model;

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
        /// <param name="EmployeeMasterViewModel">employee Create View Model.</param>
        /// <returns>Returns created model.</returns>
        EmployeeMasterViewModel CreateEmployee(EmployeeMasterViewModel EmployeeMasterViewModel);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeMasterViewModel.</returns>
        EmployeeMasterViewModel GetEmployeeDetails(int employeeId);

        /// <summary>
        /// Update Member Personal Details.
        /// </summary>
        /// <param name="EmployeeMasterViewModel">EmployeeMasterViewModel.</param>
        /// <returns>Returns updated EmployeeMasterViewModel</returns>
        EmployeeMasterViewModel UpdateEmployeeDetails(EmployeeMasterViewModel EmployeeMasterViewModel);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeMasterResponse.</returns>
        //EmployeeMasterViewModel GetEmployeeDetails(int employeeId);

        ///// <summary>
        ///// Update Employee
        ///// </summary>
        ///// <param name="EmployeeMasterModel">EmployeeMasterModel.</param>
        ///// <returns>Returns updated EmployeeMasterViewModel</returns>
        //EmployeeMasterViewModel UpdateEmployeeOtherDetails(EmployeeMasterViewModel EmployeeMasterModel);

        /// <summary>
        /// Delete employee.
        /// </summary>
        /// <param name="employeeIds">employeeIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEmployee(string employeeIds, out string errorMessage);

        /// <summary>
        /// Get list of employee Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>employeeMemberFollowUpListViewModel</returns>
        /// employeeMemberFollowUpListViewModel employeeMemberFollowUpList(int employeeId, long employeeId, DataTableViewModel dataTableModel);
    }
}
