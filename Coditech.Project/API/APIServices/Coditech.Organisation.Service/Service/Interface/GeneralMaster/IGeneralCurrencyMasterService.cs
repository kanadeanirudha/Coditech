using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralCurrencyMasterService
    {
        GeneralCurrencyMasterListModel GetCurrencyList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralCurrencyMasterModel CreateCurrency(GeneralCurrencyMasterModel model);
        GeneralCurrencyMasterModel GetCurrency(short generalCurrencyMasterId);
        bool UpdateCurrency(GeneralCurrencyMasterModel model);
        bool DeleteCurrency(ParameterModel parameterModel);
    }
}
