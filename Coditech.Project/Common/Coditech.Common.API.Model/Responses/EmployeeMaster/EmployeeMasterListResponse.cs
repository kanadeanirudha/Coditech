using Coditech.Common.API.Model.Response;

namespace Coditech.Common.API.Model.Responses.EmployeeMaster
{
    public class EmployeeMasterListResponse : BaseListResponse
    {
        public List<EmployeeMasterModel> EmployeeMasterList { get; set; }
    }
}
