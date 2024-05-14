using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IEmployeeServiceClient : IBaseClient
    {
        /// <summary>
        /// Get list of Employee.
        /// </summary>
        /// <returns>EmployeeServiceListResponse</returns>
        EmployeeServiceListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get Employee by employeeId.
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns>Returns EmployeeServiceResponse.</returns>
        EmployeeServiceResponse GetEmployeeService(long employeeId);

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
        TrueFalseResponse DeleteEmployee(ParameterModel body);
    }
}
