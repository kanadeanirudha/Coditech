using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralEnumaratorMasterService
    {
        GeneralEnumaratorListModel GetEnumaratorList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralEnumaratorModel CreateEnumarator(GeneralEnumaratorModel model);
        GeneralEnumaratorModel GetEnumarator(int GeneralEnumaratorMasterId);
        bool UpdateEnumarator(GeneralEnumaratorModel model);
        bool DeleteEnumarator(ParameterModel parameterModel);
    }
}
