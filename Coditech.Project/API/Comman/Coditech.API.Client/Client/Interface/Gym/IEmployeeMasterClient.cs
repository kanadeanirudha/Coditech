using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IEmployeeMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of Employee.
        /// </summary>
        /// <returns>EmployeeMasterListResponse</returns>
        EmployeeMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeMasterResponse.</returns>
        EmployeeMasterResponse GetEmployeeOtherDetails(int employeeId);

        /// <summary>
        /// Update Employee Master.
        /// </summary>
        /// <param name="EmployeeMasterModel">EmployeeMasterModel.</param>
        /// <returns>Returns updated EmployeeMasterResponse</returns>
        EmployeeMasterResponse UpdateEmployeeOtherDetails(EmployeeMasterModel body);

        /// <summary>
        /// Delete Employee.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEmployee(ParameterModel body);

        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <returns>GymMemberFollowUpListResponse</returns>
        /// GymMemberFollowUpListResponse GymMemberFollowUpList(int employeeId, long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

    }
}
