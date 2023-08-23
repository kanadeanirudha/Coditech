using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralNationalityMasterService
    {
        GeneralNationalityListModel GetNationalityList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralNationalityModel CreateNationality(GeneralCityModel model);
        GeneralNationalityModel GetNationality(short generalCityMasterId);
        bool UpdateNationality(GeneralNationalityModel model);
        bool DeleteNationality(ParameterModel parameterModel);
    }
}
