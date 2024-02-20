using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralPersonAttendanceDetailsService
    {
        GeneralPersonAttendanceDetailsListModel GetPersonAttendanceList(long entityId, string userType, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPersonAttendanceDetailsModel InserUpdateGeneralPersonAttendanceDetails(GeneralPersonAttendanceDetailsModel model);
        GeneralPersonAttendanceDetailsModel GetPersonAttendance(long generalPersonAttendanceDetailsId);
        bool DeletePersonAttendance(ParameterModel parameterModel);
    }
}

