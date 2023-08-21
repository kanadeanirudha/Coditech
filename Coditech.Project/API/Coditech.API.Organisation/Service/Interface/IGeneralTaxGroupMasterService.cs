using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralTaxGroupMasterService
    {
        GeneralTaxGroupMasterListModel GetTaxGroupMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralTaxGroupMasterModel CreateTaxGroupMaster(GeneralTaxGroupMasterModel model);
        GeneralTaxGroupMasterModel GetTaxGroupMaster(short generalTaxGroupMasterId);
        bool UpdateTaxGroupMaster(GeneralTaxGroupMasterModel model);
        bool DeleteTaxGroupMaster(ParameterModel parameterModel);
    }
}
