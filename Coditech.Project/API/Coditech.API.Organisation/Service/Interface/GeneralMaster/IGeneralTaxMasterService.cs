using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralTaxMasterService
    {
        GeneralTaxMasterListModel GetTaxMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralTaxMasterModel CreateTaxMaster(GeneralTaxMasterModel model);
        GeneralTaxMasterModel GetTaxMaster(short generalTaxMasterId);
        bool UpdateTaxMaster(GeneralTaxMasterModel model);
        bool DeleteTaxMaster(ParameterModel parameterModel);
    }
}
