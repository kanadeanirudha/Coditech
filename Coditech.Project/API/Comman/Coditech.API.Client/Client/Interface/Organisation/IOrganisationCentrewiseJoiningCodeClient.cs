using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IOrganisationCentrewiseJoiningCodeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Organisation Centrewise Joining Code .
        /// </summary>
        /// <returns>OrganisationCentrewiseJoiningCodeListResponse</returns>
        OrganisationCentrewiseJoiningCodeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create PaymentGateways.
        /// </summary>
        /// <param name="OrganisationCentrewiseJoiningCodeModel">OrganisationCentrewiseJoiningCodeModel.</param>
        /// <returns>Returns PaymentGatewaysResponse.</returns>
        OrganisationCentrewiseJoiningCodeResponse CreateOrganisationCentrewiseJoiningCode(OrganisationCentrewiseJoiningCodeModel body);
    }
}
