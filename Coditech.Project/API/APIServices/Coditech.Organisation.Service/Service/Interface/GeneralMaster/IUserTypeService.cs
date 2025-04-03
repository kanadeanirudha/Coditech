using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IUserTypeService
    {
        UserTypeListModel GetUserTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        UserTypeModel CreateUserType(UserTypeModel model);
        UserTypeModel GetUserType(short userTypeId);
        bool UpdateUserType(UserTypeModel model);
        bool DeleteUserType(ParameterModel parameterModel);
    }
}
