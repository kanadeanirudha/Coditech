using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAccSetupMasterService
    {
        AccSetupMasterListModel GetAccSetupMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AccSetupMasterModel CreateAccSetupMaster(AccSetupMasterModel model);
        AccSetupMasterModel GetAccSetupMaster(short accSetupMasterId);
        bool UpdateAccSetupMaster(AccSetupMasterModel model);
        bool DeleteAccSetupMaster(ParameterModel parameterModel);
    }
}
