using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralCityMasterService
    {
        GeneralCityListModel GetCityList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralCityMasterModel CreateCity(GeneralCityMasterModel model);
        GeneralCityMasterModel GetCity(int generalCityMasterId);
        bool UpdateCity(GeneralCityMasterModel model);
        bool DeleteCity(ParameterModel parameterModel);
    }
}
