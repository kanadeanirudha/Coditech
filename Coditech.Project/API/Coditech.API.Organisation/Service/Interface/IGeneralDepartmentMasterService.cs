using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralDepartmentMasterService
    {
        GeneralDepartmentListModel GetDepartmentList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralDepartmentModel CreateDepartment(GeneralDepartmentModel model);
        GeneralDepartmentModel GetDepartment(short deneralDepartmentMasterId);
        bool UpdateDepartment(GeneralDepartmentModel model);
        bool DeleteDepartment(ParameterModel parameterModel);
        GeneralDepartmentListModel GetDepartmentsByCentreCode(string centreCode);
    }
}
