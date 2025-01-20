using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralTaxGroupMasterService
    {
        GeneralTaxGroupMasterListModel GetTaxGroupMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralTaxGroupModel CreateTaxGroupMaster(GeneralTaxGroupModel model);
        GeneralTaxGroupModel GetTaxGroupMaster(short generalTaxGroupMasterId);
        bool UpdateTaxGroupMaster(GeneralTaxGroupModel model);
        bool DeleteTaxGroupMaster(ParameterModel parameterModel);
    }
}
