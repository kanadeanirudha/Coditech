using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralEnumaratorGroupService
    {
        #region EnumaratorGroup
        GeneralEnumaratorGroupListModel GetEnumaratorGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralEnumaratorGroupModel CreateEnumaratorGroup(GeneralEnumaratorGroupModel model);
        GeneralEnumaratorGroupModel GetEnumaratorGroup(int generalEnumaratorGroupMasterId);
        bool UpdateEnumaratorGroup(GeneralEnumaratorGroupModel model);
        bool DeleteEnumaratorGroup(ParameterModel parameterModel);
        #endregion

        #region Enumarator
        GeneralEnumaratorModel InsertUpdateEnumarator(GeneralEnumaratorModel model);
        GeneralEnumaratorModel GetEnumarator(int heneralEnumaratorMasterId);
        bool DeleteEnumarator(ParameterModel parameterModel);
        #endregion
    }
}
