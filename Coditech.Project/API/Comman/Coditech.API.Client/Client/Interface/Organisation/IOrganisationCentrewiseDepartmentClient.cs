using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IOrganisationCentrewiseDepartmentClient : IBaseClient
    {
        /// <summary>
        /// Get list of OrganisationCentrewiseDepartment.
        /// </summary>
        /// <returns>OrganisationCentrewiseDepartmentListResponse</returns>
        OrganisationCentrewiseDepartmentListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}
