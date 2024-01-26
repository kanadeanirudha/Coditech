using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
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

        /// <summary>
        /// Update Associate UnAssociate Centrewise Department.
        /// </summary>
        /// <param name="OrganisationCentrewiseDepartmentModel">OrganisationCentrewiseDepartmentModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseDepartmentResponse</returns>
        OrganisationCentrewiseDepartmentResponse UpdateAssociateUnAssociateCentrewiseDepartment(OrganisationCentrewiseDepartmentModel body);
    }
}
