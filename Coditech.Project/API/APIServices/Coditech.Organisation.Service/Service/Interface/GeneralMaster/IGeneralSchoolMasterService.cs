using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IGeneralSchoolMasterService
    {
        GeneralSchoolListModel GetSchoolList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralSchoolModel CreateSchool(GeneralSchoolModel model);
        GeneralSchoolModel GetSchool(short generalSchoolMasterId);
        bool UpdateSchool(GeneralSchoolModel model);
        bool DeleteSchool(ParameterModel parameterModel);
    }
}
