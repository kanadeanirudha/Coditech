using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralEnumaratorGroupMasterService
    {
        GeneralEnumaratorGroupListModel GetGeneralEnumaratorGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralEnumaratorGroupModel CreateGeneralEnumaratorGroup(GeneralEnumaratorGroupModel model);
        GeneralEnumaratorGroupModel GetGeneralEnumaratorGroup(int generalGeneralEnumaratorGroupMasterId);
        bool UpdateGeneralEnumaratorGroup(GeneralEnumaratorGroupModel model);
        bool DeleteGeneralEnumaratorGroup(ParameterModel parameterModel);
    }
}
