using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPathologyTestGroupService
    {
        HospitalPathologyTestGroupListModel GetHospitalPathologyTestGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPathologyTestGroupModel CreateHospitalPathologyTestGroup(HospitalPathologyTestGroupModel model);
        HospitalPathologyTestGroupModel GetHospitalPathologyTestGroup(int hospitalPathologyTestGroupId);
        bool UpdateHospitalPathologyTestGroup(HospitalPathologyTestGroupModel model);
        bool DeleteHospitalPathologyTestGroup(ParameterModel parameterModel);
    }
}
