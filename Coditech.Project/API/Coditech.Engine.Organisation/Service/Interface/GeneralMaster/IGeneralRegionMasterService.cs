using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralRegionMasterService
    {
        GeneralRegionListModel GetRegionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralRegionModel CreateRegion(GeneralRegionModel model);
        GeneralRegionModel GetRegion(short generalRegionMasterId);
        bool UpdateRegion(GeneralRegionModel model);
        bool DeleteRegion(ParameterModel parameterModel);
        GeneralRegionListModel GetRegionByCountryWise(string countryCode);
    }
}
