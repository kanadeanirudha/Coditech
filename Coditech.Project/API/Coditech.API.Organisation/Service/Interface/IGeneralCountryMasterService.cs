using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralCountryMasterService
    {
        GeneralCountryListModel GetCountryList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralCountryModel CreateCountry(GeneralCountryModel model);
        GeneralCountryModel GetCountry(short generalCountryMasterId);
        bool UpdateCountry(GeneralCountryModel model);
        bool DeleteCountry(ParameterModel parameterModel);
    }
}
