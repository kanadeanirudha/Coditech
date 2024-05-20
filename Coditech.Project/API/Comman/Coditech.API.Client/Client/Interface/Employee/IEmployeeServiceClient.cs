using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IEmployeeServiceClient : IBaseClient
    {
        /// <summary>
        /// Get list of Employee Service List.
        /// </summary>
        /// <returns>EmployeeServiceListResponse</returns>
        EmployeeServiceListResponse EmployeeServiceList(long employeeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);


        /// <summary>
        /// Create Employee.
        /// </summary>
        /// <param name="EmployeeServiceModel">EmployeeServiceModel.</param>
        /// <returns>Returns EmployeeServiceResponse.</returns>
        EmployeeServiceResponse CreateEmployeeService(EmployeeServiceModel body);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <param name="personId">personId</param>
        /// <param name="employeeServiceId">employeeServiceId</param>
        /// <returns>Returns EmployeeServiceResponse.</returns>
        EmployeeServiceResponse GetEmployeeService(long employeeId, long personId, long employeeServiceId);

        /// <summary>
        /// Update Employee Service.
        /// </summary>
        /// <param name="EmployeeServiceModel">EmployeeServiceModel.</param>
        /// <returns>Returns updated EmployeeServiceResponse</returns>
        EmployeeServiceResponse UpdateEmployeeService(EmployeeServiceModel model);

        /// <summary>
        /// Delete Employee.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEmployeeService(ParameterModel body);
    }
}
