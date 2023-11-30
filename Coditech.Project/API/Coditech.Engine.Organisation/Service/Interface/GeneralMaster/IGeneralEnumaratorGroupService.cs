using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralEnumaratorGroupService
    {
        GeneralEnumaratorGroupListModel GetEnumaratorGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralEnumaratorGroupModel CreateEnumaratorGroup(GeneralEnumaratorGroupModel model);
        GeneralEnumaratorGroupModel GetEnumaratorGroup(int GeneralEnumaratorGroupMasterId);
        bool UpdateEnumaratorGroup(GeneralEnumaratorGroupModel model);
        bool DeleteEnumaratorGroup(ParameterModel parameterModel);
    }
}
