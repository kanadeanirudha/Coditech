using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
namespace Coditech.API.Client
{
    public interface IOrganisationCentrewiseAccountSetupClient : IBaseClient
    {
        /// <summary>
        /// Get OrganisationCentrewiseAccountSetup by organisationCentrewiseAccountSetupId.
        /// </summary>
        /// <param name="organisationCentrewiseAccountSetupId">organisationCentrewiseAccountSetupId</param>
        /// <param name="centreCode">centreCode</param>
        /// <returns>Returns OrganisationCentrewiseAccountSetupResponse.</returns>
        OrganisationCentrewiseAccountSetupResponse GetOrganisationCentrewiseAccountSetup(string centreCode);

        /// <summary>
        /// Update OrganisationCentrewiseAccountSetup.
        /// </summary>
        /// <param name="OrganisationCentrewiseAccountSetupModel">OrganisationCentrewiseAccountSetupModel.</param>
        /// <returns>Returns updated OrganisationCentrewiseAccountSetupResponse</returns>
        OrganisationCentrewiseAccountSetupResponse UpdateOrganisationCentrewiseAccountSetup(OrganisationCentrewiseAccountSetupModel body);
    }
}
