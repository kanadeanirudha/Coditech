using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPathologyTestPricesService
    {
        HospitalPathologyTestPricesListModel GetHospitalPathologyTestPricesList(int hospitalPathologyPriceCategoryEnumId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPathologyTestPricesModel CreateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel model);
        HospitalPathologyTestPricesModel GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId);
        bool UpdateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel model);
        bool DeleteHospitalPathologyTestPrices(ParameterModel parameterModel);
    }
}
