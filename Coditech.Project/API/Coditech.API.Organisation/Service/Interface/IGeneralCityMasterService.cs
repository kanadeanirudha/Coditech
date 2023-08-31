using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralCityMasterService
    {
        GeneralCityListModel GetCityList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralCityModel CreateCity(GeneralCityModel model);
        GeneralCityModel GetCity(int generalCityMasterId);
        bool UpdateCity(GeneralCityModel model);
        bool DeleteCity(ParameterModel parameterModel);
    }
}
