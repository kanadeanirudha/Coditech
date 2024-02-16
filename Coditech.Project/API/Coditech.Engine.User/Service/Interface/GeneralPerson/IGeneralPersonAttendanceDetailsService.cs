using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralPersonAttendanceDetailsService
    {
        GeneralPersonAttendanceDetailsListModel GetPersonAttendanceList(long personId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPersonAttendanceDetailsModel CreatePersonAttendance(GeneralPersonAttendanceDetailsModel model);
        GeneralPersonAttendanceDetailsModel GetPersonAttendance(long generalPersonAttendanceDetailsId);
        bool UpdatePersonAttendance(GeneralPersonAttendanceDetailsModel model);
        bool DeletePersonAttendance(ParameterModel parameterModel);
    }
}

