using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPatientTypeService
    {
        HospitalPatientTypeListModel GetHospitalPatientTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPatientTypeModel CreateHospitalPatientType(HospitalPatientTypeModel model);
        HospitalPatientTypeModel GetHospitalPatientType(byte hospitalPatientTypeId);
        bool UpdateHospitalPatientType(HospitalPatientTypeModel model);
        bool DeleteHospitalPatientType(ParameterModel parameterModel);
    }
}
