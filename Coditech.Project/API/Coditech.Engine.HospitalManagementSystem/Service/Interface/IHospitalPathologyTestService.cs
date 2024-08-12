using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPathologyTestService
    {
        HospitalPathologyTestListModel GetHospitalPathologyTestList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPathologyTestModel CreateHospitalPathologyTest(HospitalPathologyTestModel model);
        HospitalPathologyTestModel GetHospitalPathologyTest(long hospitalPathologyTestId);
        bool UpdateHospitalPathologyTest(HospitalPathologyTestModel model);
        bool DeleteHospitalPathologyTest(ParameterModel parameterModel);
    }
}
