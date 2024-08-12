using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralDistrictMasterService
    {
        GeneralDistrictListModel GetDistrictList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralDistrictModel CreateDistrict(GeneralDistrictModel model);
        GeneralDistrictModel GetDistrict(short generalDistrictMasterId);
        bool UpdateDistrict(GeneralDistrictModel model);
        bool DeleteDistrict(ParameterModel parameterModel);
        GeneralDistrictListModel GetDistrictByRegionWise(int generalRegionMasterId);
    }
}
