using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IEmployeeServiceService
    {
        EmployeeServiceListModel GetEmployeeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        EmployeeServiceModel GetEmployeeService(long employeeId);
        bool UpdateEmployeeService(EmployeeServiceModel model);
        bool DeleteEmployee(ParameterModel parameterModel);
    }
}
